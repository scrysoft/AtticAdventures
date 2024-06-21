/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.AddOns.Agility.Demo
{
    using Opsive.UltimateCharacterController.AddOns.Agility;
    using Opsive.UltimateCharacterController.Character;
    using Opsive.UltimateCharacterController.Objects;
    using UnityEngine;

    /// <summary>
    /// Activates when the character is hanging on the Moving Platform.
    /// </summary>
    public class HangMovingPlatform : MonoBehaviour
    {
        [Tooltip("A reference to the Moving Platform component.")]
        [SerializeField] protected MovingPlatform m_MovingPlatform;

        private Hang m_Hang;

        /// <summary>
        /// Initialize the default values.
        /// </summary>
        private void Awake()
        {
            m_MovingPlatform.MovementType = MovingPlatform.PathMovementType.Target;
        }

        /// <summary>
        /// Sets the Moving Platform movement type when the character is hanging on the Moving Platform.
        /// </summary>
        private void Update()
        {
            if (m_Hang == null) {
                return;
            }

            if (m_MovingPlatform.MovementType == MovingPlatform.PathMovementType.Target) {
                if (m_Hang.ActiveHangState == Hang.HangState.Shimmy && m_Hang.DetectedObject == m_MovingPlatform.gameObject) {
                    m_MovingPlatform.MovementType = MovingPlatform.PathMovementType.Loop;
                }
            }
        }

        /// <summary>
        /// An object has entered the trigger.
        /// </summary>
        /// <param name="other">The Collider that entered the trigger.</param>
        private void OnTriggerEnter(Collider other)
        {
            var characterLocomotion = other.GetComponentInParent<UltimateCharacterLocomotion>();
            if (characterLocomotion == null) {
                return;
            }

            m_Hang = characterLocomotion.GetAbility<Hang>();
            if (m_Hang == null) {
                return;
            }
        }

        /// <summary>
        /// An object has exited the trigger.
        /// </summary>
        /// <param name="other">The Collider that exited the trigger.</param>
        private void OnTriggerExit(Collider other)
        {
            if (m_Hang == null) {
                return;
            }

            var characterLocomotion = other.GetComponentInParent<UltimateCharacterLocomotion>();
            if (characterLocomotion == null) {
                return;
            }

            m_Hang = null;
            m_MovingPlatform.MovementType = MovingPlatform.PathMovementType.Loop;
        }
    }
}