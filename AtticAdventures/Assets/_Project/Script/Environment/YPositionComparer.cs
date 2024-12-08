using UnityEngine;
using UnityEngine.Events;

public class YPositionComparer : MonoBehaviour
{
    [SerializeField] private Transform objectA;
    [SerializeField] private Transform objectB;
    [SerializeField] private UnityEvent onConditionMet;

    private bool eventTriggered = false;

    void Update()
    {
        if (!eventTriggered && Mathf.Approximately(objectA.position.y, objectB.position.y))
        {
            eventTriggered = true;
            onConditionMet?.Invoke();
        }
    }
}
