using UnityEngine;

namespace AtticAdventures
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] CanvasGroup canvasGroup;

        public CanvasGroup GetCanvasGroup()
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetCanvasGroup();
            }

            return canvasGroup;
        }
    }
}
