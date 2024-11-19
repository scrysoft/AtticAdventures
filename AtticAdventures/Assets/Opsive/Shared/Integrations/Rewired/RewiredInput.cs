/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.Shared.Integrations.Rewired
{
    using Opsive.Shared.Input;
    using UnityEngine;
    using global::Rewired;

    /// <summary>
    /// Acts as a bridge for Rewired.
    /// </summary>
    public class RewiredInput : PlayerInput
    {
        [Tooltip("The Rewired player id.")]
        [SerializeField] protected int m_PlayerID;
        [Tooltip("Should the cursor be disabled?")]
        [SerializeField] protected bool m_DisableCursor = true;
        [Tooltip("Should the cursor be enabled when the escape key is pressed?")]
        [SerializeField] protected bool m_EnableCursorWithEscape = true;
        [Tooltip("If the cursor is enabled with escape should the look vector be prevented from updating?")]
        [SerializeField] protected bool m_PreventLookVectorChanges = true;
        [Tooltip("Are touch controls being used with the Rewired integration?")]
        [SerializeField] protected bool m_EnableTouchControls;

        public int PlayerID { get { return m_PlayerID; }
            set {
                m_PlayerID = value;
                if (Application.isPlaying) {
                    m_Player = ReInput.players.GetPlayer(value);
                }
            }
        }
        public bool DisableCursor
        {
            get { return m_DisableCursor; }
            set
            {
                m_DisableCursor = value;
                if (m_DisableCursor && Cursor.visible) {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                } else if (!m_DisableCursor && !Cursor.visible) {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }
        public bool EnableCursorWithEscape { get { return m_EnableCursorWithEscape; } set { m_EnableCursorWithEscape = value; } }
        public bool PreventLookVectorChanges { get { return m_PreventLookVectorChanges; } set { m_PreventLookVectorChanges = value; } }
        public bool EnableTouchControls { get { return m_EnableTouchControls; } set { m_EnableTouchControls = value; } }

        private Player m_Player;

        /// <summary>
        /// Initialize the default values.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            m_Player = ReInput.players.GetPlayer(m_PlayerID);
        }

        /// <summary>
        /// The component has been enabled.
        /// </summary>
        private void OnEnable()
        {
            if (m_DisableCursor) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        /// <summary>
        /// Unlock the cursor.
        /// </summary>
        private void OnDisable()
        {
            if (m_DisableCursor) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        /// <summary>
        /// Returns true if the pointer is over a UI element.
        /// </summary>
        /// <returns>True if the pointer is over a UI element.</returns>
        public override bool IsPointerOverUI()
        {
            if (m_EnableTouchControls) {
                return false;
            }
            return base.IsPointerOverUI();
        }

        /// <summary>
        /// Update the cursor state values.
        /// </summary>
        private void LateUpdate()
        {
            // Enable the cursor if the escape key is pressed. Disable the cursor if it is visbile but should be disabled upon press.
            if (m_EnableCursorWithEscape && UnityEngine.Input.GetKeyDown(KeyCode.Escape)) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                if (m_PreventLookVectorChanges) {
                    OnApplicationFocus(false);
                }
            } else if (Cursor.visible && m_DisableCursor && !IsPointerOverUI() && (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0) || UnityEngine.Input.GetKeyDown(KeyCode.Mouse1))) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                if (m_PreventLookVectorChanges) {
                    OnApplicationFocus(true);
                }
            }
#if UNITY_EDITOR
            // The cursor should be visible when the game is paused.
            if (!Cursor.visible && Time.deltaTime == 0) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
#endif
        }

        /// <summary>
        /// Internal method which returns true if the button is being pressed.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        /// <returns>True of the button is being pressed.</returns>
        protected override bool GetButtonInternal(string name)
        {
            return m_Player.GetButton(name);
        }

        /// <summary>
        /// Internal method which returns true if the button was pressed this frame.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        /// <returns>True if the button is pressed this frame.</returns>
        protected override bool GetButtonDownInternal(string name)
        {
            return m_Player.GetButtonDown(name);
        }

        /// <summary>
        /// Internal method which returnstrue if the button is up.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        /// <returns>True if the button is up.</returns>
        protected override bool GetButtonUpInternal(string name)
        {
            return m_Player.GetButtonUp(name);
        }

        /// <summary>
        /// Internal method which returns the value of the axis with the specified name.
        /// </summary>
        /// <param name="name">The name of the axis.</param>
        /// <returns>The value of the axis.</returns>
        protected override float GetAxisInternal(string name)
        {
            return m_Player.GetAxis(name);
        }

        /// <summary>
        /// Internal method which returns the value of the raw axis with the specified name.
        /// </summary>
        /// <param name="name">The name of the axis.</param>
        /// <returns>The value of the raw axis.</returns>
        protected override float GetAxisRawInternal(string name)
        {
            return m_Player.GetAxisRaw(name);
        }

        /// <summary>
        /// Returns the position of the mouse.
        /// </summary>
        /// <returns>The mouse position.</returns>
        public override Vector2 GetMousePosition()
        {
            return m_Player.controllers.Mouse.screenPosition;
        }

        /// <summary>
        /// Enables or disables gameplay input. An example of when it will not be enabled is when there is a fullscreen UI over the main camera.
        /// </summary>
        /// <param name="enable">True if the input is enabled.</param>
        protected override void EnableGameplayInput(bool enable)
        {
            base.EnableGameplayInput(enable);

            if (enable && m_DisableCursor) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}