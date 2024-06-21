/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.AddOns.Agility
{
    using Opsive.Shared.Events;
    using Opsive.Shared.Game;
    using Opsive.UltimateCharacterController.Camera;
    using Opsive.UltimateCharacterController.Character.Abilities;
    using UnityEngine;

    /// <summary>
    /// The Roll ability allows the character to play a rolling animation. A rolling animation can also play if the character falls from a tall object. While in first person view
    /// the camera can roll with the character, stay in an upright rotation, or switch to a third person perspective.
    /// </summary>
    [DefaultState("Roll")]
    [DefaultAbilityIndex(102)]
    [DefaultStartType(AbilityStartType.ButtonDown)]
    [DefaultInputName("Action")]
    [DefaultUseRootMotionPosition(AbilityBoolOverride.True)]
    [DefaultAllowPositionalInput(false)]
    [DefaultAllowRotationalInput(false)]
    [Opsive.Shared.Utility.Group("Agility Pack")]
    public class Roll : Ability
    {
        /// <summary>
        /// Specifies the type of roll that should play.
        /// </summary>
        private enum RollType
        {
            Left,       // Left roll on the ground.
            Right,      // Right roll on the ground.
            Forward,    // Forward roll on the ground.
            Land        // The roll has been started from a large fall.
        }

        [Tooltip("Specifies the cast distance to ensure there is enough space for the character to roll.")]
        [SerializeField] protected float m_StartCastDistance = 2;
        [Tooltip("Start the roll ability if the character falls from a height greater than the specified value. Set to -1 to disable the falling roll.")]
        [SerializeField] protected float m_RollFallHeight = 3.5f;
        [Tooltip("Should the ability check against the slope before rolling?")]
        [SerializeField] protected bool m_CheckForSlope = true;
#if FIRST_PERSON_CONTROLLER && THIRD_PERSON_CONTROLLER
        [Tooltip("Should the roll ability switch to a third person view if the character is in a first person view?")]
        [SerializeField] protected bool m_SwitchToThirdPerson;
#endif

        public float StartCastDistance { get { return m_StartCastDistance; } set { m_StartCastDistance = value; } }
        public float RollFallHeight { get { return m_RollFallHeight; } set { m_RollFallHeight = value; } }
        public bool CheckForSlope { get { return m_CheckForSlope; } set { m_CheckForSlope = value; } }
#if FIRST_PERSON_CONTROLLER && THIRD_PERSON_CONTROLLER
        public bool SwitchToThirdPerson { get { return m_SwitchToThirdPerson; } set { m_SwitchToThirdPerson = value; } }
#endif

        private bool m_LandingRoll;
        private RollType m_RollType = RollType.Forward;
        private bool m_InFirstPersonPerspective;
        private RaycastHit m_RaycastHit;

        public override int AbilityIntData { get { return (int)m_RollType; } }

        /// <summary>
        /// Initialize the default values.
        /// </summary>
        public override void Awake()
        {
            base.Awake();
            
            EventHandler.RegisterEvent<float>(m_GameObject, "OnCharacterLand", OnCharacterLand);
            EventHandler.RegisterEvent(m_GameObject, "OnAnimatorRollComplete", OnComplete);
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

            // The character can't roll on steep slopes.
            if (m_CheckForSlope) {
                var slope = Vector3.Angle(m_CharacterLocomotion.Up, m_CharacterLocomotion.GroundedRaycastHit.normal);
                if (slope > m_CharacterLocomotion.SlopeLimit) {
                    return false;
                }
            }

            // Don't roll if an object would get in the way.
            if (m_StartCastDistance > 0) {
                var direction = new Vector3(m_CharacterLocomotion.InputVector.x, 0, m_CharacterLocomotion.InputVector.y);
                // If the direction is zero then the character will roll foward.
                if (direction.sqrMagnitude == 0) {
                    direction.z = 1;
                }
                if (m_CharacterLocomotion.SingleCast(m_Transform.TransformDirection(direction).normalized, Vector3.zero, m_StartCastDistance, m_CharacterLayerManager.SolidObjectLayers, ref m_RaycastHit)) {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// The ability has started.
        /// </summary>
        protected override void AbilityStarted()
        {
            base.AbilityStarted();

            if (m_LandingRoll) {
                m_RollType = RollType.Land;
            } else {
                // The roll is based on the character's direction. Horizontal roll has priority over forward.
                var horizontalRoll = m_CharacterLocomotion.InputVector.sqrMagnitude > 0 && Mathf.Abs(m_CharacterLocomotion.InputVector.x) >= Mathf.Abs(m_CharacterLocomotion.InputVector.y);
                if (horizontalRoll) {
                    if (m_CharacterLocomotion.InputVector.x < 0) {
                        m_RollType = RollType.Left;
                    } else {
                        m_RollType = RollType.Right;
                    }
                } else {
                    m_RollType = RollType.Forward;
                }
            }

#if FIRST_PERSON_CONTROLLER && THIRD_PERSON_CONTROLLER
            // Optionally switch to a third person perspective if the character is in a first person perspective.
            if (m_SwitchToThirdPerson) {
                m_InFirstPersonPerspective = m_CharacterLocomotion.FirstPersonPerspective;
                if (m_InFirstPersonPerspective) {
                    var camera = Opsive.Shared.Camera.CameraUtility.FindCamera(m_GameObject);
                    if (camera != null) {
                        var cameraController = camera.gameObject.GetCachedComponent<CameraController>();
                        if (cameraController != null) {
                            cameraController.SetPerspective(false);
                        }
                    }
                }
            }
#endif
        }

        /// <summary>
        /// Called when another ability is attempting to start and the current ability is active.
        /// Returns true or false depending on if the new ability should be blocked from starting.
        /// </summary>
        /// <param name="startingAbility">The ability that is starting.</param>
        /// <returns>True if the ability should be blocked.</returns>
        public override bool ShouldBlockAbilityStart(Ability startingAbility)
        {
            // Lots of concurrent abilities should be blocked from starting when roll is active.
            return startingAbility is HeightChange || startingAbility is StoredInputAbilityBase || startingAbility is Character.Abilities.Items.ItemAbility;
        }
        
        /// <summary>
        /// Updates the ability.
        /// </summary>
        public override void Update()
        {
            // Do not call the base method to prevent an attribute from stopping the roll.
        }

        /// <summary>
        /// The character has landed after falling a spcified amount.
        /// </summary>
        /// <param name="fallHeight"></param>
        private void OnCharacterLand(float fallHeight)
        {
            // The ability can start automatically if the character lands from a large fall height
            if (m_RollFallHeight == -1 || fallHeight < m_RollFallHeight) {
                return;
            }

            m_LandingRoll = true;
            StartAbility();
            // The ability may have been blocked from starting - reset the bool to prevent a landing roll from playing when the input is pressed.
            m_LandingRoll = false;
        }

        /// <summary>
        /// The roll animation has completed.
        /// </summary>
        private void OnComplete()
        {
            StopAbility();
        }

#if FIRST_PERSON_CONTROLLER && THIRD_PERSON_CONTROLLER
        /// <summary>
        /// The ability has stopped running.
        /// </summary>
        /// <param name="force">Was the ability force stopped?</param>
        protected override void AbilityStopped(bool force)
        {
            base.AbilityStopped(force);

            // If the character started in a first person perspective it should be switched back when the ability ends.
            if (m_SwitchToThirdPerson && m_InFirstPersonPerspective) {
                var camera = Opsive.Shared.Camera.CameraUtility.FindCamera(m_GameObject);
                if (camera != null) {
                    var cameraController = camera.gameObject.GetCachedComponent<CameraController>();
                    if (cameraController != null) {
                        cameraController.SetPerspective(true);
                    }
                }
            }
        }
#endif

        /// <summary>
        /// The character has been destroyed.
        /// </summary>
        public override void OnDestroy()
        {
            base.OnDestroy();

            EventHandler.UnregisterEvent<float>(m_GameObject, "OnCharacterLand", OnCharacterLand);
            EventHandler.UnregisterEvent(m_GameObject, "OnAnimatorRollComplete", OnComplete);
        }
    }
}