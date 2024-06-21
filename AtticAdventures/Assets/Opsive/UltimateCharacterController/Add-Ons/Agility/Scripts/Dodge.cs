/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.AddOns.Agility
{
    using Opsive.Shared.Events;
    using Opsive.UltimateCharacterController.Character.Abilities;
    using UnityEngine;

    /// <summary>
    /// The Dodge ability will allow the character to quickly dodge left, right, forward or back in order to avoid an attack.
    /// </summary>
    [DefaultAbilityIndex(101)]
    [DefaultUseRootMotionPosition(AbilityBoolOverride.True)]
    [DefaultStartType(AbilityStartType.ButtonDown)]
    [DefaultInputName("Jump")]
    [Opsive.Shared.Utility.Group("Agility Pack")]
    public class Dodge : Ability
    {
        /// <summary>
        /// The direction that the character should dodge.
        /// </summary>
        public enum Direction
        {
            Left,       // Left dodge.
            Right,      // Right dodge.
            Forward,    // Forward dodge.
            Backward,   // Backward dodge.
            None        // No dodge.
        }

        [Tooltip("Should the ability start when the movement input is pressed, released, and then pressed again?")]
        [SerializeField] protected bool m_DoubleMovementStart;
        [Tooltip("The double movement history will timeout after the specified number of seconds.")]
        [SerializeField] protected float m_DoubleMovementTimeout = 0.2f;
        [Tooltip("Does the character need to be aiming in order to dodge?")]
        [SerializeField] protected bool m_RequireAim = true;
        [Tooltip("Does the character need to have a target in order to dodge? The Aim Assist component is required if this value is true.")]
        [SerializeField] protected bool m_RequireTarget = true;
        [Tooltip("The direction that the ability should dodge if the character isn't moving.")]
        [SerializeField] protected Direction m_NoVelocityDirection = Direction.None;

        public bool DoubleMovementStart { get { return m_DoubleMovementStart; } set { m_DoubleMovementStart = value; } }
        public float DoubleMovementTimeout { get { return m_DoubleMovementTimeout; } set { m_DoubleMovementTimeout = value; } }
        public bool RequireAim { get { return m_RequireAim; } set { m_RequireAim = value; } }
        public bool RequireTarget { get { return m_RequireTarget; } set { m_RequireTarget = value; } }
        public Direction NoVelocityDirection { get { return m_NoVelocityDirection; } set { m_NoVelocityDirection = value; } }

        private Character.Abilities.AssistAim m_AssistAim;

        private Vector2[] m_InactiveInput = new Vector2[2];
        private float m_InactiveInputTime = -1;
        private bool m_Aiming;
        private Direction m_Direction;

        public override int AbilityIntData { get { return (int)m_Direction; } }

        /// <summary>
        /// Initialize the default values.
        /// </summary>
        public override void Awake()
        {
            base.Awake();

            m_AssistAim = m_CharacterLocomotion.GetAbility<Character.Abilities.AssistAim>();

            EventHandler.RegisterEvent<bool, bool>(m_GameObject, "OnAimAbilityStart", OnAim);
            EventHandler.RegisterEvent(m_GameObject, "OnAnimatorDodgeComplete", OnComplete);

            if (m_DoubleMovementStart && m_StartType != AbilityStartType.Automatic) {
                Debug.LogWarning("Warning: The start type on the Dodge ability should be set to Automatic if Double Movement Start is enabled.");
                m_StartType = AbilityStartType.Automatic;
            }
        }

        /// <summary>
        /// Update the input while the ability is not active.
        /// </summary>
        public override void InactiveUpdate()
        {
            if (!m_DoubleMovementStart) {
                return;
            }

            if (!m_CharacterLocomotion.Grounded || (m_RequireAim && !m_Aiming) || (m_RequireTarget && m_AssistAim != null && m_AssistAim.Target == null)) {
                m_InactiveInputTime = -1;
                return;
            }

            var firstInactiveInputMagnitude = m_InactiveInput[0].sqrMagnitude;
            var secondInactiveInputMagnitude = m_InactiveInput[1].sqrMagnitude;
            if (firstInactiveInputMagnitude > 0 && secondInactiveInputMagnitude == 0) {
                // Clear the input history if the timeout has elapsed. This will prevent the ability from starting if there is a long pause in between movements.
                if (m_InactiveInputTime + m_DoubleMovementTimeout < Time.time) {
                    m_InactiveInput[0] = Vector2.zero;
                    m_InactiveInputTime = -1;
                }
            } else if (secondInactiveInputMagnitude > 0 || m_CharacterLocomotion.RawInputVector.sqrMagnitude > 0) {
                // Add to the inactive input. The ability won't start until element 0 has input, element 1 has no input, 
                // and the current input is the same as element 0.
                m_InactiveInput[0] = m_InactiveInput[1];
                m_InactiveInput[1] = m_CharacterLocomotion.RawInputVector;
                m_InactiveInputTime = Time.time;
            }
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

            if (!m_CharacterLocomotion.Grounded || (m_RequireAim && !m_Aiming) || (m_RequireTarget && m_AssistAim != null && m_AssistAim.Target == null)) {
                return false;
            }

            var localVelocity = m_CharacterLocomotion.LocalVelocity;
            localVelocity.y = 0;
            if (m_NoVelocityDirection == Direction.None && localVelocity.sqrMagnitude == 0) {
                return false;
            }

            // Double movement start requires the input to be active one frame, inactive the next, and then active again.
            if (m_DoubleMovementStart) {
                if (m_InactiveInput[0].sqrMagnitude == 0 || m_InactiveInput[1].sqrMagnitude > 0) {
                    return false;
                }

                // The current input must match the original input.
                if (m_InactiveInput[0] != m_CharacterLocomotion.RawInputVector) {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Should the input be checked to ensure button up is using the correct value?
        /// </summary>
        protected override bool ShouldCheckInput() { return false; }

        /// <summary>
        /// The ability has started.
        /// </summary>
        protected override void AbilityStarted()
        {
            base.AbilityStarted();

            if (m_CharacterLocomotion.LocalVelocity.sqrMagnitude == 0) {
                m_Direction = m_NoVelocityDirection;
            } else {
                var inputVector = m_CharacterLocomotion.ActiveMovementType.UseIndependentLook(true) ? m_CharacterLocomotion.InputVector : m_CharacterLocomotion.RawInputVector;
                // Horizontal dodge has priority over forward/back.
                var horizontalDodge = Mathf.Abs(inputVector.x) >= Mathf.Abs(inputVector.y);
                if (horizontalDodge) {
                    if (inputVector.x < 0) {
                        m_Direction = Direction.Left;
                    } else {
                        m_Direction = Direction.Right;
                    }
                } else {
                    if (inputVector.y > 0) {
                        m_Direction = Direction.Forward;
                    } else {
                        m_Direction = Direction.Backward;
                    }
                }
            }
        }
        
        /// <summary>
        /// Updates the ability.
        /// </summary>
        public override void Update()
        {
            // Do not call the base method to prevent an attribute from stopping the dodge.
        }

        /// <summary>
        /// The Aim ability has started or stopped.
        /// </summary>
        /// <param name="aim">Has the Aim ability started?</param>
        /// <param name="inputStart">Was the ability started from input?</param>
        private void OnAim(bool aim, bool inputStart)
        {
            if (!inputStart) {
                return;
            }
            m_Aiming = aim;
        }

        /// <summary>
        /// The dodge animation has completed.
        /// </summary>
        private void OnComplete()
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

            m_InactiveInput[0] = m_InactiveInput[1] = Vector2.zero;
            m_InactiveInputTime = -1;
        }

        /// <summary>
        /// The character has been destroyed.
        /// </summary>
        public override void OnDestroy()
        {
            base.OnDestroy();
            
            EventHandler.UnregisterEvent<bool, bool>(m_GameObject, "OnAimAbilityStart", OnAim);
            EventHandler.UnregisterEvent(m_GameObject, "OnAnimatorDodgeComplete", OnComplete);
        }
    }
}