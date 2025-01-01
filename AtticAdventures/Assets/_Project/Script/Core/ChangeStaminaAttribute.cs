using Opsive.UltimateCharacterController.Traits;
using UnityEngine;

namespace AtticAdventures
{
    public class ChangeStaminaAttribute : MonoBehaviour
    {
        public CharacterAttributeManager characterAttributeManager;
        public RectTransform background;
        public RectTransform staminaBar;

        public void SetStaminaMaxValue(float value)
        {
            Attribute stamina = characterAttributeManager.GetAttribute("Stamina");
            stamina.MaxValue = value;
        }

        public void SetStaminaBarWidth(float width)
        {
            if (staminaBar != null)
            {
                staminaBar.localScale = new Vector3(width, staminaBar.localScale.y, staminaBar.localScale.z);
            }
        }

        public void SetStaminaBackgroundWidth(float width)
        {
            if (background != null)
            {
                background.localScale = new Vector3(width, background.localScale.y, background.localScale.z);
            }
        }
    }
}
