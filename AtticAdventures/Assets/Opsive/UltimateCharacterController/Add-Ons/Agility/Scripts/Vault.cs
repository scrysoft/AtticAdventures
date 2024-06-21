/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.AddOns.Agility
{
    using Opsive.Shared.Events;
    using Opsive.UltimateCharacterController.Character.Abilities;
    using Opsive.UltimateCharacterController.Objects.CharacterAssist;
    using UnityEngine;

    /// <summary>
    /// The Vault ability will allow the character to traverse over a vertical object.
    /// </summary>
    [DefaultState("Vault")]
    [DefaultAbilityIndex(105)]
    [DefaultStartType(AbilityStartType.ButtonDown)]
    [DefaultAllowRotationalInput(false)]
    [DefaultAllowPositionalInput(false)]
    [DefaultInputName("Action", 0)]
    [DefaultInputName("Jump", 1)]
    [DefaultUseRootMotionPosition(AbilityBoolOverride.True)]
    [DefaultUseLookDirection(false)]
    [Opsive.Shared.Utility.Group("Agility Pack")]
    public class Vault : DetectObjectAbilityBase
    {
        [Tooltip("The maximum height of the object that the character can climb over.")]
        [SerializeField] protected float m_MaxHeight = 2f;
        [Tooltip("Adds to the vault object's offset.z value to allow the ability to play sooner when the character is running versus walking. The x value represents the " +
                 "character's z velocity while the y represents the amount to add the Ability Start Location's depth offset value by.")]
        [SerializeField] protected AnimationCurve m_StartLocationDepthOffset = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(4, 0.35f), new Keyframe(8, 0.45f) });

        public float MaxHeight { get { return m_MaxHeight; } set { m_MaxHeight = value; } }
        public AnimationCurve StartLocationDepthMultiplier { get { return m_StartLocationDepthOffset; } set { m_StartLocationDepthOffset = value; } }

        public override float AbilityFloatData { get { return m_StartVelocity; } }
        public override int AbilityIntData { get { return Mathf.RoundToInt(m_Height * 1000); } } // AbilityFloatData is used by the velocity. Multiply by a large value and convert to an int.

        private float m_StartVelocity = -1;
        private MoveTowardsLocation[] m_StartLocations;
        private float m_Height;

        /// <summary>
        /// Initialize the default values.
        /// </summary>
        public override void Awake()
        {
            base.Awake();

            EventHandler.RegisterEvent(m_GameObject, "OnAnimatorVaultComplete", OnComplete);
        }

        /// <summary>
        /// Can the ability be started?
        /// </summary>
        /// <returns>True if the ability can be started.</returns>
        public override bool CanStartAbility()
        {
            // The character has to be on the ground.
            if (!m_CharacterLocomotion.Grounded) {
                return false;
            }

            // An attribute may prevent the ability from starting.
            if (!base.CanStartAbility()) {
                return false;
            }

            // The top of the detected object must be beneath the max height.
            var position = m_Transform.InverseTransformPoint(m_RaycastResult.point);
            position.y = 0;
            position = m_Transform.TransformPoint(position);
            RaycastHit hit;
            if (!Physics.Raycast(position + m_CharacterLocomotion.Up * m_MaxHeight - m_RaycastResult.normal * 0.1f, -m_CharacterLocomotion.Up, out hit,
                m_MaxHeight + 0.1f, m_CharacterLayerManager.SolidObjectLayers, QueryTriggerInteraction.Ignore)) {
                return false;
            }

            m_Height = m_Transform.InverseTransformPoint(hit.point).y;

            return true;
        }

        /// <summary>
        /// The ability will start - perform any initialization before starting.
        /// </summary>
        /// <returns>True if the ability should start.</returns>
        public override bool AbilityWillStart()
        {
            if (m_StartVelocity != -1) {
                return true;
            }

            m_StartVelocity = Mathf.Max(m_CharacterLocomotion.LocalVelocity.z, 0);
            return base.AbilityWillStart();
        }

        /// <summary>
        /// Returns the possible MoveTowardsLocations that the character can move towards.
        /// </summary>
        /// <returns>The possible MoveTowardsLocations that the character can move towards.</returns>
        public override MoveTowardsLocation[] GetMoveTowardsLocations()
        {
            m_StartLocations = m_DetectedObject.GetComponentsInChildren<MoveTowardsLocation>();
            if (m_StartLocations != null && m_StartLocations.Length > 0) {
                // The character should start vaulting depending on the speed that they are moving at. If the character is running the vault animation should start sooner than
                // if the character is walking.
                for (int i = 0; i < m_StartLocations.Length; ++i) {
                    var offset = m_StartLocations[i].Offset;
                    offset.z = m_StartLocations[i].StartOffset.z + m_StartLocationDepthOffset.Evaluate(Mathf.Min(m_CharacterLocomotion.LocalVelocity.z, 
                        (m_DetectedObject.transform.position - m_Transform.position).magnitude));
                    m_StartLocations[i].Offset = offset;
                }
            }

            return m_StartLocations;
        }

        /// <summary>
        /// Called when another ability is attempting to start and the current ability is active.
        /// Returns true or false depending on if the new ability should be blocked from starting.
        /// </summary>
        /// <param name="startingAbility">The ability that is starting.</param>
        /// <returns>True if the ability should be blocked.</returns>
        public override bool ShouldBlockAbilityStart(Ability startingAbility)
        {
            return startingAbility is Character.Abilities.Items.ItemAbility;
        }

        /// <summary>
        /// Called when the current ability is attempting to start and another ability is active.
        /// Returns true or false depending on if the active ability should be stopped.
        /// </summary>
        /// <param name="activeAbility">The ability that is currently active.</param>
        /// <returns>True if the ability should be stopped.</returns>
        public override bool ShouldStopActiveAbility(Ability activeAbility)
        {
            return activeAbility is Character.Abilities.Items.ItemAbility;
        }

        /// <summary>
        /// The ability has started.
        /// </summary>
        protected override void AbilityStarted()
        {
            base.AbilityStarted();

            m_CharacterLocomotion.AllowHorizontalCollisionDetection = false;
            m_CharacterLocomotion.AllowVerticalCollisionDetection = false;
        }

        /// <summary>
        /// Updates the ability.
        /// </summary>
        public override void Update()
        {
            // Do not call the base method to prevent an attribute from stopping the vault.
        }

        /// <summary>
        /// The vault animation has completed.
        /// </summary>
        private void OnComplete()
        {
            m_CharacterLocomotion.AllowHorizontalCollisionDetection = true;
            m_CharacterLocomotion.AllowVerticalCollisionDetection = true;
            StopAbility();
        }

        /// <summary>
        /// The ability has stopped running.
        /// </summary>
        /// <param name="force">Was the ability force stopped?</param>
        protected override void AbilityStopped(bool force)
        {
            base.AbilityStopped(force);

            if (m_StartLocations != null && m_StartLocations.Length > 0) {
                // Reset the offset after it has been modified.
                for (int i = 0; i < m_StartLocations.Length; ++i) {
                    var offset = m_StartLocations[i].Offset;
                    offset.z = m_StartLocations[i].StartOffset.z;
                    m_StartLocations[i].Offset = offset;
                }
            }
            m_StartVelocity = -1;
        }

        /// <summary>
        /// The character has been destroyed.
        /// </summary>
        public override void OnDestroy()
        {
            base.OnDestroy();

            EventHandler.UnregisterEvent(m_GameObject, "OnAnimatorVaultComplete", OnComplete);
        }
    }
}