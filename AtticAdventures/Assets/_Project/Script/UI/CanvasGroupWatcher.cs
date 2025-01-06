using UnityEngine;
using UnityEngine.Events;

public class CanvasGroupAlphaWatcher : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private UnityEvent onAlphaOne;

    private bool eventFired = false;

    private void Update()
    {
        if (canvasGroup == null) return;

        if (!eventFired && Mathf.Approximately(canvasGroup.alpha, 1f))
        {
            eventFired = true;
            onAlphaOne.Invoke();
        }

        if (eventFired && !Mathf.Approximately(canvasGroup.alpha, 1f))
        {
            eventFired = false;
        }
    }
}
