using System.Collections;
using UnityEngine;

namespace AtticAdventures
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] CanvasGroup canvasGroup;
        private float fadeDuration = 0.2f;

        public CanvasGroup GetCanvasGroup()
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }

            return canvasGroup;
        }

        public IEnumerator FadeCanvasGroup(float targetAlpha)
        {
            if (canvasGroup == null)
            {
                yield break;
            }

            float startAlpha = canvasGroup.alpha;
            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
                yield return null;
            }

            canvasGroup.alpha = targetAlpha;
        }
    }
}
