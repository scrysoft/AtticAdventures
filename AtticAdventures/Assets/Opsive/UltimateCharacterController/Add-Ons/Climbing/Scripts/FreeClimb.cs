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
    using Opsive.UltimateCharacterController.Character;
    using Opsive.UltimateCharacterController.Character.Abilities;
    using Opsive.UltimateCharacterController.Objects.CharacterAssist;
    using Opsive.UltimateCharacterController.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// The FreeClimb ability allows the character to move in all directions on a climbable object. The character can traverse curved rough surfaces and make 90 degree turns.
    /// They are also able to leap in an upwards direction for quicker movement.
    /// </summary>
    [DefaultStartType(AbilityStartType.ButtonDown)]
    [DefaultStopType(AbilityStopType.ButtonToggle)]
    [DefaultInputName("Action")]
    [DefaultState("FreeClimb")]
    [DefaultAbilityIndex(503)]
    [DefaultAllowRotationalInput(false)]
    [DefaultUseGravity(AbilityBoolOverride.False)]
    [DefaultUseRootMotionPosition(AbilityBoolOverride.True)]
    [DefaultDetectHorizontalCollisions(AbilityBoolOverride.False)]
    [DefaultDetectVerticalCollisions(AbilityBoolOverride.False)]
    [DefaultEquippedSlots(0)]
    [DefaultCastOffset(0, 0.4f, -0.25f)]
    [Group("Climbing Pack")]
    public class FreeClimb : TraversalClimb
    {
        /// <summary>
        /// The movements that the ability can perform.
        /// </summary>
        [System.Flags]
        public enum AllowedMovement
        {
            InAirMount = 1,                 // The character can mount while in the air.
            InnerCornerTurn = 2,            // The character can perform an inner 90 degree turn.
            OuterCornerTurn = 4,            // The character can perform an outer 90 degree turn.
            Leap = 8,                       // The character can leap climb in the upward direction.
            BottomDismount = 16,            // The character can dismount on the bottom.
            TopDismount = 32,               // The character can dismount on the top.
#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
            StartFromHorizontalHang = 64,   // The character can start climbing from a horizontal position of the Agility Pack Hang ability.
            StartFromVerticalHang = 128,    // The character can start climbing from a vertical position of the Agility Pack Hang ability.
#endif
        }

        [Tooltip("Should debug lines be drawn to the editor indicating the location of the horizontal and vertical casts?")]
        [SerializeField] protected bool m_DrawDebugLines;
        [Tooltip("The movements that the ability can perform.")]
        [SerializeField] protected AllowedMovement m_AllowedMovements = (AllowedMovement)(-1);
        [Tooltip("The distance from the climbing object that the character should start mounting.")]
        [SerializeField] protected float m_BottomMountDistance = 0.5f;
        [Tooltip("The offset from the character to the climb object.")]
        [SerializeField] protected float m_ClimbOffset = 0.45f;
        [Tooltip("The speed that the character moves towards the climbing object.")]
        [SerializeField] protected float m_MoveTowardsSpeed = 0.02f;
        [Tooltip("Should the ability stop if the jump button is pressed?")]
        [SerializeField] protected bool m_StopClimbOnJump = true;
        [Tooltip("Specifies the distance from the character's center position when the character should stop moving horizontally when near the edge of the climb object.")]
        [SerializeField] protected float m_HorizontalEdgeOffset = 0.35f;
        [Tooltip("Specifies the distance from the character's center position when the character should transition to an inner corner.")]
        [SerializeField] protected float m_InnerCornerOffset = 0.1f;
        [Tooltip("Specifies the distance from the character's pivot position when the character should stop moving down near the edge of the climb object.")]
        [SerializeField] protected float m_BottomEdgeOffset = 0.5f;
        [Tooltip("Specifies the distance from the character's pivot position when the character should stop moving up near the edge of the climb object.")]
        [SerializeField] protected float m_TopEdgeOffset = 0.65f;
        [Tooltip("The offset between the character's hand and the IK raycast target.")]
        [SerializeField] protected Vector3 m_HandIKCastOffset = new Vector3(0, 0.1f, 0);
        [Tooltip("The offset between the character's foot and the IK raycast target.")]
        [SerializeField] protected Vector3 m_FootIKCastOffset = new Vector3(0, 0.0f, 0);
        [Tooltip("The offset to apply to the hand position relative to the object hit at the hand position.")]
        [SerializeField] protected Vector3 m_HandIKOffset = new Vector3(0, 0, -0.02f);
        [Tooltip("The offset to apply to the foot position relative to the object hit at the foot position.")]
        [SerializeField] protected Vector3 m_FootIKOffset = new Vector3(0, 0, -0.05f);
        [Tooltip("Specifies the distance from the character's pivot position when the character should dismount when near the bottom of the climb object.")]
        [SerializeField] protected float m_BottomDismountOffset = 0.3f;
        [Tooltip("Specifies the distance from the character's pivot position when the character should dismount when near the top of the climb object.")]
        [SerializeField] protected float m_TopDismountOffset = 1.47f;
        [Tooltip("Horizontal and vertical distance specifying the edge offset when the character is leaping.")]
        [SerializeField] protected Vector2 m_LeapEdgeOffset = new Vector2(0.8f, 3f);
#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
        [Tooltip("The offset used to check for a climbable object if the hang ability is active.")]
        [SerializeField] protected Vector3 m_StartHangOffset = new Vector3(1.5f, 1f, -0.5f);
#endif

        public AllowedMovement AllowedMovements { get { return m_AllowedMovements; } set { m_AllowedMovements = value; } }
        public float BottomMountDistance { get { return m_BottomMountDistance; } set { m_BottomMountDistance = value; } }
        public float ClimbOffset { get { return m_ClimbOffset; } set { m_ClimbOffset = value; } }
        public float MoveTowardsSpeed { get { return m_MoveTowardsSpeed; } set { m_MoveTowardsSpeed = value; } }
        public bool StopClimbOnJump { get { return m_StopClimbOnJump; } set { m_StopClimbOnJump = value; } }
        public float HorizontalEdgeOffset { get { return m_HorizontalEdgeOffset; } set { m_HorizontalEdgeOffset = value; } }
        public float InnerCornerOffset { get { return m_InnerCornerOffset; } set { m_InnerCornerOffset = value; } }
        public float BottomEdgeOffset { get { return m_BottomEdgeOffset; } set { m_BottomEdgeOffset = value; } }
        public float TopEdgeOffset { get { return m_TopEdgeOffset; } set { m_TopEdgeOffset = value; } }
        public Vector3 HandIKCastOffset { get { return m_HandIKCastOffset; } set { m_HandIKCastOffset = value; } }
        public Vector3 FootIKCastOffset { get { return m_FootIKCastOffset; } set { m_FootIKCastOffset = value; } }
        public Vector3 HandIKOffset { get { return m_HandIKOffset; } set { m_HandIKOffset = value; } }
        public Vector3 FootIKOffset { get { return m_FootIKOffset; } set { m_FootIKOffset = value; } }
        public float BottomDismountOffset { get { return m_BottomDismountOffset; } set { m_BottomDismountOffset = value; } }
        public float TopDismountOffset { get { return m_TopDismountOffset; } set { m_TopDismountOffset = value; } }
#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
        public Vector3 StartHangOffset { get { return m_StartHangOffset; } set { m_StartHangOffset = value; } }
#endif

        /// <summary>
        /// Specifies the current state of the ability.
        /// </summary>
        private enum ClimbState
        {
            BottomMount,            // The character is mounting from the bottom.
            TopMount,               // The character is mounting from the top.
            Climb,                  // The character is climbing.
            InnerCorner,            // The character is turning a 90 degree corner that turns into the character.
            OuterCorner,            // The character is turning a 90 degree corner that turns away from the character.
            BottomDismount,         // The character is pulling themself up to end the ability.
            TopDismount,            // The character is pulling themself up to end the ability.
            HorizontalHangStart,    // The character is starting to climb from the horizontal location of the Hang ability.
            VerticalHangStart,      // The character is starting to climb from the vertical location of the Hang ability.
            None
        }

        private ClimbState m_ClimbState;
        private float m_MoveDirection;

        private UltimateCharacterLocomotionHandler m_Handler;
        private Jump m_Jump;
        private SpeedChange m_SpeedChange;
        private CharacterIKBase m_CharacterIK;
        private ActiveInputEvent m_JumpInput;
        private MoveTowardsLocation[] m_BottomMoveTowardsLocation;

#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
        private Hang m_Hang;
        private LedgeStrafe m_LedgeStrafe;
#endif

        private RaycastHit m_RaycastHit;
        private bool m_TopStop;

        private Dictionary<CharacterIKBase.IKGoal, Vector3> m_IKPositionMap;
        private Dictionary<CharacterIKBase.IKGoal, Quaternion> m_IkRotationMap;

        public override int AbilityIntData => (int)m_ClimbState;
        public override float AbilityFloatData => m_MoveDirection;

        /// <summary>
        /// Initialize the default values.
        /// </summary>
        public override void Awake()
        {
            base.Awake();

            if (m_ObjectDetection == ObjectDetectionMode.Trigger) {
                Debug.LogError("Error: The Free Climb ObjectDetectionMode should not be set to Trigger.");
                Enabled = false;
                return;
            }

            m_Handler = m_GameObject.GetCachedComponent<UltimateCharacterLocomotionHandler>();
            m_Jump = m_CharacterLocomotion.GetAbility<Jump>();
            m_SpeedChange = m_CharacterLocomotion.GetAbility<SpeedChange>();
            var modelManager = m_GameObject.GetCachedComponent<ModelManager>();
            if (modelManager == null) {
                m_CharacterIK = m_GameObject.GetComponentInChildren<CharacterIKBase>();
            } else {
                m_CharacterIK = modelManager.ActiveModel.GetCachedComponent<CharacterIKBase>();
            }
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
            // The character may be trying to start from the top of the free climb object. Cast a ray towards the character just below the front of the character.
            // If a valid object is hit then the character can mount from the top.
            if (m_CharacterLocomotion.Grounded) {
                var castDistance = (m_CastDistance + Mathf.Abs(m_CastOffset.z)) * 2;
                if (base.CanStartAbility()) {
                    var raycastDirection = m_CharacterLocomotion.Rotation * Vector3.forward;

                    // The object must exist to the left and the right.
                    var lookRotation = Quaternion.LookRotation(raycastDirection);
                    for (int i = 0; i < 2; ++i) {
                        if (!Physics.Raycast(MathUtility.TransformPoint(m_CharacterLocomotion.Position, lookRotation, new Vector3(m_HorizontalEdgeOffset * (i == 0 ? 1 : -1),
                                            m_CharacterLocomotion.Radius, 0)), raycastDirection, out m_RaycastHit, castDistance,
                                            m_CharacterLayerManager.SolidObjectLayers, QueryTriggerInteraction.Ignore)) {
                            return false;
                        }
                        if (!ValidateObject(m_RaycastHit.collider.gameObject, m_RaycastHit)) {
                            return false;
                        }
                    }

                    m_ClimbState = ClimbState.BottomMount;
                    return true;
                }

                if (Physics.Raycast(m_CharacterLocomotion.TransformPoint(0, -m_CharacterLocomotion.Radius, castDistance), -(m_CharacterLocomotion.Rotation * Vector3.forward), 
                                    out m_RaycastResult, castDistance, m_DetectLayers, QueryTriggerInteraction.Ignore)) {
                    // The object must be a free climb object.
                    var hitObject = m_RaycastResult.collider.gameObject;
                    if (!ValidateObject(hitObject, m_RaycastResult)) {
                        return false;
                    }

                    var raycastNormal = Vector3.ProjectOnPlane(m_RaycastResult.normal, m_CharacterLocomotion.Up).normalized;
                    if (raycastNormal.sqrMagnitude == 0) {
                        return false;
                    }

                    // There must not be an object in the way.
                    if (Physics.Raycast(MathUtility.TransformPoint(m_RaycastResult.point, Quaternion.LookRotation(raycastNormal), new Vector3(0, m_CharacterLocomotion.Radius, 0.1f)),
                                    -m_CharacterLocomotion.Up, out m_RaycastHit, m_CharacterLocomotion.Height, m_CharacterLayerManager.SolidObjectLayers, QueryTriggerInteraction.Ignore)) {
                        if (!ValidateObject(m_RaycastResult.collider.gameObject, m_RaycastHit)) {
                            return false;
                        }
                    }

                    // The character can climb on the object.
                    DetectedObject = m_RaycastResult.collider.gameObject;
                    m_ClimbState = ClimbState.TopMount;
                    return true;
                }
            } else {
                if ((m_AllowedMovements & AllowedMovement.InAirMount) == 0) {
                    return false;
                }

                // The character's pivot position must be over the climbable object.
                if (!Physics.SphereCast(m_CharacterLocomotion.TransformPoint(m_CastOffset), m_CharacterLocomotion.Radius, (m_CharacterLocomotion.Rotation * Vector3.forward), out m_RaycastResult, (m_CastDistance + Mathf.Abs(m_CastOffset.z)) * 2, m_DetectLayers, QueryTriggerInteraction.Ignore) || 
                    (m_RaycastResult.transform != null && !ValidateObject(m_RaycastResult.collider.gameObject, m_RaycastResult))) {
                    return false;
                }

                // The top of the character must be over the climbable object.
                if (!Physics.Raycast(m_CharacterLocomotion.Position + m_CharacterLocomotion.Up * m_CharacterLocomotion.Height, (m_CharacterLocomotion.Rotation * Vector3.forward), out m_RaycastResult, (m_CastDistance + Mathf.Abs(m_CastOffset.z)) * 2, m_DetectLayers, QueryTriggerInteraction.Ignore) ||
                    (m_RaycastResult.transform != null && !ValidateObject(m_RaycastResult.collider.gameObject, m_RaycastResult))) {
                    return false;
                }

                m_ClimbState = ClimbState.BottomMount;
                return base.CanStartAbility();
            }

            return false;
        }

        /// <summary>
        /// Returns the possible MoveTowardsLocations that the character can move towards.
        /// </summary>
        /// <returns>The possible MoveTowardsLocations that the character can move towards.</returns>
        public override MoveTowardsLocation[] GetMoveTowardsLocations()
        {
            // The character can move to a start location if dropping to start.
            if (m_ClimbState == ClimbState.TopMount) {
                var moveTowardsLocations = m_DetectedObject.GetCachedComponents<MoveTowardsLocation>();
                if (moveTowardsLocations == null || moveTowardsLocations.Length == 0) {
                    moveTowardsLocations = m_DetectedObject.GetComponentsInChildren<MoveTowardsLocation>();
                }
                if (moveTowardsLocations != null) {
                    var rotation = Quaternion.LookRotation(-m_RaycastResult.normal, m_CharacterLocomotion.Up);
                    var invRotation = Quaternion.LookRotation(m_RaycastResult.normal, m_CharacterLocomotion.Up);
                    // The start location should be modified to be directly in front of the climb object raycast position.
                    for (int i = 0; i < moveTowardsLocations.Length; ++i) {
                        var startPosition = MathUtility.TransformPoint(m_RaycastResult.point, rotation, moveTowardsLocations[i].StartOffset);
                        moveTowardsLocations[i].Offset = moveTowardsLocations[i].transform.InverseTransformPoint(startPosition);
                        var yawOffset = MathUtility.InverseTransformQuaternion(moveTowardsLocations[i].transform.rotation, invRotation).eulerAngles.y;
                        moveTowardsLocations[i].YawOffset = MathUtility.ClampAngle(yawOffset + moveTowardsLocations[i].StartYawOffset);
                    }
                }
                return moveTowardsLocations;
            } else if (m_CharacterLocomotion.Grounded) {
                // The character should mount near the bottom.
                if (m_BottomMoveTowardsLocation == null || m_BottomMoveTowardsLocation.Length == 0) {
                    m_BottomMoveTowardsLocation = new MoveTowardsLocation[1];
                    m_BottomMoveTowardsLocation[0] = new GameObject(m_GameObject.name + "FreeClimbBottomMountLocation").AddComponent<MoveTowardsLocation>();
                    m_BottomMoveTowardsLocation[0].Offset = Vector3.zero;
                    m_BottomMoveTowardsLocation[0].YawOffset = 0;
                }

                // Do a SingleCast to ensure the character doesn't get too close to a rough object.
                if (m_CharacterLocomotion.SingleCast(m_CharacterLocomotion.Rotation * Vector3.forward, m_CharacterLocomotion.TransformPoint(0, 0, -m_CharacterLocomotion.Radius), 
                                                    m_RaycastResult.distance * 2 + m_CharacterLocomotion.Radius, m_CharacterLayerManager.SolidObjectLayers, ref m_RaycastHit)) {
                    m_RaycastResult = m_RaycastHit;
                }

                var rotation = Quaternion.LookRotation(m_RaycastResult.normal, m_CharacterLocomotion.Up);
                var localPosition = MathUtility.InverseTransformPoint(m_RaycastResult.point, rotation, m_CharacterLocomotion.Position);
                localPosition.z = m_BottomMountDistance;
                m_BottomMoveTowardsLocation[0].transform.position = MathUtility.TransformPoint(m_RaycastResult.point, rotation, localPosition);
                var rotationVector = Vector3.ProjectOnPlane(-m_RaycastResult.normal, m_CharacterLocomotion.Up);
                m_BottomMoveTowardsLocation[0].transform.rotation = Quaternion.LookRotation(rotationVector, m_CharacterLocomotion.Up);

                return m_BottomMoveTowardsLocation;
            }
            return base.GetMoveTowardsLocations();
        }

        /// <summary>
        /// The ability has started.
        /// </summary>
        protected override void AbilityStarted()
        {
            base.AbilityStarted();

            if (!m_CharacterLocomotion.Grounded && m_ClimbState != ClimbState.HorizontalHangStart && m_ClimbState != ClimbState.VerticalHangStart) {
                m_ClimbState = ClimbState.Climb;
                m_CharacterLocomotion.ResetRotationPosition();
            }
            // Only the top mount requires root motion rotation.
            m_CharacterLocomotion.ForceRootMotionRotation = m_ClimbState == ClimbState.TopMount;
            m_TopStop = false;

            // The jump button can stop the ability.
            if (m_StopClimbOnJump && m_Handler != null && m_Jump != null && m_Jump.InputNames.Length > 0 && m_Jump.StartType == AbilityStartType.ButtonDown) {
                m_JumpInput = GenericObjectPool.Get<ActiveInputEvent>();
                m_JumpInput.Initialize(ActiveInputEvent.Type.ButtonDown, m_Jump.InputNames[0], "OnJumpInput");
                m_Handler.RegisterInputEvent(m_JumpInput);
                EventHandler.RegisterEvent(m_GameObject, "OnJumpInput", OnJumpInput);
            }

            // The character's limbs should be placed on the climb object.
            if (m_CharacterIK != null) {
                m_CharacterIK.OnUpdateIKPosition += OnUpdateIKPosition;
                m_CharacterIK.OnUpdateIKRotation += OnUpdateIKRotation;

                m_IKPositionMap = new Dictionary<CharacterIKBase.IKGoal, Vector3>();
                m_IkRotationMap = new Dictionary<CharacterIKBase.IKGoal, Quaternion>();
            }

            EventHandler.RegisterEvent(m_GameObject, "OnAnimatorFreeClimbStartInPosition", OnFreeClimbStartInPosition);
            EventHandler.RegisterEvent(m_GameObject, "OnAnimatorFreeClimbTurnComplete", OnFreeClimbTurnComplete);
            EventHandler.RegisterEvent(m_GameObject, "OnAnimatorFreeClimbComplete", OnFreeClimbComplete);
        }

        /// <summary>
        /// The free climb ability is in starting position.
        /// </summary>
        private void OnFreeClimbStartInPosition()
        {
            // After the character is in position reset the rotation and position to prevent any external jump forces from affecting the character's move direction.
            m_CharacterLocomotion.ResetRotationPosition();

            m_ClimbState = ClimbState.Climb;
            m_MoveDirection = 0;
            m_CharacterLocomotion.ForceRootMotionRotation = false;
            SetAbilityIntDataParameter(AbilityIntData);
            SetAbilityFloatDataParameter(AbilityFloatData);
        }

        /// <summary>
        /// Updates the free climb state.
        /// </summary>
        public override void Update()
        {
            base.Update();

            // Clear the ik mappings to allow new ik values to be determined.
            if (m_CharacterIK != null) {
                m_IKPositionMap.Clear();
                m_IkRotationMap.Clear();
            }

            if (m_ClimbState == ClimbState.HorizontalHangStart || m_ClimbState == ClimbState.VerticalHangStart) {
                return;
            }

            var raycastNormal = Vector3.ProjectOnPlane((m_CharacterLocomotion.Rotation * Vector3.forward), m_CharacterLocomotion.Up).normalized;
            // The ability should stop if a climb object is no longer in front of the character. Perform a raycast every update to keep the RaycastHit value updated.
            if (!Physics.SphereCast(m_CharacterLocomotion.TransformPoint(m_CastOffset), m_CharacterLocomotion.Radius, raycastNormal, out m_RaycastHit, 
                (m_CastDistance + Mathf.Abs(m_CastOffset.z)) * 2, m_DetectLayers, QueryTriggerInteraction.Ignore) ||
                (m_RaycastHit.transform != m_RaycastResult.transform && !ValidateObject(m_RaycastHit.collider.gameObject, m_RaycastHit))) {
                StopAbility();
                return;
            }
            m_RaycastResult = m_RaycastHit;

            // If the character isn't climbing then the inputs do not need to be checked.
            if (m_ClimbState != ClimbState.Climb) {
                m_CharacterLocomotion.InputVector = Vector2.zero;
                return;
            }

            if (m_CharacterLocomotion.RawInputVector.y > 0) {
#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
                // The Hang ability may be able to start.
                if (m_Hang != null && m_Hang.TryStartFromClimb(this)) {
                    return;
                }
#endif

                // Stop the character from going through the ceiling.
                if (m_TopStop || Physics.Raycast(m_CharacterLocomotion.Position, m_CharacterLocomotion.Up, out m_RaycastHit, m_CharacterLocomotion.Height, 
                    m_CharacterLayerManager.SolidObjectLayers, QueryTriggerInteraction.Ignore)) {
                    // Stop the speed change ability if the character would hit an object when the speed change ability is active.
                    if (m_SpeedChange != null && m_SpeedChange.IsActive) {
                        m_CharacterLocomotion.TryStopAbility(m_SpeedChange);
                    }
                    m_TopStop = true;
                    m_CharacterLocomotion.InputVector = Vector3.zero;
                    return;
                }

                if ((m_AllowedMovements & AllowedMovement.TopDismount) != 0) {
                    // The character may need to dismount.
                    var rayPosition = m_CharacterLocomotion.TransformPoint(0, m_TopDismountOffset + m_CharacterLocomotion.Radius + m_CharacterLocomotion.ColliderSpacing, -Mathf.Abs(m_CastOffset.z));
#if UNITY_EDITOR
                    if (m_DrawDebugLines) {
                        Debug.DrawRay(rayPosition, raycastNormal, Color.green);
                    }
#endif
                    if (!Physics.Raycast(rayPosition, raycastNormal, out m_RaycastHit, 
                            (m_CastDistance + Mathf.Abs(m_CastOffset.z)) * 2, 1 << m_DetectedObject.layer, QueryTriggerInteraction.Ignore)) {
                        m_ClimbState = ClimbState.TopDismount;
                        SetAbilityIntDataParameter(AbilityIntData);
                        return;
                    }
                }
            } else if (m_CharacterLocomotion.RawInputVector.y < 0) {
                m_TopStop = false;
                if ((m_AllowedMovements & AllowedMovement.BottomDismount) != 0 &&
                            m_CharacterLocomotion.SingleCast(-m_CharacterLocomotion.Up, m_CharacterLocomotion.TransformDirection(0, 0, m_CastOffset.z), m_BottomDismountOffset, m_CharacterLayerManager.SolidObjectLayers, ref m_RaycastHit)) {
                    m_ClimbState = ClimbState.BottomDismount;
                    SetAbilityIntDataParameter(AbilityIntData);
                    return;
                }
            }

            // The object needs to exist in front of the move direction.
            var rayRotation = Quaternion.LookRotation(raycastNormal, m_CharacterLocomotion.Up);
            if (m_CharacterLocomotion.RawInputVector.sqrMagnitude > 0) {
                m_TopStop = false;
                // An object cannot block the character's path.
                var rayDirection = MathUtility.TransformDirection(Vector3.right * Mathf.Sign(m_CharacterLocomotion.RawInputVector.x), rayRotation);
                if (Mathf.Abs(m_CharacterLocomotion.RawInputVector.x) > 0 && 
                                                m_CharacterLocomotion.SingleCast(rayDirection, m_CharacterLocomotion.TransformDirection(0, 0, m_CastOffset.z * 2),
                                                m_InnerCornerOffset, m_CharacterLayerManager.SolidObjectLayers, ref m_RaycastHit)) {
#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
                    // The Hang ability may be able to start.
                    if (m_Hang != null && m_Hang.TryStartFromClimb(this)) {
                        return;
                    }
#endif
                    // The character may be able to move from one side to another with an inner corner.
                    if ((m_SpeedChange == null || !m_SpeedChange.IsActive) && (m_AllowedMovements & AllowedMovement.InnerCornerTurn) != 0 && 
                            ValidateObject(m_RaycastHit.collider.gameObject, m_RaycastHit) && Mathf.Abs(Vector3.Dot(m_RaycastHit.normal, m_RaycastResult.normal)) < 0.01f) {
                        m_ClimbState = ClimbState.InnerCorner;
                        m_MoveDirection = m_CharacterLocomotion.RawInputVector.x;
                        m_CharacterLocomotion.ForceRootMotionRotation = true;
                        SetAbilityIntDataParameter(AbilityIntData);
                        SetAbilityFloatDataParameter(AbilityFloatData);
                        return;
                    }
                    // Stop the speed change ability if the character would hit an object when the speed change ability is active.
                    if (m_SpeedChange != null && m_SpeedChange.IsActive) {
                        m_CharacterLocomotion.TryStopAbility(m_SpeedChange);
                    }
                    m_CharacterLocomotion.InputVector = Vector3.zero;
                    return;
                }

                var rayPosition = m_CharacterLocomotion.TransformPoint(m_CharacterLocomotion.RawInputVector.x * ((m_SpeedChange != null && m_SpeedChange.IsActive) ? m_LeapEdgeOffset.x : m_HorizontalEdgeOffset),
                                                                (m_CharacterLocomotion.RawInputVector.y > 0 ?
                                                                    ((m_SpeedChange != null && m_SpeedChange.IsActive) ? m_LeapEdgeOffset.y : m_TopEdgeOffset) : m_BottomEdgeOffset), -m_CastDistance);
#if UNITY_EDITOR
                if (m_DrawDebugLines) {
                    Debug.DrawRay(rayPosition, (m_CastDistance + Mathf.Abs(m_CastOffset.z)) * 2 * raycastNormal);
                }
#endif
                // The character may be able to move from one side to another with an outer corner.
                if (!Physics.Raycast(rayPosition, raycastNormal, out m_RaycastHit, (m_CastDistance + Mathf.Abs(m_CastOffset.z)) * 2,
                    m_CharacterLayerManager.SolidObjectLayers, QueryTriggerInteraction.Ignore) || m_RaycastHit.transform != m_RaycastResult.transform) {
                    if (m_RaycastHit.transform != null) {
                        if (!ValidateObject(m_RaycastHit.collider.gameObject, m_RaycastHit)) {
#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
                            // The Ledge Strafe ability may be able to start.
                            if (m_LedgeStrafe != null && m_LedgeStrafe.CanStartFromClimb()) {
                                m_ClimbState = ClimbState.TopDismount;
                                SetAbilityIntDataParameter(AbilityIntData);
                                return;
                            }
#endif
                            // An object was hit but the object isn't climbable, stop moving.
                            m_CharacterLocomotion.InputVector = Vector3.zero;
                            return;
                        }

                        // A new climbable object was found, continue on.
                        return;
                    }

                    var originalRayPosition = rayPosition;
                    if ((m_SpeedChange == null || !m_SpeedChange.IsActive) && (m_AllowedMovements & AllowedMovement.OuterCornerTurn) != 0 && Mathf.Abs(m_CharacterLocomotion.RawInputVector.x) > 0) {
                        rayPosition = MathUtility.TransformPoint(rayPosition, rayRotation, Vector3.forward * m_CastDistance * 2);
                        rayDirection = MathUtility.TransformDirection(Vector3.right * -Mathf.Sign(m_CharacterLocomotion.RawInputVector.x), rayRotation);
                        if (Physics.Raycast(rayPosition, rayDirection, out m_RaycastHit, (m_CastDistance + Mathf.Abs(m_CastOffset.z)) * 2, m_DetectLayers, QueryTriggerInteraction.Ignore) && 
                            ValidateObject(m_RaycastHit.collider.gameObject, m_RaycastHit) && Mathf.Abs(Vector3.Dot(m_RaycastHit.normal, m_RaycastResult.normal)) < 0.01f) {
                            m_ClimbState = ClimbState.OuterCorner;
                            m_MoveDirection = m_CharacterLocomotion.RawInputVector.x;
                            m_CharacterLocomotion.ForceRootMotionRotation = true;
                            SetAbilityIntDataParameter(AbilityIntData);
                            SetAbilityFloatDataParameter(AbilityFloatData);
                            return;
                        }
                    }

                    // Stop the speed change ability if the character would hit an object when the speed change ability is active.
                    if (!Physics.Raycast(originalRayPosition, raycastNormal, out m_RaycastHit, (m_CastDistance + Mathf.Abs(m_CastOffset.z)) * 2, m_DetectLayers, QueryTriggerInteraction.Ignore) || m_RaycastHit.transform != m_RaycastResult.transform) {
                        if (m_SpeedChange != null && m_SpeedChange.IsActive) {
                            m_CharacterLocomotion.TryStopAbility(m_SpeedChange);
                        }
                        
                        // No object was hit - stop the character.
                        m_CharacterLocomotion.InputVector = Vector3.zero;
                    }
                }
            }

            // No matter the movement type the x input should be set to the Horizontal Movement parameter, and the y input should be the Forward Movement parameter.
            if (m_CharacterLocomotion.InputVector.sqrMagnitude > 0) {
                m_CharacterLocomotion.InputVector = m_CharacterLocomotion.RawInputVector;
            }
        }

        /// <summary>
        /// Update the controller's rotation values.
        /// </summary>
        public override void UpdateRotation()
        {
            base.UpdateRotation();

            if (m_ClimbState != ClimbState.Climb) {
                return;
            }

            // The character should always face the climb object.
            var raycastNormal = Vector3.ProjectOnPlane(m_RaycastResult.normal, m_CharacterLocomotion.Up).normalized;
            var targetRotation = Quaternion.LookRotation(-raycastNormal, m_CharacterLocomotion.Up);
            m_CharacterLocomotion.DesiredRotation = Quaternion.Inverse(m_CharacterLocomotion.Rotation) * targetRotation;
        }

        /// <summary>
        /// Verify the position values. Called immediately before the position is applied.
        /// </summary>
        public override void ApplyPosition()
        {
            base.ApplyPosition();

            if (m_ClimbState != ClimbState.Climb) {
                return;
            }

            // When the character is climbing they should always be a set distance away from the object. Force that position based on the object's position and normal.
            var offset = m_CharacterLocomotion.InverseTransformPoint(m_RaycastResult.point).z - m_ClimbOffset;
            var localDesiredMovement = m_CharacterLocomotion.InverseTransformDirection(m_CharacterLocomotion.DesiredMovement);
            localDesiredMovement.z = Mathf.MoveTowards(localDesiredMovement.z, offset, m_MoveTowardsSpeed * m_CharacterLocomotion.TimeScale);
            m_CharacterLocomotion.DesiredMovement = m_CharacterLocomotion.TransformDirection(localDesiredMovement);
        }

        /// <summary>
        /// Rotates the IK object to be facing in the direction of the climb object.
        /// </summary>
        /// <param name="ikGoal">The IK object being repositioned.</param>
        /// <param name="ikRotation">The current rotation of the IK object.</param>
        /// <param name="ikPosition">The current position of the IK object.</param>
        /// <returns>The new position of the IK object.</returns>
        public Quaternion OnUpdateIKRotation(CharacterIKBase.IKGoal ikGoal, Quaternion ikRotation, Vector3 ikPosition)
        {
            UpdateIKRotationPosition(ikGoal, ref ikRotation, ref ikPosition);

            return ikRotation;
        }

        /// <summary>
        /// Positions the IK object to be on the climb object.
        /// </summary>
        /// <param name="ikGoal">The IK object being repositioned.</param>
        /// <param name="ikRotation">The current rotation of the IK object.</param>
        /// <param name="ikPosition">The current position of the IK object.</param>
        /// <returns>The new position of the IK object.</returns>
        public Vector3 OnUpdateIKPosition(CharacterIKBase.IKGoal ikGoal, Vector3 ikPosition, Quaternion ikRotation)
        {
            UpdateIKRotationPosition(ikGoal, ref ikRotation, ref ikPosition);

            return ikPosition;
        }

        /// <summary>
        /// Rotates and positions the IK object to be on the haclimbng object.
        /// </summary>
        /// <param name="ikGoal">The IK object being repositioned.</param>
        /// <param name="ikRotation">The rotation of the IK object.</param>
        /// <param name="ikPosition">The position of the IK object.</param>
        private void UpdateIKRotationPosition(CharacterIKBase.IKGoal ikGoal, ref Quaternion ikRotation, ref Vector3 ikPosition)
        {
            var updateIK = false;
            if (m_IkRotationMap.ContainsKey(ikGoal)) {
                ikRotation = m_IkRotationMap[ikGoal];
                ikPosition = m_IKPositionMap[ikGoal];
            } else {
                updateIK = true;
            }

            // Don't do another raycast if the raycast has already been performed for the current tick.
            if (!updateIK) {
                return;
            }

            // The IK needs to be updated. Perform the update for both the rotation and position so the raycast only needs to be cast once.
            var raycastNormal = Vector3.ProjectOnPlane(m_RaycastResult.normal, m_CharacterLocomotion.Up).normalized;
            var rotation = m_CharacterLocomotion.Rotation;
            Vector3 offset;
            if (ikGoal == CharacterIKBase.IKGoal.LeftHand || ikGoal == CharacterIKBase.IKGoal.RightHand) {
                offset = MathUtility.TransformDirection(m_HandIKCastOffset, rotation);
            } else { // Left or right foot.
                offset = MathUtility.TransformDirection(m_FootIKOffset, rotation);
            }
            ikPosition += offset;
            if (!Physics.Raycast(ikPosition + raycastNormal * (Mathf.Abs(m_CastOffset.z) + 0.2f), -raycastNormal, out m_RaycastHit, 
                                    (m_CastDistance + Mathf.Abs(m_CastOffset.z) + Mathf.Abs(offset.z)) * 2, m_DetectLayers, QueryTriggerInteraction.Ignore)) {
                m_RaycastHit = m_RaycastResult;
            }

            if (m_ClimbState == ClimbState.Climb) {
                ikRotation *= Quaternion.LookRotation(-m_RaycastHit.normal, m_CharacterLocomotion.Up) * Quaternion.Inverse(rotation);
            }

            // The hand should be resting flat on the climb object in front of the hand. If no object was hit then the center climb object will be used.
            var localPosition = MathUtility.InverseTransformPoint(ikPosition, rotation, m_RaycastHit.point);
            localPosition.x = 0;
            if (ikGoal == CharacterIKBase.IKGoal.LeftHand || ikGoal == CharacterIKBase.IKGoal.RightHand) {
                localPosition.y = -m_HandIKCastOffset.y;
                localPosition += m_HandIKOffset;
            } else { // Left or right foot.
                localPosition.y = -m_FootIKCastOffset.y;
                localPosition += m_FootIKOffset;
            }
            ikPosition = MathUtility.TransformPoint(ikPosition, rotation, localPosition);

            m_IkRotationMap.Add(ikGoal, ikRotation);
            m_IKPositionMap.Add(ikGoal, ikPosition);
        }

        /// <summary>
        /// The free climb inner or outher turn has completed.
        /// </summary>
        private void OnFreeClimbTurnComplete()
        {
            m_ClimbState = ClimbState.Climb;
            m_MoveDirection = 0;
            m_CharacterLocomotion.ForceRootMotionRotation = false;
            SetAbilityIntDataParameter(AbilityIntData);
            SetAbilityFloatDataParameter(AbilityFloatData);
        }

        /// <summary>
        /// The jump ability input is being triggered.
        /// </summary>
        private void OnJumpInput()
        {
            if (!m_Jump.Enabled || m_ClimbState != ClimbState.Climb) {
                return;
            }

            OnFreeClimbComplete();
        }

        /// <summary>
        /// Called when another ability is attempting to start and the current ability is active.
        /// Returns true or false depending on if the new ability should be blocked from starting.
        /// </summary>
        /// <param name="startingAbility">The ability that is starting.</param>
        /// <returns>True if the ability should be blocked.</returns>
        public override bool ShouldBlockAbilityStart(Ability startingAbility)
        {
            if (startingAbility is SpeedChange) {
                if ((m_AllowedMovements & AllowedMovement.Leap) == 0) {
                    return true;
                }
                var rayPosition = m_CharacterLocomotion.TransformPoint(Mathf.Sign(m_CharacterLocomotion.RawInputVector.x) * m_LeapEdgeOffset.x, m_CharacterLocomotion.RawInputVector.y > 0 ? m_LeapEdgeOffset.y : 0, 0);
                var raycastNormal = Vector3.ProjectOnPlane(-(m_CharacterLocomotion.Rotation * Vector3.forward), m_CharacterLocomotion.Up).normalized;
                if (!Physics.Raycast(rayPosition, -raycastNormal, out m_RaycastHit, m_CastDistance, 1 << m_DetectedObject.layer, QueryTriggerInteraction.Ignore) || 
                    m_RaycastHit.transform != m_RaycastResult.transform) {
                    return true;
                }
                if (m_CharacterLocomotion.RawInputVector.x != 0) {
                    var rayDirection = MathUtility.TransformDirection(Vector3.right * Mathf.Sign(m_CharacterLocomotion.RawInputVector.x), Quaternion.LookRotation(-raycastNormal, m_CharacterLocomotion.Up));
                    if (m_CharacterLocomotion.SingleCast(rayDirection, Vector3.zero, m_LeapEdgeOffset.x, m_CharacterLayerManager.SolidObjectLayers, ref m_RaycastHit)) {
                        return true;
                    }
                }
            }
            return base.ShouldBlockAbilityStart(startingAbility);
        }

        /// <summary>
        /// The character's model has switched.
        /// </summary>
        /// <param name="activeModel">The active character model.</param>
        protected override void OnSwitchModels(GameObject activeModel)
        {
            base.OnSwitchModels(activeModel);

            m_CharacterIK = activeModel.GetCachedComponent<CharacterIKBase>();
        }

        /// <summary>
        /// The free climb ability has completed - stop the ability.
        /// </summary>
        private void OnFreeClimbComplete()
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
            if (m_CharacterIK != null) {
                m_CharacterIK.OnUpdateIKPosition -= OnUpdateIKPosition;
                m_CharacterIK.OnUpdateIKRotation -= OnUpdateIKRotation;
            }

            EventHandler.UnregisterEvent(m_GameObject, "OnAnimatorFreeClimbStartInPosition", OnFreeClimbStartInPosition);
            EventHandler.UnregisterEvent(m_GameObject, "OnAnimatorFreeClimbTurnComplete", OnFreeClimbTurnComplete);
            EventHandler.UnregisterEvent(m_GameObject, "OnAnimatorFreeClimbComplete", OnFreeClimbComplete);
        }

#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
        /// <summary>
        /// Tries to start the climbing ability from the hang ablity.
        /// </summary>
        /// <param name="hang">A reference to the active hang ability.</param>
        /// <returns>True if the ability was started.</returns>
        public override bool TryStartFromHang(Hang hang)
        {
            if (m_CharacterLocomotion.RawInputVector.sqrMagnitude == 0 ||
                (m_CharacterLocomotion.RawInputVector.x != 0 && (m_AllowedMovements & AllowedMovement.StartFromHorizontalHang) == 0) ||
                (m_CharacterLocomotion.RawInputVector.y != 0 && (m_AllowedMovements & AllowedMovement.StartFromVerticalHang) == 0)) {
                return false;
            }

            // The climbable object must exist in the direction of the input.
            var offset = m_StartHangOffset;
            offset.x = m_CharacterLocomotion.RawInputVector.x != 0 ? Mathf.Sign(m_CharacterLocomotion.RawInputVector.x) * m_StartHangOffset.x : 0;
            // There should not be a vertical offset for the raycast during a vertical transfer.
            offset.y = m_CharacterLocomotion.RawInputVector.y == 0 ? Mathf.Sign(m_CharacterLocomotion.RawInputVector.y) * m_StartHangOffset.y : 0;
#if UNITY_EDITOR
            if (m_DrawDebugLines) {
                Debug.DrawRay(m_CharacterLocomotion.TransformPoint(offset), (m_CastDistance + Mathf.Abs(offset.z)) * 2 * (m_CharacterLocomotion.Rotation * Vector3.forward), Color.green);
            }
#endif
            if (!Physics.Raycast(m_CharacterLocomotion.TransformPoint(offset), (m_CharacterLocomotion.Rotation * Vector3.forward), out m_RaycastResult, (m_CastDistance + Mathf.Abs(offset.z)) * 2, m_DetectLayers, QueryTriggerInteraction.Ignore) ||
                !ValidateObject(m_RaycastResult.collider.gameObject, m_RaycastResult)) {
                return false;
            }

            // The hit object is valid.
            DetectedObject = m_RaycastResult.collider.gameObject;

            // Stop the hang ability and start the climb ability.
            hang.StopAbility();
            m_ClimbState = m_CharacterLocomotion.RawInputVector.x != 0 ? ClimbState.HorizontalHangStart : ClimbState.VerticalHangStart;
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