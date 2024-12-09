using UnityEngine;
using UnityEngine.Events;

namespace AtticAdventures
{
    public class AnimationTriggerEvents : MonoBehaviour
    {
        public UnityEvent OnEventOneTriggered;
        public UnityEvent OnEventTwoTriggered;

        public void TriggerEventOne()
        {
            Debug.Log("TEST: Invincible");
            OnEventOneTriggered?.Invoke();
        }

        public void TriggerEventTwo()
        {
            Debug.Log("TEST: Not Invincible");
            OnEventTwoTriggered?.Invoke();
        }
    }
}
