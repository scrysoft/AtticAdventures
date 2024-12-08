using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Character.Abilities;
using Opsive.UltimateCharacterController.Traits;
using UnityEngine;
using UnityEngine.Events;

namespace AtticAdventures
{
    public class PickUpPowerArms : MonoBehaviour, IInteractableTarget
    {
        private bool isInteractable = true;
        private ModelManager modelManager;

        public UnityEvent OnInteracted;

        private void Start()
        {
            modelManager = FindAnyObjectByType<ModelManager>();
        }

        public bool CanInteract(GameObject character, Interact interactAbility)
        {
            return isInteractable;
        }

        public void Interact(GameObject character, Interact interactAbility)
        {
            if (!isInteractable) return;

            isInteractable = false;

            if (modelManager != null)
            {
                modelManager.ActiveModel = modelManager.AvailableModels[1];
            }

            OnInteracted?.Invoke();
        }
    }
}
