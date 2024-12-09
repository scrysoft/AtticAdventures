using Opsive.UltimateCharacterController.Character.Abilities;
using Opsive.UltimateCharacterController.Items.Actions;
using Opsive.UltimateCharacterController.Traits;

using UnityEngine;
using UnityEngine.Events;

namespace AtticAdventures
{
    public class PickUpBattery : MonoBehaviour, IInteractableTarget
    {
        private bool isInteractable = true;

        public UnityEvent OnInteracted;

        Attribute energy;
        float maxEnergy;

        private void Start()
        {
            AttributeManager attributeManager = FindAnyObjectByType<MagicAction>().GetComponent<AttributeManager>();
            energy = attributeManager.GetAttribute("Energy");
            maxEnergy = energy.MaxValue;
        }

        public bool CanInteract(GameObject character, Interact interactAbility)
        {
            return isInteractable;
        }

        public void Interact(GameObject character, Interact interactAbility)
        {
            if (!isInteractable) return;


            if(maxEnergy != energy.Value)
            {
                energy.Value += 1;
                isInteractable = false;

                OnInteracted?.Invoke();

                if (transform.parent != null)
                {
                    transform.parent.gameObject.SetActive(false);
                }
            }

        }
    }
}
