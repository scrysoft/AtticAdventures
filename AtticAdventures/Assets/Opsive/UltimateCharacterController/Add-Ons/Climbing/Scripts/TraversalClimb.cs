/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.AddOns.Climbing
{
    using Opsive.Shared.Events;
    using Opsive.UltimateCharacterController.Character.Abilities;
    using UnityEngine;

    /// <summary>
    /// Abstract base class for climbing abilities that allow for free movement.
    /// </summary>
    public abstract class TraversalClimb : Climb
    {
        [Tooltip("Can the ability auto start when the character is airborne?")]
        [SerializeField] protected bool m_AirborneAutoStart = true;

        public bool AirborneAutoStart { get { return m_AirborneAutoStart; } set { m_AirborneAutoStart = value; } }

        private bool m_HasBeenGrounded;

        /// <summary>
        /// Initialize the default values.
        /// </summary>
        public override void Awake()
        {
            base.Awake();

            m_HasBeenGrounded = m_CharacterLocomotion.Grounded;

            EventHandler.RegisterEvent<bool>(m_GameObject, "OnCharacterGrounded", OnGrounded);
#if ULTIMATE_CHARACTER_CONTROLLER_SWIMMING
            EventHandler.RegisterEvent<Ability, bool>(m_GameObject, "OnCharacterAbilityActive", OnAbilityActive);
#endif
        }

        /// <summary>
        /// Try to start while the character is in the air.
        /// </summary>
        public override void InactiveUpdate()
        {
            if (m_LookSource == null) {
                return;
            }

            if (m_AirborneAutoStart && !m_CharacterLocomotion.Grounded) {
                m_CharacterLocomotion.TryStartAbility(this);
            }
        }

        /// <summary>
        /// Called when the ablity is tried to be started. If false is returned then the ability will not be started.
        /// </summary>
        /// <returns>True if the ability can be started.</returns>
        public override bool CanStartAbility()
        {
            if (!base.CanStartAbility()) {
                return false;
            }

            if (InputIndex != -1 || (AirborneAutoStart && m_HasBeenGrounded)) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// The ability has started.
        /// </summary>
        protected override void AbilityStarted()
        {
            base.AbilityStarted();

            // Require the character to be grounded before the ability can start again.
            m_HasBeenGrounded = false;
        }

#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
        /// <summary>
        /// Called when another ability is attempting to start and the current ability is active.
        /// Returns true or false depending on if the new ability should be blocked from starting.
        /// </summary>
        /// <param name="startingAbility">The ability that is starting.</param>
        /// <returns>True if the ability should be blocked.</returns>
        public override bool ShouldBlockAbilityStart(Ability startingAbility)
        {
            // Hang will be started manually by Hang.TryStartFromClimb.
            if (startingAbility is Agility.Hang) {
                return true;
            }
            
            return base.ShouldBlockAbilityStart(startingAbility);
        }
#endif

        /// <summary>
        /// The character has changed grounded states. 
        /// </summary>
        /// <param name="grounded">Is the character on the ground?</param>
        private void OnGrounded(bool grounded)
        {
            if (grounded) {
                m_HasBeenGrounded = true;
            }
        }

#if ULTIMATE_CHARACTER_CONTROLLER_SWIMMING
        /// <summary>
        /// The character's ability has been started or stopped.
        /// </summary>
        /// <param name="ability">The ability which was started or stopped.</param>
        /// <param name="active">True if the ability was started, false if it was stopped.</param>
        private void OnAbilityActive(Ability ability, bool active)
        {
            if (!(ability is Swimming.Swim)) {
                return;
            }

            m_HasBeenGrounded = false;
        }
#endif

#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
        /// <summary>
        /// Tries to start the climbing ability from the hang ablity.
        /// </summary>
        /// <param name="hang">A reference to the active hang ability.</param>
        /// <returns>True if the ability was started.</returns>
        public abstract bool TryStartFromHang(Agility.Hang hang);
#else
        /// <summary>
        /// Empty method to allow for compilation.
        /// </summary>
        /// <param name="hang">A reference to the active hang ability.</param>
        /// <returns>True if the ability was started.</returns>
        public abstract bool TryStartFromHang(Ability climb);
#endif

        /// <summary>
        /// The ability has stopped running.
        /// </summary>
        /// <param name="force">Was the ability force stopped?</param>
        protected override void AbilityStopped(bool force)
        {
            base.AbilityStopped(force);

            ResetInput(true);
        }

        /// <summary>
        /// Called when the character is destroyed.
        /// </summary>
        public override void OnDestroy()
        {
            base.OnDestroy();

            EventHandler.UnregisterEvent<bool>(m_GameObject, "OnCharacterGrounded", OnGrounded);
#if ULTIMATE_CHARACTER_CONTROLLER_SWIMMING
            EventHandler.UnregisterEvent<Ability, bool>(m_GameObject, "OnCharacterAbilityActive", OnAbilityActive);
#endif
        }
    }
}