using Opsive.UltimateCharacterController.Character.Abilities;
using Opsive.UltimateCharacterController.Traits;
using UnityEngine;

namespace AtticAdventures
{
    public class InteractionAnchor : MonoBehaviour, IInteractableTarget
    {
        InteractionIndicator indicator;

        private bool canInteract = true;

        private void Start()
        {
            indicator = FindAnyObjectByType<InteractionIndicator>();
        }

        public bool CanInteract(GameObject character, Interact interactAbility)
        {
            if (canInteract)
            {
                indicator.SetTargetTransform(transform);
                indicator.ShowIndicator();
                return true;

            }
            else
            {
                indicator.HideIndicator();
                return false;
            }

        }

        public void Interact(GameObject character, Interact interactAbility)
        {
            indicator.HideIndicator();
        }
    }
}
