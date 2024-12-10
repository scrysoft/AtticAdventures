using UnityEngine;
using UnityEngine.Events;

namespace AtticAdventures
{
    public class AnimationTriggerEventsChip : MonoBehaviour
    {
        public UnityEvent OnEventOneTriggered;
        public UnityEvent OnEventTwoTriggered;

        public void TriggerEventOneChip()
        {
            OnEventOneTriggered?.Invoke();
        }

        public void TriggerEventTwoChip()
        {
            OnEventTwoTriggered?.Invoke();
        }
    }
}
