using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlCollectibleTrackerUI : MonoBehaviour
{
    public static OwlCollectibleTrackerUI Instance { get; private set; }

    [SerializeField]
    private List<GameObject> owls = new List<GameObject>();

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private float fadeDuration = 1f;
    [SerializeField]
    private float visibleDuration = 2f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if(canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        DisableAllOwls();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
        }
    }

    private void DisableAllOwls()
    {
        foreach (var owl in owls)
        {
            if (owl != null)
                owl.SetActive(false);
        }
    }

    public void ActivateOwl(int index)
    {
        if (index < 0 || index >= owls.Count || owls[index] == null)
        {
            return;
        }

        owls[index].SetActive(true);

        if (canvasGroup != null)
        {
            StartCoroutine(FadeInAndOut());
        }
    }

    private IEnumerator FadeInAndOut()
    {
        yield return FadeCanvasGroup(0, 1, fadeDuration);

        yield return new WaitForSeconds(visibleDuration);

        yield return FadeCanvasGroup(1, 0, fadeDuration);
    }

    private IEnumerator FadeCanvasGroup(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}
