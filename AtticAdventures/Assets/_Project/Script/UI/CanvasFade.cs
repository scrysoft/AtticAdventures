using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasFade : MonoBehaviour
{
    private Canvas canvas;
    private Camera mainCamera;
    private float distanceThreshold = 20f;
    private float fadeSpeed = 1f;

    private CanvasGroup canvasGroup;
    private RectTransform canvasRectTransform;
    private bool isVisible = true;

    void Start()
    {

        mainCamera = Camera.main;
        canvas = GetComponent<Canvas>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();

        canvasGroup = canvas.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = canvas.gameObject.AddComponent<CanvasGroup>();
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(mainCamera.transform.position, canvasRectTransform.position);

        bool shouldBeVisible = IsCanvasInView() && distance <= distanceThreshold;

        if (shouldBeVisible != isVisible)
        {
            isVisible = shouldBeVisible;
            StopAllCoroutines();
            StartCoroutine(FadeCanvas(isVisible ? 1f : 0f));
        }
    }

    bool IsCanvasInView()
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(canvasRectTransform.position);

        return screenPoint.x >= 0f && screenPoint.x <= 1f && screenPoint.y >= 0f && screenPoint.y <= 1f && screenPoint.z > 0f;
    }

    IEnumerator FadeCanvas(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeSpeed)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}
