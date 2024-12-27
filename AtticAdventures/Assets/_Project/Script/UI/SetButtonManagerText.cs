using Michsky.UI.Dark;
using UnityEngine;

namespace AtticAdventures
{
    public class SetButtonManagerText : MonoBehaviour
    {
        [SerializeField] ButtonManager buttonManager;

        private void OnEnable()
        {
            buttonManager = GetComponent<ButtonManager>();
        }

        public void SetText(string value)
        {
            buttonManager.buttonText = value;
            buttonManager.UpdateUI();
        }
    }
}
