/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.AddOns.Agility
{
    using Opsive.Shared.Events;
    using Opsive.Shared.Game;
    using Opsive.UltimateCharacterController.Character;
    using Opsive.UltimateCharacterController.Character.Abilities;
    using Opsive.UltimateCharacterController.Utility;
    using UnityEngine;

    /// <summary>
    /// The Ledge Strafe ability will play a strafing animation while the character is on a ledge. This ledge should be narrow to prevent
    /// the character from being able to walk normally across it. A wall must exist on one side of the ledge that the character can lean against.
    /// </summary>
    [DefaultState("LedgeStrafe")]
    [DefaultAbilityIndex(106)]
    [DefaultDetectHorizontalCollisions(AbilityBoolOverride.False)]
    [DefaultUseRootMotionPosition(AbilityBoolOverride.True)]
    [DefaultStopType(AbilityStopType.Manual)]
    [DefaultEquippedSlots(0)]
    [Opsive.Shared.Utility.Group("Agility Pack")]
    public class LedgeStrafe : DetectGroundAbilityBase
    {
        [Tooltip("The distance to detect a wall before the ability can start.")]
        [SerializeField] protected float m_DetectWallDistance = 0.5f;
        [Tooltip("The distance to check to the left and right of the initial wall position to determine if location is a valid starting position.")]
        [SerializeField] protected float m_HorizontalStartOffset = 0.2f;
        [Tooltip("The distance that the character should stand away from the wall.")]
        [SerializeField] protected float m_WallOffset = 0.15f;
        [Tooltip("The distance away from the wall that the gap exists.")]
        [SerializeField] protected float m_GapOffset = 1f;
        [Tooltip("The minimum height of the ledge.")]
        [SerializeField] protected float m_MinimumLedgeHeight = 1f;
        [Tooltip("The radius of the cast when determining if a gap exists and the ability is determining if it can start.")]
        [SerializeField] protected float m_CanStartGapOffsetCastRadius = 0.4f;
        [Tooltip("The radius of the cast when determining if a gap exists and the ability is active.")]
        [SerializeField] protected float m_ActiveGapOffsetCastRadius = 0.1f;
        [Tooltip("The speed that the character should move towards the wall when starting.")]
        [SerializeField] protected float m_StartMoveTowardsWallSpeed = 0.05f;
        [Tooltip("The amount of force to apply when the ability stops from input or the jump ability. This will push the character away from the wall.")]
        [SerializeField] protected float m_StopForce = 0.1f;
        [Tooltip("The number of frames to apply the force in when the ability stops from input or the jump ability. This will push the character away from the wall.")]
        [SerializeField] protected int m_StopForceFrames = 10;
#if ULTIMATE_CHARACTER_CONTROLLER_CLIMBING
        [Tooltip("Can the ability start from an active Climb aiblity?")]
        [SerializeField] protected bool m_StartFromClimb = true;
#endif

        public float DetectWallDistance { get { return m_DetectWallDistance; } set { m_DetectWallDistance = value; } }
        public float HorizontalStartOffset { get { return m_HorizontalStartOffset; } set { m_HorizontalStartOffset = value; } }
        public float WallOffset { get { return m_WallOffset; } set { m_WallOffset = value; } }
        public float GapOffset { get { return m_GapOffset; } set { m_GapOffset = value; } }
        public float MinimumLedgeHeight { get { return m_MinimumLedgeHeight; } set { m_MinimumLedgeHeight = value; } }
        public float CanStartGapOffsetCastRadius { get { return m_CanStartGapOffsetCastRadius; } set { m_CanStartGapOffsetCastRadius = value; } }
        public float ActiveGapOffsetCastRadius { get { return m_ActiveGapOffsetCastRadius; } set { m_ActiveGapOffsetCastRadius = value; } }
        public float StartMoveTowardsWallSpeed { get { return m_StartMoveTowardsWallSpeed; } set { m_StartMoveTowardsWallSpeed = value; } }
        public float StopForce { get { return m_StopForce; } set { m_StopForce = value; } }
        public int StopForceFrames { get { return m_StopForceFrames; } set { m_StopForceFrames = value; } }
#if ULTIMATE_CHARACTER_CONTROLLER_CLIMBING
        public bool StartFromClimb { get { return m_StartFromClimb; } set { m_StartFromClimb = value; } }
#endif

        private ILookSource m_LookSource;
        private bool m_InPosition;
        private bool m_InRotation;
        private RaycastHit m_WallRaycastResult;
        private RaycastHit m_RaycastHit;
        private int m_StopFrame = -1;

        public override bool ImmediateStartItemVerifier { get { return true; } }

        /// <summary>
        /// Initialize the default values.
        /// </summary>
        public override void Awake()
        {
            base.Awake();

            EventHandler.RegisterEvent<ILookSource>(m_GameObject, "OnCharacterAttachLookSource", OnAttachLookSource);
            EventHandler.RegisterEvent<Ability, bool>(m_GameObject, "OnCharacterAbilityActive", OnAbilityActive);
        }

        /// <summary>
        /// A new ILookSource object has been attached to the character.
        /// </summary>
        /// <param name="lookSource">The ILookSource object attached to the character.</param>
        private void OnAttachLookSource(ILookSource lookSource)
        {
            m_LookSource = lookSource;
        }

        /// <summary>
        /// Can the ability be started?
        /// </summary>
        /// <returns>True if the ability can be started.</returns>
        public override bool CanStartAbility()
        {
            // Prevent the ability from starting if it just stopped. With root motion the character may move slightly backwards when just starting to move forward.
            if (m_StopFrame + 2 >= Time.frameCount) {
                return false;
            }

            if (m_CharacterLocomotion.IsAbilityTypeActive<Jump>() || m_CharacterLocomotion.IsAbilityTypeActive<Fall>()) {
                return false;
            }

            if (m_CharacterLocomotion.ItemEquipVerifierAbility != null && m_CharacterLocomotion.ItemEquipVerifierAbility.IsActive) {
                if (m_CharacterLocomotion.ItemEquipVerifierAbility.StartingAbility is Hang) {
                    return false;
                }
            }

            // Ensure the character is over a valid ground object.
            if (!base.CanStartAbility()) {
                return false;
            }

            // Detect the wall that the character will strafe against.
            var nearWall = false;
            for (int i = 0; i < 4; ++i) {
                // Check for a wall against the four directions 
                if (m_CharacterLocomotion.SingleCast(m_CharacterLocomotion.Rotation * Quaternion.AngleAxis(i * 90, m_CharacterLocomotion.Up) * Vector3.forward, 
                    m_CharacterLocomotion.Up * m_CharacterLocomotion.Height / 4, m_DetectWallDistance, m_CharacterLayerManager.IgnoreInvisibleCharacterLayers, ref m_WallRaycastResult)) {

                    // The ground object beneath the wall raycast point must be the same ground collider as what the character is on.
                    RaycastHit groundRaycastHit;
                    if (Physics.Raycast(MathUtility.TransformPoint(m_WallRaycastResult.point,
                                                                Quaternion.LookRotation(m_WallRaycastResult.normal, m_CharacterLocomotion.Up),
                                                                new Vector3(0, 0, m_WallOffset / 2)), -m_CharacterLocomotion.Up, out groundRaycastHit,
                                                                Mathf.Infinity, m_CharacterLayerManager.IgnoreInvisibleCharacterLayers, QueryTriggerInteraction.Ignore)) {
                        // If the ground collider matches then the object is valid.
                        if (groundRaycastHit.collider != m_GroundCollider) {
                            continue;
                        }
                    }

                    // A wall exists. Detect if a wall exists to the left and right of the current detected object. This will determine if the character should move inwards to prevent
                    // the character from standing on the edge of the ledge.
                    RaycastHit leftRaycastHit, rightRaycastHit;
                    var leftHit = GroundObjectOffsetExists(-m_HorizontalStartOffset, out leftRaycastHit);
                    var rightHit = GroundObjectOffsetExists(m_HorizontalStartOffset, out rightRaycastHit);
                    // If a left and right object exists then the wall raycast result is in the middle. If only one side exists then move the character to that side.
                    if (leftHit && !rightHit) {
                        m_WallRaycastResult = leftRaycastHit;
                    } else if (!leftHit && rightHit) {
                        m_WallRaycastResult = rightRaycastHit;
                    }
                    nearWall = true;
                    break;
                }
            }

            if (!nearWall) {
                return false;
            }

            // The character is near a wall. Ensure there are no objects on the opposite side of the ledge.
            if (!IsNearLedgeGap()) {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Detects if the ground object exists at the specified horizontal offset.
        /// </summary>
        /// <param name="horizontalOffset">The offset to check for a ground object.</param>
        /// <param name="raycastHit">The results from the wall raycast.</param>
        /// <returns>True if the ground object exists.</returns>
        private bool GroundObjectOffsetExists(float horizontalOffset, out RaycastHit raycastHit)
        {
            // First ensure a wall exists at the specified horizontal offset.
            if (Physics.Raycast(MathUtility.TransformPoint(m_WallRaycastResult.point,
                                                Quaternion.LookRotation(m_WallRaycastResult.normal, m_CharacterLocomotion.Up),
                                                new Vector3(horizontalOffset, 0, m_DetectWallDistance / 2)), -m_WallRaycastResult.normal, out raycastHit, 
                                                m_DetectWallDistance, m_CharacterLayerManager.IgnoreInvisibleCharacterLayers, QueryTriggerInteraction.Ignore)) {
                // The ground should now be detected.
                RaycastHit groundRaycastHit;
                if (Physics.Raycast(MathUtility.TransformPoint(raycastHit.point,
                                                            Quaternion.LookRotation(raycastHit.normal, m_CharacterLocomotion.Up),
                                                            new Vector3(0, 0, m_WallOffset / 2)), -m_CharacterLocomotion.Up, out groundRaycastHit,
                                                            Mathf.Infinity, m_CharacterLayerManager.IgnoreInvisibleCharacterLayers, QueryTriggerInteraction.Ignore)) {
                    // If the ground collider matches then the object is valid.
                    if (groundRaycastHit.collider == m_GroundCollider) {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Retruns true if the wall is near the gap from the ledge.
        /// </summary>
        /// <returns>True if the wall is near the gap from the ledge.</returns>
        private bool IsNearLedgeGap()
        {
            RaycastHit hit;
            var castPosition = MathUtility.TransformPoint(m_WallRaycastResult.point, Quaternion.LookRotation(m_WallRaycastResult.normal, m_CharacterLocomotion.Up),
                Vector3.forward * m_GapOffset);
            if (Physics.SphereCast(castPosition, IsActive ? m_ActiveGapOffsetCastRadius : m_CanStartGapOffsetCastRadius, -m_CharacterLocomotion.Up, out hit, m_MinimumLedgeHeight, 
                m_CharacterLayerManager.IgnoreInvisibleCharacterLayers, QueryTriggerInteraction.Ignore)) {
#if ULTIMATE_CHARACTER_CONTROLLER_CLIMBING
                // Ladders are considered a gap.
                if (hit.collider.gameObject.GetCachedComponent<Climbing.Objects.Ladder>() != null) {
                    return true;
                }
#endif
                return false;
            }
            return true;
        }

        /// <summary>
        /// Called when another ability is attempting to start and the current ability is active.
        /// Returns true or false depending on if the new ability should be blocked from starting.
        /// </summary>
        /// <param name="startingAbility">The ability that is starting.</param>
        /// <returns>True if the ability should be blocked.</returns>
        public override bool ShouldBlockAbilityStart(Ability startingAbility)
        {
            return startingAbility is Slide || startingAbility is SpeedChange;
        }

        /// <summary>
        /// Called when the current ability is attempting to start and another ability is active.
        /// Returns true or false depending on if the active ability should be stopped.
        /// </summary>
        /// <param name="activeAbility">The ability that is currently active.</param>
        /// <returns>True if the ability should be stopped.</returns>
        public override bool ShouldStopActiveAbility(Ability activeAbility)
        {
            return activeAbility is Slide || activeAbility is SpeedChange;
        }

        /// <summary>
        /// The ability has started.
        /// </summary>
        protected override void AbilityStarted()
        {
            base.AbilityStarted();

            m_InPosition = m_InRotation = false;

            // Force independent look so the movement type won't try to rotate the character.
            EventHandler.ExecuteEvent(m_GameObject, "OnCharacterForceIndependentLook", true);
        }

        /// <summary>
        /// Updates the ability.
        /// </summary>
        public override void Update()
        {
            if (!m_InPosition || !m_InRotation) {
                return;
            }

            // The ability should stop if there is no longer a wall behind the character. object is no longer in front of the character. Perform a raycast every update to keep the RaycastHit value updated.
            var raycastPosition = m_CharacterLocomotion.InverseTransformPoint(m_WallRaycastResult.point);
            raycastPosition.x = raycastPosition.z = 0;
            if (!Physics.Raycast(m_CharacterLocomotion.TransformPoint(raycastPosition) + m_WallRaycastResult.normal * m_DetectWallDistance / 2, -m_WallRaycastResult.normal, out m_RaycastHit,
                m_DetectWallDistance, 1 << m_WallRaycastResult.collider.gameObject.layer, QueryTriggerInteraction.Ignore)) {
                StopAbility();
                return;
            }

            // Ensure the normal is facing in a valid direction.
            if (m_RaycastHit.normal != m_CharacterLocomotion.Up) {
                m_WallRaycastResult = m_RaycastHit;
            }

            // Stop the ability if there is no more ledge.
            if (!IsNearLedgeGap()) {
                StopAbility();
                return;
            }

            base.Update();

            var inputVector = m_CharacterLocomotion.RawInputVector;
            if (m_CharacterLocomotion.FirstPersonPerspective) {
                // While in first person perspective the movement should be relative to the camera looking from the character's head.
                inputVector.x *= -1;
            } else {
                // The third person move direction can use the forward movement depending on the look source direction.
                if (Mathf.Abs(inputVector.y) > 0) {
                    inputVector.x = inputVector.y * (Vector3.Dot(m_LookSource.LookDirection(true), m_CharacterLocomotion.Rotation * Vector3.right) < 0 ? 1 : -1);
                }
                inputVector.y = 0;
            }

            // Stop movement if an object is in the way.
            var hit = new RaycastHit();
            if (inputVector.sqrMagnitude > 0 &&
                m_CharacterLocomotion.SingleCast(m_CharacterLocomotion.Rotation * Vector3.right * (inputVector.x < 0 ? 1 : -1), Vector3.zero,
                            m_WallOffset + (!m_CharacterLocomotion.Moving ? 0.1f : 0), m_CharacterLayerManager.SolidObjectLayers, ref hit)) {
                m_CharacterLocomotion.InputVector = m_CharacterLocomotion.RawInputVector = Vector2.zero;
                return;
            }

            m_CharacterLocomotion.InputVector = inputVector;
        }

        /// <summary>
        /// Update the controller's rotation values.
        /// </summary>
        public override void UpdateRotation()
        {
            // The character should always face away from the wall.
            var raycastNormal = Vector3.ProjectOnPlane(m_WallRaycastResult.normal, m_CharacterLocomotion.Up).normalized;
            var targetRotation = Quaternion.RotateTowards(m_CharacterLocomotion.Rotation, Quaternion.LookRotation(raycastNormal, m_CharacterLocomotion.Up), m_CharacterLocomotion.MotorRotationSpeed);
            m_CharacterLocomotion.DesiredRotation = Quaternion.Inverse(m_CharacterLocomotion.Rotation) * targetRotation;

            if (!m_InRotation && Quaternion.Angle(m_CharacterLocomotion.Rotation, targetRotation) < 0.001f) {
                m_InRotation = true;
            }
        }

        /// <summary>
        /// Update the controller's position values.
        /// </summary>
        public override void UpdatePosition()
        {
            if (m_InPosition) {
                return;
            }
            
            // If the character is not in position yet they should be moved to the wall position. Do not rely on input to move the character ot the correct position.
            var currentPosition = m_CharacterLocomotion.Position + m_CharacterLocomotion.DesiredMovement;
            var wallRotation = Quaternion.LookRotation(m_WallRaycastResult.normal, m_CharacterLocomotion.Up);
            var targetPosition = m_CharacterLocomotion.InverseTransformPoint(MathUtility.TransformPoint(m_WallRaycastResult.point, wallRotation, Vector3.forward * m_WallOffset));
            targetPosition.y = 0;
            targetPosition = MathUtility.TransformPoint(m_CharacterLocomotion.TransformPoint(targetPosition), wallRotation, Vector3.forward * m_WallOffset);
            m_CharacterLocomotion.DesiredMovement = Vector3.MoveTowards(Vector3.zero, targetPosition - currentPosition, m_StartMoveTowardsWallSpeed * m_CharacterLocomotion.TimeScale * Time.timeScale);

            if ((targetPosition - currentPosition).magnitude < m_CharacterLocomotion.ColliderSpacing) {
                m_InPosition = true;
            }
        }

        /// <summary>
        /// Verify the position values.
        /// </summary>
        public override void ApplyPosition()
        {
            if (!m_InPosition) {
                return;
            }

            // Position the character to be close to the wall.
            var moveDirection = m_CharacterLocomotion.InverseTransformDirection(m_CharacterLocomotion.DesiredMovement);
            var targetPosition = MathUtility.TransformPoint(m_WallRaycastResult.point, Quaternion.LookRotation(m_WallRaycastResult.normal, m_CharacterLocomotion.Up), Vector3.forward * m_WallOffset);
            moveDirection.z = MathUtility.InverseTransformPoint(m_CharacterLocomotion.Position + m_CharacterLocomotion.DesiredMovement, m_CharacterLocomotion.Rotation, targetPosition).z;
            m_CharacterLocomotion.DesiredMovement = m_CharacterLocomotion.TransformDirection(moveDirection);
        }

        /// <summary>
        /// Can the ability be stopped?
        /// </summary>
        /// <param name="force">Should the ability be force stopped?</param>
        /// <returns>True if the ability can be stopped.</returns>
        public override bool CanStopAbility(bool force)
        {
            if (force) { return true; }

            return m_InPosition && m_InRotation;
        }

        /// <summary>
        /// The ability has stopped running.
        /// </summary>
        /// <param name="force">Was the ability force stopped?</param>
        protected override void AbilityStopped(bool force)
        {
            base.AbilityStopped(force);

            // The hang ability may have stopped the ledge strafe. Reassign the starting ability so the ItemEquipVerifier will
            // reequip the items after hang has ended.
            var hangAbility = m_CharacterLocomotion.GetAbility<Hang>();
            if (hangAbility != null && hangAbility.LedgeStrafeDropStart && m_CharacterLocomotion.ItemEquipVerifierAbility != null) {
                m_CharacterLocomotion.ItemEquipVerifierAbility.StartingAbility = hangAbility;
            }

            m_StopFrame = Time.frameCount;
            // If the ability is stopped form input then add a small amount of force to push the character away from the wall.
            if (InputIndex != -1) {
                m_CharacterLocomotion.AddForce(m_WallRaycastResult.normal * m_StopForce, m_StopForceFrames, false);
            }

            EventHandler.ExecuteEvent(m_GameObject, "OnCharacterForceIndependentLook", false);
        }

        /// <summary>
        /// The character's ability has been started or stopped.
        /// </summary>
        /// <param name="ability">The ability which was started or stopped.</param>
        /// <param name="active">True if the ability was started, false if it was stopped.</param>
        private void OnAbilityActive(Ability ability, bool active)
        {
            if (!active || m_StopFrame != Time.frameCount || !(ability is Jump)) {
                return;
            }

            var jump = ability as Jump;
            jump.ImmediateJump = true;
            // Add a small amount of force to the character to push them away from the wall.
            m_CharacterLocomotion.AddForce(m_CharacterLocomotion.Rotation * Vector3.forward * m_StopForce, m_StopForceFrames, false);
        }

        /// <summary>
        /// Can the ledge strafe ability start from the climbing ability?
        /// </summary>
        /// <returns>True if the ledge strafe ability can start.</returns>
        public bool CanStartFromClimb()
        {
#if ULTIMATE_CHARACTER_CONTROLLER_CLIMBING
            if (m_CharacterLocomotion.RawInputVector.y <= 0 ||!m_StartFromClimb) {
                return false;
            }

            // A wall must exist on the opposite side.
            if (!Physics.Raycast(m_CharacterLocomotion.Position + m_CharacterLocomotion.Up * m_CharacterLocomotion.Height, m_CharacterLocomotion.Rotation * Vector3.forward, out m_RaycastHit, 
                                    (m_DetectWallDistance + m_CharacterLocomotion.Radius) * 2, m_CharacterLayerManager.SolidObjectLayers, QueryTriggerInteraction.Ignore)) {
                return false;
            }

            // The wall must be above ground.
            if (!Physics.Raycast(m_RaycastHit.point, -m_CharacterLocomotion.Up, out m_RaycastHit, m_CharacterLocomotion.Height, m_CharacterLayerManager.SolidObjectLayers, QueryTriggerInteraction.Ignore)) {
                return false;
            }

            // The ground must be a valid strafing object.
            if (!IsOverValidObject(m_RaycastHit)) {
                return false;
            }

            return true;
#else
            return false;
#endif
        }

        /// <summary>
        /// The character has been destroyed.
        /// </summary>
        public override void OnDestroy()
        {
            base.OnDestroy();

            EventHandler.UnregisterEvent<ILookSource>(m_GameObject, "OnCharacterAttachLookSource", OnAttachLookSource);
            EventHandler.UnregisterEvent<Ability, bool>(m_GameObject, "OnCharacterAbilityActive", OnAbilityActive);
        }
    }
}