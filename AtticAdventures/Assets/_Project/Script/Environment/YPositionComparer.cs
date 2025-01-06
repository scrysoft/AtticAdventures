using UnityEngine;
using UnityEngine.Events;

public class YPositionComparer : MonoBehaviour
{
    [Header("Zu prüfende Objekte")]
    [SerializeField] private Transform plane;
    [SerializeField] private Transform positionA;
    [SerializeField] private Transform positionB;

    [Header("Events")]
    [SerializeField] private UnityEvent onEnterNearA;
    [SerializeField] private UnityEvent onEnterNearB;
    [SerializeField] private UnityEvent onEnterBetween;

    [Header("Einstellungen")]
    [SerializeField] private float threshold = 0.2f;

    private enum PlaneState { NearA, NearB, Between }
    private PlaneState currentState = PlaneState.Between;

    void Update()
    {
        float distA = Mathf.Abs(plane.position.y - positionA.position.y);
        float distB = Mathf.Abs(plane.position.y - positionB.position.y);

        PlaneState newState;

        if (distA <= threshold)
        {
            newState = PlaneState.NearA;
        }
        else if (distB <= threshold)
        {
            newState = PlaneState.NearB;
        }
        else
        {
            newState = PlaneState.Between;
        }

        if (newState != currentState)
        {
            currentState = newState;

            switch (currentState)
            {
                case PlaneState.NearA:
                    onEnterNearA?.Invoke();
                    break;
                case PlaneState.NearB:
                    onEnterNearB?.Invoke();
                    break;
                case PlaneState.Between:
                    onEnterBetween?.Invoke();
                    break;
            }
        }
    }
}
