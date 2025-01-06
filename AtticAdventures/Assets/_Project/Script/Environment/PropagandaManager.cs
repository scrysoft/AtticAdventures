using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PropagandaManager : MonoBehaviour
{
    public float minTime = 120f;
    public float maxTime = 300f;
    public UnityEvent onRandomIntervalEvent;

    private Coroutine eventCoroutine;
    private bool isRunning = false;


    private IEnumerator TriggerEventPeriodically()
    {
        while (true)
        {
            float randomDelay = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(randomDelay);
            onRandomIntervalEvent.Invoke();
        }
    }


    public void EnablePropaganda()
    {
        if (!isRunning)
        {
            eventCoroutine = StartCoroutine(TriggerEventPeriodically());
            isRunning = true;
        }
    }

    public void DisablePropaganda()
    {
        if (isRunning)
        {
            StopCoroutine(eventCoroutine);
            eventCoroutine = null;
            isRunning = false;
        }
    }
}
