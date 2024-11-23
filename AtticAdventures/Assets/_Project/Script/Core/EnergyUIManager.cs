using AtticAdventures.EventSystem;
using Opsive.UltimateCharacterController.Traits;
using UnityEngine;

namespace AtticAdventures
{
    public class EnergyUIManager : MonoBehaviour
    {
        public FloatEventChannel floatEventChannel;
        public AttributeManager attributeManager;
        private Attribute energyAttribute;

        private float previousValue;

        private void Start()
        {
            energyAttribute = attributeManager.GetAttribute("Energy");

            if (energyAttribute != null)
            {
                previousValue = energyAttribute.Value;
            }
        }

        private void Update()
        {
            if (energyAttribute == null) return;

            float currentValue = energyAttribute.Value;

            if (!Mathf.Approximately(currentValue, previousValue))
            {
                HandleEnergyChanged(currentValue);
                previousValue = currentValue;
            }
        }

        private void HandleEnergyChanged(float currentValue)
        {
            floatEventChannel.Invoke(currentValue);
        }
    }
}
