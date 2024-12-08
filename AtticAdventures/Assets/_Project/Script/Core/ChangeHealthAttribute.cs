using Opsive.UltimateCharacterController.Traits;
using UnityEngine;

namespace AtticAdventures
{
    public class ChangeHealthAttribute : MonoBehaviour
    {
        public CharacterAttributeManager characterAttributeManager;
        public RectTransform background;
        public RectTransform healthBar;

        public void SetHealthMaxValue(float value)
        {
            Attribute health = characterAttributeManager.GetAttribute("Health");
            health.MaxValue = value;
        }

        public void SetHealthBarWidth(float width)
        {
            if (healthBar != null)
            {
                healthBar.localScale = new Vector3(width, healthBar.localScale.y, healthBar.localScale.z);
            }
        }

        public void SetHealthBackgroundWidth(float width)
        {
            if (background != null)
            {
                background.localScale = new Vector3(width, background.localScale.y, background.localScale.z);
            }
        }
    }
}
