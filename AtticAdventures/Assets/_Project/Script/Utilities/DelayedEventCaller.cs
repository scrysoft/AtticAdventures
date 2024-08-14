using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AtticAdventures.Utilities
{
    public class DelayedEventCaller : MonoBehaviour
    {
        public float delayTime = 0f;

        public UnityEvent onEventTriggered;

        void Start()
        {
            StartCoroutine(TriggerEventAfterDelay(delayTime));
        }

        IEnumerator TriggerEventAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            onEventTriggered.Invoke();
        }
    }

}
