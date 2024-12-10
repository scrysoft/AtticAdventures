using UnityEngine;
using UnityEngine.Events;

namespace AtticAdventures
{
    public class AnimationTriggerEventsChipArms : MonoBehaviour
    {
        public UnityEvent OnEventOneTriggered;
        public UnityEvent OnEventTwoTriggered;

        public void TriggerEventOne()
        {
            OnEventOneTriggered?.Invoke();
        }

        public void TriggerEventTwo()
        {
            OnEventTwoTriggered?.Invoke();
        }
    }
}
