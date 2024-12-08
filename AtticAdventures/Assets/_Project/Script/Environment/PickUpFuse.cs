using Opsive.UltimateCharacterController.Character.Abilities;
using Opsive.UltimateCharacterController.Traits;
using UnityEngine;
using UnityEngine.Events;

namespace AtticAdventures
{
    public class PickUpFuse : MonoBehaviour, IInteractableTarget
    {
        private bool isInteractable = true;

        public UnityEvent OnInteracted;

        public bool CanInteract(GameObject character, Interact interactAbility)
        {
            return isInteractable;
        }

        public void Interact(GameObject character, Interact interactAbility)
        {
            if (!isInteractable) return;

            isInteractable = false;

            OnInteracted?.Invoke();
        }
    }
}
