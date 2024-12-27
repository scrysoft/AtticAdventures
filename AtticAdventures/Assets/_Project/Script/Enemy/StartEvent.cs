using UnityEngine;
using UnityEngine.Events;

public class StartEvent : MonoBehaviour
{
    [Tooltip("Dieses UnityEvent wird beim Start des GameObjects ausgel�st.")]
    public UnityEvent onStartEvent;

    private void Start()
    {
        onStartEvent?.Invoke();
    }
}
