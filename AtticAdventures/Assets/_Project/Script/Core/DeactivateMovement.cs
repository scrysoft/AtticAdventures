using Opsive.UltimateCharacterController.Camera;
using Opsive.UltimateCharacterController.Character;
using UnityEngine;

namespace AtticAdventures
{
    public class DeactivateMovement : MonoBehaviour
    {
        UltimateCharacterLocomotion playerLocomotion;
        CameraController cameraController;

        public void ToggleMovement(bool value)
        {
            if (playerLocomotion == null)
            {
                playerLocomotion = FindAnyObjectByType<UltimateCharacterLocomotion>();
            }

            if (cameraController == null)
            {
                cameraController = FindAnyObjectByType<CameraController>();    
            }

            if (playerLocomotion != null)
            {
                playerLocomotion.enabled = value;
            }

            if(cameraController != null)
            {
                cameraController.enabled = value;
            }
        }

    }
}
