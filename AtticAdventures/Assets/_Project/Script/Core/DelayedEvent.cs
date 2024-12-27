using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class DelayedEvent : MonoBehaviour
{
    [Tooltip("Das UnityEvent, das nach Ablauf des Timers aufgerufen wird.")]
    public UnityEvent onDelayFinished;


    public void StartDelayedEvent(float delayInSeconds)
    {
        StartCoroutine(InvokeEventAfterDelay(delayInSeconds));
    }


    private IEnumerator InvokeEventAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        onDelayFinished.Invoke();
    }
}
