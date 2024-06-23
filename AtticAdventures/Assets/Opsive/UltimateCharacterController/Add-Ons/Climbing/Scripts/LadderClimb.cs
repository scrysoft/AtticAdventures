/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.AddOns.Climbing
{
    using Opsive.Shared.Events;
    using Opsive.Shared.Game;
    using Opsive.Shared.Input;
    using Opsive.Shared.Utility;
#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
    using Opsive.UltimateCharacterController.AddOns.Agility;
#endif
    using Opsive.UltimateCharacterController.AddOns.Climbing.Objects;
    using Opsive.UltimateCharacterController.Character;
    using Opsive.UltimateCharacterController.Character.Abilities;
    using Opsive.UltimateCharacterController.Objects.CharacterAssist;
    using UnityEngine;

    /// <summary>
    /// The LadderClimb ability allows the character to move up and down a ladder. The character can mount/dismount on the ladder from either direction and can mount while in the air.
    /// </summary>
    [DefaultStartType(AbilityStartType.ButtonDown)]
    [DefaultStopType(AbilityStopType.ButtonToggle)]
    [DefaultInputName("Action")]
    [DefaultState("LadderClimb")]
    [DefaultAbilityIndex(501)]
    [DefaultAllowRotationalInput(false)]
    [DefaultUseGravity(AbilityBoolOverride.False)]
    [DefaultUseRootMotionPosition(AbilityBoolOverride.True)]
    [DefaultUseRootMotionRotation(AbilityBoolOverride.True)]
    [DefaultDetectHorizontalCollisions(AbilityBoolOverride.False)]
    [DefaultDetectVerticalCollisions(AbilityBoolOverride.False)]
    [DefaultEquippedSlots(0)]
    [Group("Climbing Pack")]
    public class LadderClimb : TraversalClimb
    {
        /// <summary>
        /// The movements that the ability can perform.
        /// </summary>
        [System.Flags]
        public enum AllowedMovement
        {
            InAirMount = 1,         // The character can mount while in the air.
            BottomDismount = 4,     // The character can dismount on the bottom.
            TopDismount = 8,        // The character can dismount on the top.
#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
            StartFromHang = 16,     // The character can start climbing from the Agility Pack Hang ability.
#endif
        }

        [Tooltip("The movements that the ability can perform.")]
        [SerializeField] protected AllowedMovement m_AllowedMovements = (AllowedMovement)(-1);
        [Tooltip("Specifies the speed that the character moves towards the ladder if mounting in the air.")]
        [SerializeField] protected float m_AirborneMoveTowardsSpeed = 0.2f;
        [Tooltip("Should the ability stop if the jump button is pressed?")]
        [SerializeField] protected bool m_StopClimbOnJump = true;
#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
        [Tooltip("The offset used to check for a ladder if the hang ability is active.")]
        [SerializeField] protected Vector3 m_StartHangOffset = new Vector3(1, 0, 0);
#endif

        public AllowedMovement AllowedMovements { get { return m_AllowedMovements; } set { m_AllowedMovements = value; } }
        public float AirborneMoveTowardsSpeed { get { return m_AirborneMoveTowardsSpeed; } set { m_AirborneMoveTowardsSpeed = value; } }
        public bool StopClimbOnJump { get { return m_StopClimbOnJump; } set { m_StopClimbOnJump = value; } }
#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
        public Vector3 StartHangOffset { get { return m_StartHangOffset; } set { m_StartHangOffset = value; } }
#endif

        /// <summary>
        /// Specifies the current state of the ability.
        /// </summary>
        private enum ClimbState
        {
            BottomMount,    // The character is mounting onto the ladder from the bottom.
            TopMount,       // The character is mounting onto the ladder from the top.
            AirMount,       // The character is mounting onto the ladder from the air.
            Climb,          // The character is climbing.
            BottomDismount, // The character is pulling themself up to end the ability.
            TopDismount,    // The character is pulling themself up to end the ability.
            HangStart,      // The character is starting to climb from the Hang ability.
            None
        }

        private MoveTowardsLocation[] m_MoveTowardsLocation = new MoveTowardsLocation[1];
        private ClimbState m_ClimbState;
        private Ladder m_Ladder;

        private UltimateCharacterLocomotionHandler m_Handler;
        private Jump m_Jump;
        private ActiveInputEvent m_JumpInput;
        private Vector3 m_AirborneMountPosition;

#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
        private Hang m_Hang;
        private LedgeStrafe m_LedgeStrafe;
#endif
        public override int AbilityIntData {
            get {
                return (int)m_ClimbState;
            }
        }

        /// <summary>
        /// Initialize the default values.
        /// </summary>
        public override void Awake()
        {
            base.Awake();

            if (m_ObjectDetection == ObjectDetectionMode.Trigger) {
                Debug.LogError("Error: The Ladder Climb ObjectDetectionMode should not be set to Trigger.");
                Enabled = false;
                return;
            }

            m_Handler = m_GameObject.GetCachedComponent<UltimateCharacterLocomotionHandler>();
            m_Jump = m_CharacterLocomotion.GetAbility<Jump>();
#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
            m_Hang = m_CharacterLocomotion.GetAbility<Hang>();
            m_LedgeStrafe = m_CharacterLocomotion.GetAbility<LedgeStrafe>();
#endif
        }

        /// <summary>
        /// Called when the ablity is tried to be started. If false is returned then the ability will not be started.
        /// </summary>
        /// <returns>True if the ability can be started.</returns>
        public override bool CanStartAbility()
        {
            // The character may be trying to start from the top of the ladder. Cast a ray towards the character just below the front of the character.
            // If an object is hit then a ladder is beneath the character.
            if (m_CharacterLocomotion.Grounded) {
                var position = m_CharacterLocomotion.TransformPoint(0, -0.5f, m_CastDistance);
                if (Physics.Raycast(position, -(m_CharacterLocomotion.Rotation * Vector3.forward), out m_RaycastResult, m_CastDistance, m_DetectLayers, QueryTriggerInteraction.Ignore)) {
                    // The object must be a ladder.
                    var hitObject = m_RaycastResult.collider.gameObject;
                    if (!ValidateObject(hitObject, m_RaycastResult)) {
                        return false;
                    }

                    var raycastNormal = Vector3.ProjectOnPlane(m_RaycastResult.normal, m_CharacterLocomotion.Up).normalized;
                    if (raycastNormal.sqrMagnitude == 0) {
                        return false;
                    }

                    // The character can climb on the ladder.
                    DetectedObject = m_RaycastResult.collider.gameObject;
                    m_ClimbState = ClimbState.TopMount;
                    return true;
                }
            } else if ((m_AllowedMovements & AllowedMovement.InAirMount) == 0) {
                return false;
            }
            
            if (base.CanStartAbility()) {
                m_ClimbState = ClimbState.BottomMount;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Validates the object to ensure it is valid for the current ability.
        /// </summary>
        /// <param name="obj">The object being validated.</param>
        /// <param name="raycastHit">The raycast hit of the detected object. Will be null for trigger detections.</param>
        /// <returns>True if the object is valid. The object may not be valid if it doesn't have an ability-specific component attached.</returns>
        protected override bool ValidateObject(GameObject obj, RaycastHit? raycastHit)
        {
            if ((m_Ladder = obj.GetCachedComponent<Ladder>()) == null) {
                return false;
            }

            return m_Ladder.CanMount(m_CharacterLocomotion) && base.ValidateObject(obj, raycastHit);
        }

        /// <summary>
        /// Returns the possible MoveTowardsLocations that the character can move towards.
        /// </summary>
        /// <returns>The possible MoveTowardsLocations that the character can move towards.</returns>
        public override MoveTowardsLocation[] GetMoveTowardsLocations()
        {
            if (m_Ladder == null || !m_CharacterLocomotion.Grounded) { // There isn't a MoveTowardsLocation when the character is in the air.
                return null;
            }

            m_MoveTowardsLocation[0] = m_ClimbState == ClimbState.BottomMount ? m_Ladder.BottomMountMoveTowardsLocation : m_Ladder.TopMountMoveTowardsLocation;

            // The y location will always be the character's current vertical location.
            var offset = m_MoveTowardsLocation[0].Offset;
            offset.y = m_MoveTowardsLocation[0].transform.InverseTransformPoint(m_CharacterLocomotion.Position).y;
            m_MoveTowardsLocation[0].Offset = offset;

            return m_MoveTowardsLocation;
        }

        /// <summary>
        /// The ability has started.
        /// </summary>
        protected override void AbilityStarted()
        {
            base.AbilityStarted();

            if (!m_CharacterLocomotion.Grounded && m_ClimbState != ClimbState.HangStart) {
                m_ClimbState = ClimbState.AirMount;
                m_AirborneMountPosition = m_Ladder.GetAirborneMountPosition(m_CharacterLocomotion);
                m_CharacterLocomotion.ForceRootMotionRotation = false;
                m_CharacterLocomotion.ResetRotationPosition();
            }

            // The jump button can stop the ability.
            if (m_StopClimbOnJump && m_Handler != null && m_Jump != null && m_Jump.InputNames.Length > 0 && m_Jump.StartType == AbilityStartType.ButtonDown) {
                m_JumpInput = GenericObjectPool.Get<ActiveInputEvent>();
                m_JumpInput.Initialize(ActiveInputEvent.Type.ButtonDown, m_Jump.InputNames[0], "OnJumpInput");
                m_Handler.RegisterInputEvent(m_JumpInput);
                EventHandler.RegisterEvent(m_GameObject, "OnJumpInput", OnJumpInput);
            }

            EventHandler.RegisterEvent(m_GameObject, "OnAnimatorLadderClimbStartInPosition", OnLadderClimbStartInPosition);
            EventHandler.RegisterEvent(m_GameObject, "OnAnimatorLadderClimbComplete", OnLadderClimbComplete);
            EventHandler.ExecuteEvent(m_GameObject, "OnCharacterForceIndependentLook", true);
        }

        /// <summary>
        /// The ladder climb ability is in starting position.
        /// </summary>
        private void OnLadderClimbStartInPosition()
        {
            // After the character is in position reset the rotation and position to prevent any external jump forces from affecting the character's move direction.
            m_CharacterLocomotion.ResetRotationPosition();

            m_ClimbState = ClimbState.Climb;
            m_CharacterLocomotion.ForceRootMotionRotation = false;
            SetAbilityIntDataParameter(AbilityIntData);
        }

        /// <summary>
        /// Updates the climb values.
        /// </summary>
        public override void Update()
        {
            base.Update();

            if (m_ClimbState != ClimbState.Climb) {
                m_CharacterLocomotion.InputVector = Vector2.zero;
                return;
            }

            // Ladders do not allow horizontal movement.
            var inputVector = m_CharacterLocomotion.RawInputVector; // Use the raw value so the value is instant.
#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
            if (Mathf.Abs(inputVector.x) > 0) {
                // The Hang ability may be able to start.
                if (m_Hang != null && m_Hang.TryStartFromClimb(this)) {
                    return;
                }
            }
#endif
            inputVector.x = 0;

            // The character may be able to dismount.
            if (inputVector.y > 0) {
                var localDismountLocation = m_CharacterLocomotion.InverseTransformPoint(m_Ladder.TopDismountLocation);
                if (localDismountLocation.y <= 0) {
#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
                    // The Hang ability may be able to start.
                    if (m_LedgeStrafe != null && m_LedgeStrafe.CanStartFromClimb()) {
                        m_ClimbState = ClimbState.TopDismount;
                        SetAbilityIntDataParameter(AbilityIntData);
                        return;
                    }
#endif
                    if ((m_AllowedMovements & AllowedMovement.TopDismount) != 0) {
                        m_ClimbState = ClimbState.TopDismount;
                        SetAbilityIntDataParameter(AbilityIntData);
                    } else {
                        // Prevent the character from moving up anymore if they are at the end of the ladder and cannot dismount.
                        inputVector.y = 0;
                    }
                }
            } else if (inputVector.y < 0) {
                var localDismountLocation = m_CharacterLocomotion.InverseTransformPoint(m_Ladder.BottomDismountLocation);
                if (localDismountLocation.y >= 0) {
                    if ((m_AllowedMovements & AllowedMovement.BottomDismount) != 0) {
                        m_ClimbState = ClimbState.BottomDismount;
                        SetAbilityIntDataParameter(AbilityIntData);
                    } else {
                        // Prevent the character from moving down anymore if they are at the end of the ladder and cannot dismount.
                        inputVector.y = 0;
                    }
                }
            }
            m_CharacterLocomotion.InputVector = inputVector;
        }

        /// <summary>
        /// Update the controller's rotation values.
        /// </summary>
        public override void UpdateRotation()
        {
            base.UpdateRotation();

            if (m_ClimbState == ClimbState.TopMount || m_ClimbState == ClimbState.TopDismount || m_ClimbState == ClimbState.HangStart) {
                return;
            }

            // The character should always face the climb object.
            if (m_MoveWithObject) {
                m_CharacterLocomotion.DesiredRotation = Quaternion.identity;
            }

            var targetRotation = Quaternion.LookRotation(m_Ladder.GetLookDirection(m_CharacterLocomotion), m_Ladder.transform.up);
            m_CharacterLocomotion.DesiredRotation = Quaternion.Inverse(m_CharacterLocomotion.Rotation) * targetRotation;
        }

        /// <summary>
        /// Verify the position values. Called immediately before the position is applied.
        /// </summary>
        public override void ApplyPosition()
        {
            base.ApplyPosition();

            // The character should move to the ladder rung if they are mounting from a jump.
            if (m_ClimbState == ClimbState.AirMount){
                var position = m_CharacterLocomotion.Position;
                var targetPosition = Vector3.MoveTowards(position, m_AirborneMountPosition, m_AirborneMoveTowardsSpeed * m_CharacterLocomotion.TimeScale);
                m_CharacterLocomotion.DesiredMovement = targetPosition - position;
                // The character has arrived on a rung.
                if (m_CharacterLocomotion.DesiredMovement.sqrMagnitude < m_CharacterLocomotion.ColliderSpacing) {
                    m_ClimbState = ClimbState.Climb;
                    SetAbilityIntDataParameter(AbilityIntData);
                }
            }
        }

        /// <summary>
        /// The jump ability input is being triggered.
        /// </summary>
        private void OnJumpInput()
        {
            if (!m_Jump.Enabled || m_ClimbState != ClimbState.Climb) {
                return;
            }

            OnLadderClimbComplete();
        }

        /// <summary>
        /// The ladder climb ability has completed - stop the ability.
        /// </summary>
        private void OnLadderClimbComplete()
        {
            m_ClimbState = ClimbState.None;
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

            return m_ClimbState == ClimbState.Climb || m_ClimbState == ClimbState.None;
        }

        /// <summary>
        /// The ability has stopped running.
        /// </summary>
        /// <param name="force">Was the ability force stopped?</param>
        protected override void AbilityStopped(bool force)
        {
            base.AbilityStopped(force);

            m_ClimbState = ClimbState.BottomMount; // Reset for next start.
            
            if (m_JumpInput != null) {
                m_Handler.UnregisterInputEvent(m_JumpInput);
                GenericObjectPool.Return(m_JumpInput);
                EventHandler.UnregisterEvent(m_GameObject, "OnJumpInput", OnJumpInput);
            }
            EventHandler.UnregisterEvent(m_GameObject, "OnAnimatorLadderClimbStartInPosition", OnLadderClimbStartInPosition);
            EventHandler.UnregisterEvent(m_GameObject, "OnAnimatorLadderClimbComplete", OnLadderClimbComplete);
        }

#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
        /// <summary>
        /// Tries to start the climbing ability from the hang ablity.
        /// </summary>
        /// <param name="hang">A reference to the active hang ability.</param>
        /// <returns>True if the ability was started.</returns>
        public override bool TryStartFromHang(Hang hang)
        {
            if (m_CharacterLocomotion.RawInputVector.x == 0 || (m_AllowedMovements & AllowedMovement.StartFromHang) == 0) {
                return false;
            }

            // The ladder must exist to the right or left of the character.
            var offset = m_StartHangOffset;
            offset.x = m_CharacterLocomotion.RawInputVector.x > 0 ? m_StartHangOffset.x : -m_StartHangOffset.x;
#if UNITY_EDITOR
            Debug.DrawRay(m_CharacterLocomotion.TransformPoint(offset), (m_CastDistance + Mathf.Abs(offset.z)) * 2 * (m_CharacterLocomotion.Rotation * Vector3.forward), Color.red);
#endif
            if (!Physics.Raycast(m_CharacterLocomotion.TransformPoint(offset), m_CharacterLocomotion.Rotation * Vector3.forward, out m_RaycastResult, (m_CastDistance + Mathf.Abs(offset.z)) * 2, m_DetectLayers, QueryTriggerInteraction.Ignore) ||
                !ValidateObject(m_RaycastResult.collider.gameObject, m_RaycastResult)) {
                return false;
            }

            // The hit object is valid.
            DetectedObject = m_RaycastResult.collider.gameObject;
            m_CharacterLocomotion.InputVector = m_CharacterLocomotion.RawInputVector;

            // Stop the hang ability and start the climb ability.
            hang.StopAbility();
            m_ClimbState = ClimbState.HangStart;
            m_CharacterLocomotion.TryStartAbility(this, true, true);
            return true;
        }
#else
        /// <summary>
        /// Empty method to allow for compilation.
        /// </summary>
        /// <param name="hang">A reference to the active hang ability.</param>
        /// <returns>True if the ability was started.</returns>
        public override bool TryStartFromHang(Ability hang)
        {
            return false;
        }
#endif
    }
}