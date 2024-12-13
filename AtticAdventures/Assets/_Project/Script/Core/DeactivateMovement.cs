using Opsive.UltimateCharacterController.Character;
using UnityEngine;

namespace AtticAdventures
{
    public class DeactivateMovement : MonoBehaviour
    {
        UltimateCharacterLocomotion playerLocomotion;

        public void ToggleMovement(bool value)
        {
            if (playerLocomotion == null)
            {
                playerLocomotion = FindAnyObjectByType<UltimateCharacterLocomotion>();
            }

            if (playerLocomotion != null)
            {
                playerLocomotion.enabled = value;
            }
        }

    }
}
