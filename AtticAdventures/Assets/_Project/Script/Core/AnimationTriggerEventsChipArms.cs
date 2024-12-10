using UnityEngine;
using UnityEngine.Events;

namespace AtticAdventures
{
    public class AnimationTriggerEventsChipArms : MonoBehaviour
    {
        public UnityEvent OnEventOneTriggered;
        public UnityEvent OnEventTwoTriggered;

        public void TriggerEventOneChipArms()
        {
            OnEventOneTriggered?.Invoke();
        }

        public void TriggerEventTwoChipArms()
        {
            OnEventTwoTriggered?.Invoke();
        }
    }
}
