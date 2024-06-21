/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.AddOns.Agility
{
    using Opsive.Shared.Events;
    using Opsive.Shared.Game;
    using Opsive.Shared.StateSystem;
    using Opsive.UltimateCharacterController.Character.Abilities;
    using Opsive.UltimateCharacterController.Game;
    using Opsive.UltimateCharacterController.Utility;
    using UnityEngine;

    /// <summary>
    /// The Crawl ability will play a crawling animation with the character moving on all four limbs. If the crawling ability is started by a trigger the ability
    /// will not stop until after the character has left the trigger.
    /// </summary>
    [DefaultState("Crawl")]
    [DefaultStartType(AbilityStartType.Manual)]
    [DefaultStopType(AbilityStopType.Manual)]
    [DefaultAbilityIndex(103)]
    [DefaultUseRootMotionPosition(AbilityBoolOverride.True)]
    [DefaultEquippedSlots(0)]
    [DefaultReequipSlots(true)]
    [Opsive.Shared.Utility.Group("Agility Pack")]
    public class Crawl : HeightChange
    {
        [Tooltip("The name of the state that should activate when crawl is active. Crawl will not be active when the ability is playing the stop animation.")]
        [SerializeField] protected string m_CrawlActiveStateName = "CrawlActive";
        [Tooltip("The maximum number of valid triggers that the ability can detect. Set to 0 to prevent the ability from starting with a trigger.")]
        [SerializeField] protected int m_MaxTriggerObjectCount = 1;
        [Tooltip("The LayerMask of the trigger that should be detected.")]
        [SerializeField] protected LayerMask m_TriggerLayerMask = ~(1 << LayerManager.UI | 1 << LayerManager.SubCharacter | 1 << LayerManager.Overlay | 1 << LayerManager.VisualEffect);
        [Tooltip("The unique ID value of the trigger's Object Identifier component. A value of -1 indicates that this ID should not be used.")]
        [SerializeField] protected int m_TriggerObjectID = -1;

        public string CrawlActiveStateName { get { return m_CrawlActiveStateName; } set { m_CrawlActiveStateName = value; } }
        public LayerMask TriggerLayerMask { get { return m_TriggerLayerMask; } set { m_TriggerLayerMask = value; } }
        public int TriggerObjectID { get { return m_TriggerObjectID; } set { m_TriggerObjectID = value; } }

        /// <summary>
        /// Specifies the current state that the ability is in.
        /// </summary>
        private enum CrawlAbilityState
        {
            Active,     // The Crawl ability is running.
            Stopping,   // The Crawl ability is stopping.
            Complete    // The Crawl ability has completed.
        }
        private CrawlAbilityState m_CrawlState = CrawlAbilityState.Complete;
        private GameObject[] m_DetectedTriggerObjects;
        private int m_DetectedTriggerObjectsCount;
        private bool m_TriggerStart;

        private CrawlAbilityState CrawlState {
            get { return m_CrawlState; }
            set {
                if (m_CrawlState != value) {
                    m_CrawlState = value;
                    var updateState = false;
                    if (m_CrawlState == CrawlAbilityState.Active) {
                        EventHandler.ExecuteEvent(m_GameObject, "OnHeightChangeAdjustHeight", m_CapsuleColliderHeightAdjustment);
                        updateState = true;
                    } else if (m_CrawlState == CrawlAbilityState.Stopping) {
                        EventHandler.ExecuteEvent(m_GameObject, "OnHeightChangeAdjustHeight", -m_CapsuleColliderHeightAdjustment);
                        updateState = true;
                    }
                    if (updateState && !string.IsNullOrEmpty(m_CrawlActiveStateName)) {
                        StateManager.SetState(m_GameObject, m_CrawlActiveStateName, m_CrawlState == CrawlAbilityState.Active);
                    }
                }
            }
        }
        public override int AbilityIntData { get { return m_CrawlState == CrawlAbilityState.Active ? 0 : 1; } }
        public override bool IsConcurrent { get { return false; } }

        /// <summary>
        /// Initialize the default values.
        /// </summary>
        public override void Awake()
        {
            base.Awake();

            if (m_MaxTriggerObjectCount > 0) {
                m_DetectedTriggerObjects = new GameObject[m_MaxTriggerObjectCount];
            }

            EventHandler.RegisterEvent(m_GameObject, "OnAnimatorCrawlComplete", OnCrawlComplete);
        }

        /// <summary>
        /// Can the ability be started?
        /// </summary>
        /// <returns>True if the ability can be started.</returns>
        public override bool CanStartAbility()
        {
            // An attribute may prevent the ability from starting.
            if (!base.CanStartAbility()) {
                return false;
            }

            // The character has to be on the ground.
            if (!m_CharacterLocomotion.Grounded) {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The ability will start - perform any initialization before starting.
        /// </summary>
        /// <returns>True if the ability should start.</returns>
        public override bool AbilityWillStart()
        {
            // The character may have entered and left the trigger in the time that it too the ability to start.
            if (m_MaxTriggerObjectCount > 0 && m_DetectedTriggerObjectsCount == 0) {
                return false;
            }

            return base.AbilityWillStart();
        }

        /// <summary>
        /// The ability has started.
        /// </summary>
        protected override void AbilityStarted()
        {
            base.AbilityStarted();

            CrawlState = CrawlAbilityState.Active;

            EventHandler.ExecuteEvent(m_GameObject, "OnHeightChangeAdjustHeight", m_CapsuleColliderHeightAdjustment);
        }

        /// <summary>
        /// Called when another ability is attempting to start and the current ability is active.
        /// Returns true or false depending on if the new ability should be blocked from starting.
        /// </summary>
        /// <param name="startingAbility">The ability that is starting.</param>
        /// <returns>True if the ability should be blocked.</returns>
        public override bool ShouldBlockAbilityStart(Ability startingAbility)
        {
            return startingAbility is StoredInputAbilityBase || startingAbility is HeightChange;
        }

        /// <summary>
        /// Called when the current ability is attempting to start and another ability is active.
        /// Returns true or false depending on if the active ability should be stopped.
        /// </summary>
        /// <param name="activeAbility">The ability that is currently active.</param>
        /// <returns>True if the ability should be stopped.</returns>
        public override bool ShouldStopActiveAbility(Ability activeAbility)
        {
            return activeAbility is StoredInputAbilityBase;
        }
        
        /// <summary>
        /// Callback when the ability tries to be stopped. Play the get up animation.
        /// </summary>
        public override void WillTryStopAbility()
        {
            if ((!m_TriggerStart || (m_TriggerStart && m_DetectedTriggerObjectsCount == 0)) && CrawlState == CrawlAbilityState.Active && base.CanStopAbility(false)) {
                CrawlState = CrawlAbilityState.Stopping;
                UpdateAbilityAnimatorParameters();
            }
        }

        /// <summary>
        /// The Crawl ability has completed.
        /// </summary>
        private void OnCrawlComplete()
        {
            CrawlState = CrawlAbilityState.Complete;
            StopAbility();
        }

        /// <summary>
        /// Can the ability be stopped?
        /// </summary>
        /// <param name="force">Should the ability be force stopped?</param>
        /// <returns>True if the ability can be stopped.</returns>
        public override bool CanStopAbility(bool force)
        {
            if (force) { return true; }

            return CrawlState == CrawlAbilityState.Complete;
        }

        /// <summary>
        /// The character has entered a trigger.
        /// </summary>
        /// <param name="other">The trigger collider that the character entered.</param>
        public override void OnTriggerEnter(Collider other)
        {
            // The object may not be detected with a trigger.
            if (m_MaxTriggerObjectCount == 0 || m_DetectedTriggerObjects.Length <= m_DetectedTriggerObjectsCount) {
                return;
            }

            // The object has to use the correct mask.
            if (!MathUtility.InLayerMask(other.gameObject.layer, m_TriggerLayerMask)) {
                return;
            }

            // If an object id is specified then the object must have the Object Identifier component attached with the specified ID.
            if (m_TriggerObjectID != -1) {
                var objectIdentifier = other.gameObject.GetCachedParentComponent<Objects.ObjectIdentifier>();
                if (objectIdentifier == null || objectIdentifier.ID != m_TriggerObjectID) {
                    return;
                }
            }
            m_DetectedTriggerObjects[m_DetectedTriggerObjectsCount] = other.gameObject;
            m_DetectedTriggerObjectsCount++;

            // The trigger is valid and the ability should start.
            if (!IsActive) {
                m_TriggerStart = true;
                StartAbility();
            } else if (CrawlState == CrawlAbilityState.Stopping) {
                CrawlState = CrawlAbilityState.Active;
                UpdateAbilityAnimatorParameters();
            }
        }

        /// <summary>
        /// The character has exited a trigger.
        /// </summary>
        /// <param name="other">The trigger collider that the character exited.</param>
        public override void OnTriggerExit(Collider other)
        {
            // The object may not be detected with a trigger.
            if (m_MaxTriggerObjectCount == 0) {
                return;
            }

            // The object has to use the correct mask.
            if (!MathUtility.InLayerMask(other.gameObject.layer, m_TriggerLayerMask)) {
                return;
            }

            for (int i = 0; i < m_DetectedTriggerObjectsCount; ++i) {
                if (other.gameObject == m_DetectedTriggerObjects[i]) {
                    m_DetectedTriggerObjects[i] = null;
                    // Ensure there is not a gap in the trigger object elements.
                    for (int j = i; j < m_DetectedTriggerObjectsCount - 1; ++j) {
                        m_DetectedTriggerObjects[j] = m_DetectedTriggerObjects[j + 1];
                    }
                    m_DetectedTriggerObjectsCount--;
                }
            }

            // If the ability was started by entering a trigger then it be stopped by leaving the trigger.
            if (m_TriggerStart && m_DetectedTriggerObjectsCount == 0) {
                if (IsActive) {
                    StopAbility();
                } else if (m_CharacterLocomotion.IsAbilityTypeActive<ItemEquipVerifier>()) {
                    // If the ability didn't get a chance to activate before leaving the trigger then ensure the item is reequipped.
                    var itemEquipVerifier = m_CharacterLocomotion.GetAbility<ItemEquipVerifier>();
                    itemEquipVerifier.TryToggleItem(this, false);
                }
            }
        }

        /// <summary>
        /// The ability has stopped running.
        /// </summary>
        /// <param name="force">Was the ability force stopped?</param>
        protected override void AbilityStopped(bool force)
        {
            m_TriggerStart = false;
            AbilityStopped(force, false);
        }

        /// <summary>
        /// The character has been destroyed.
        /// </summary>
        public override void OnDestroy()
        {
            base.OnDestroy();

            EventHandler.UnregisterEvent(m_GameObject, "OnAnimatorCrawlComplete", OnCrawlComplete);
        }
    }
}