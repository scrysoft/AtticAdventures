
using UnityEngine;

namespace AtticAdventures
{
    public class IncreaseStamina : MonoBehaviour
    {
        ChangeStaminaAttribute staminaAttribute;

        [ContextMenu("Increase Stamina")]
        public void IncreasStaminaValue()
        {
            staminaAttribute = FindAnyObjectByType<ChangeStaminaAttribute>();
            
            if (staminaAttribute == null) return;

            Debug.Log("TEST");

            staminaAttribute.SetStaminaMaxValue(150f);
            staminaAttribute.SetStaminaBarWidth(1.5f);
            staminaAttribute.SetStaminaBackgroundWidth(1.65f);
        }
    }
}
