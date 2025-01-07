using UnityEngine;
using UnityEngine.UI;

namespace AtticAdventures
{
    public class InteractionIndicator : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;
        [SerializeField] private Image indicatorImage;
        [SerializeField] private Vector3 offset;

        [SerializeField] private float fadeDistance = 5f;
        [SerializeField] private float fadeSpeed = 0.5f;
        [SerializeField] private Transform playerTransform;

        private Canvas canvas;
        private RectTransform canvasRectTransform;
        private RectTransform indicatorRectTransform;
        private CanvasGroup canvasGroup;
        private bool isVisible = false;

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            if (canvas == null)
            {
                return;
            }

            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }

            canvasRectTransform = canvas.GetComponent<RectTransform>();

            if (indicatorImage != null)
            {
                indicatorRectTransform = indicatorImage.GetComponent<RectTransform>();
                indicatorImage.gameObject.SetActive(false);
            }          
        }

        private void Update()
        {
            if (playerTransform == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    playerTransform = player.transform;
                }
            }

            if (isVisible && targetTransform != null && indicatorImage != null)
            {
                Vector3 worldPosition = targetTransform.position + offset;
                Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);

                if (screenPos.z > 0)
                {
                    Vector2 canvasPos;
                    bool isOnScreen = RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        canvasRectTransform,
                        screenPos,
                        canvas.worldCamera,
                        out canvasPos
                    );

                    if (isOnScreen)
                    {
                        indicatorRectTransform.localPosition = canvasPos;
                        indicatorImage.gameObject.SetActive(true);
                    }
                    else
                    {
                        indicatorImage.gameObject.SetActive(false);
                    }
                }
                else
                {
                    indicatorImage.gameObject.SetActive(false);
                }

                HandleFade();
            }
        }

        private void HandleFade()
        {
            if (targetTransform == null || playerTransform == null)
            {
                return;
            }

            float distance = Vector3.Distance(playerTransform.position, targetTransform.position);
            float targetAlpha = distance <= fadeDistance ? 1f : 0f;
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
        }

        public void ShowIndicator()
        {
            if (indicatorImage != null)
            {
                isVisible = true;
                indicatorImage.gameObject.SetActive(true);
            }
        }

        public void HideIndicator()
        {
            if (indicatorImage != null)
            {
                isVisible = false;
                indicatorImage.gameObject.SetActive(false);
            }
        }

        public void SetTargetTransform(Transform target)
        {
            targetTransform = target;
        }
    }
}
