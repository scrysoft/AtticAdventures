using AtticAdventures.Input;
using Cinemachine;
using KBCore.Refs;
using System;
using System.Collections;
using UnityEngine;

namespace AtticAdventures.Core
{
    public class CameraManager : ValidatedMonoBehaviour
    {
        [Header("References")]
        [SerializeField, Anywhere] InputReader input;
        [SerializeField, Anywhere] CinemachineFreeLook freeLookVirtualCamera;

        [Header("Settings")]
        [SerializeField, Range(0.5f, 20f)] float speedMultiplier = 1f;

        private bool isRightMouseButtonPressed;
        private bool cameraMovementLock;

        private void OnEnable()
        {
            input.Look += OnLook;
            input.EnableMouseControlCamera += OnEnableMouseControlCamera;
            input.DisableMouseControlCamera += OnDisableMouseControlCamera;
        }

        private void OnDisable()
        {
            input.Look -= OnLook;
            input.EnableMouseControlCamera -= OnEnableMouseControlCamera;
            input.DisableMouseControlCamera -= OnDisableMouseControlCamera;
        }

        private void OnLook(Vector2 cameraMovement, bool isDeviceMouse)
        {
            if (cameraMovementLock) return;

            if (isDeviceMouse && !isRightMouseButtonPressed) return;

            float deviceMultiplier = isDeviceMouse ? Time.fixedDeltaTime : Time.deltaTime;

            freeLookVirtualCamera.m_XAxis.m_InputAxisValue = cameraMovement.x * speedMultiplier * deviceMultiplier;
            freeLookVirtualCamera.m_YAxis.m_InputAxisValue = cameraMovement.y * speedMultiplier * deviceMultiplier;
        }

        private void OnDisableMouseControlCamera()
        {
            isRightMouseButtonPressed = false;

            // Unlock the cursor and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Reset the camera axis to prevent jumping when re-enabling mouse control
            freeLookVirtualCamera.m_XAxis.m_InputAxisValue = 0f;
            freeLookVirtualCamera.m_YAxis.m_InputAxisValue = 0f;
        }

        private void OnEnableMouseControlCamera()
        {
            isRightMouseButtonPressed = true;

            // Lock the cursor to the center of the screen and hide it
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            StartCoroutine(DisableMouseForFrame());
        }

        private IEnumerator DisableMouseForFrame()
        {
            cameraMovementLock = true;
            yield return new WaitForEndOfFrame();
            cameraMovementLock = false;
        }
    }
}
