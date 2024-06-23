/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.AddOns.Climbing
{
    using Opsive.Shared.Events;
    using Opsive.Shared.Game;
    using Opsive.Shared.Utility;
    using Opsive.UltimateCharacterController.Character;
    using Opsive.UltimateCharacterController.Character.Abilities;
    using Opsive.UltimateCharacterController.Objects.CharacterAssist;
    using Opsive.UltimateCharacterController.Utility;
    using UnityEngine;

    /// <summary>
    /// The Short Climb ability allows the character to climb up short objects. Short Climb will specify the object height in the Ability Float Data parameter so
    /// animations can be played for different heights. 
    /// </summary>
    [DefaultStartType(AbilityStartType.ButtonDown)]
    [DefaultStopType(AbilityStopType.Manual)]
    [DefaultInputName("Action")]
    [DefaultState("ShortClimb")]
    [DefaultAbilityIndex(502)]
    [DefaultAllowRotationalInput(false)]
    [DefaultUseGravity(AbilityBoolOverride.False)]
    [DefaultUseRootMotionPosition(AbilityBoolOverride.True)]
    [DefaultDetectHorizontalCollisions(AbilityBoolOverride.False)]
    [DefaultDetectVerticalCollisions(AbilityBoolOverride.False)]
    [DefaultEquippedSlots(0)]
    [DefaultUseLookDirection(false)]
    [DefaultCastOffset(0, 0, 0)]
    [Group("Climbing Pack")]
    public class ShortClimb : Climb
    {
        [Tooltip("The maximum height of the object that the character can climb over.")]
        [SerializeField] protected float m_MaxHeight = 2f;
        [Tooltip("The multiplier to apply to the height. This can be used to specify different heights within the Animator for different sized characters.")]
        [SerializeField] protected float m_HeightMultiplier = 1;

        public float MaxHeight { get { return m_MaxHeight; } set { m_MaxHeight = value; } }
        public float HeightMultiplier { get { return m_HeightMultiplier; } set { m_HeightMultiplier = value; } }

        private float m_Height;

        public override float AbilityFloatData => m_Height;

        /// <summary>
        /// Called when the ablity is tried to be started. If false is returned then the ability will not be started.
        /// </summary>
        /// <returns>True if the ability can be started.</returns>
        public override bool CanStartAbility()
        {
            if (!m_CharacterLocomotion.Grounded || !base.CanStartAbility()) {
                return false;
            }

            var position = m_CharacterLocomotion.InverseTransformPoint(m_RaycastResult.point);
            position.y = 0;
            position = m_CharacterLocomotion.TransformPoint(position);

            // There must be space for the character to climb.
            RaycastHit hit;
            if (Physics.Raycast(position + m_CharacterLocomotion.Up * m_MaxHeight + m_RaycastResult.normal * m_CharacterLocomotion.Radius, m_CharacterLocomotion.Rotation * Vector3.forward, out hit,
                m_CharacterLocomotion.Radius * 3, m_CharacterLayerManager.SolidObjectLayers, QueryTriggerInteraction.Ignore)) {
                return false;
            }

            // The top of the detected object must be beneath the max height.
            if (!Physics.Raycast(position + m_CharacterLocomotion.Up * m_MaxHeight - m_RaycastResult.normal * 0.1f, -m_CharacterLocomotion.Up, out hit,
                m_MaxHeight, m_CharacterLayerManager.SolidObjectLayers, QueryTriggerInteraction.Ignore)) {
                return false;
            }

            m_Height = m_CharacterLocomotion.InverseTransformPoint(hit.point).y * m_HeightMultiplier;
            return true;
        }

        /// <summary>
        /// Returns the possible MoveTowardsLocations that the character can move towards.
        /// </summary>
        /// <returns>The possible MoveTowardsLocations that the character can move towards.</returns>
        public override MoveTowardsLocation[] GetMoveTowardsLocations()
        {
            if (m_DetectedObject == null) {
                return null;
            }

            var moveTowardsLocations = m_DetectedObject.GetCachedComponents<MoveTowardsLocation>();
            if (moveTowardsLocations != null) {
                var rotation = Quaternion.LookRotation(m_RaycastResult.normal, m_CharacterLocomotion.Up);
                var invRotation = Quaternion.LookRotation(-m_RaycastResult.normal, m_CharacterLocomotion.Up);
                // The start location should be modified to be directly in front of the climb object raycast position.
                for (int i = 0; i < moveTowardsLocations.Length; ++i) {
                    var startPosition = MathUtility.TransformPoint(m_RaycastResult.point, rotation, moveTowardsLocations[i].StartOffset);
                    moveTowardsLocations[i].Offset = moveTowardsLocations[i].transform.InverseTransformPoint(startPosition);
                    var yawOffset = MathUtility.InverseTransformQuaternion(moveTowardsLocations[i].transform.rotation, invRotation).eulerAngles.y;
                    moveTowardsLocations[i].YawOffset = MathUtility.ClampAngle(yawOffset + moveTowardsLocations[i].StartYawOffset);
                }
            }
            return moveTowardsLocations;
        }

        /// <summary>
        /// The ability has started.
        /// </summary>
        protected override void AbilityStarted()
        {
            base.AbilityStarted();

            m_CharacterLocomotion.ResetRotationPosition();
            EventHandler.RegisterEvent(m_GameObject, "OnAnimatorShortClimbComplete", OnShortClimbComplete);
        }

        /// <summary>
        /// Update the ability.
        /// </summary>
        public override void Update()
        {
            base.Update();

            // Set the input vector to zero so the animator transitions faster out of the movement blend tree.
            // If the movement blend tree is not active this line will not change anything.
            m_CharacterLocomotion.InputVector = Vector3.zero;
        }

        /// <summary>
        /// The short climb ability has completed - stop the ability.
        /// </summary>
        private void OnShortClimbComplete()
        {
            StopAbility();
        }

        /// <summary>
        /// The ability has stopped running.
        /// </summary>
        /// <param name="force">Was the ability force stopped?</param>
        protected override void AbilityStopped(bool force)
        {
            base.AbilityStopped(force);

            EventHandler.UnregisterEvent(m_GameObject, "OnAnimatorShortClimbComplete", OnShortClimbComplete);
        }
    }
}