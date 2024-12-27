using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;

public class SplineAnimateEvents : MonoBehaviour
{
    [SerializeField] private SplineAnimate splineAnimate;
    [SerializeField] private float threshold1;
    [SerializeField] private float threshold2;
    [SerializeField] private float threshold3;

    public UnityEvent onThreshold1Reached;
    public UnityEvent onThreshold2Reached;
    public UnityEvent onThreshold3Reached;

    private bool hasTriggered1;
    private bool hasTriggered2;
    private bool hasTriggered3;

    private void Update()
    {
        float currentTime = splineAnimate.ElapsedTime;

        if (!hasTriggered1 && currentTime >= threshold1)
        {
            hasTriggered1 = true;
            onThreshold1Reached.Invoke();
        }

        if (!hasTriggered2 && currentTime >= threshold2)
        {
            hasTriggered2 = true;
            onThreshold2Reached.Invoke();
        }

        if (!hasTriggered3 && currentTime >= threshold3)
        {
            hasTriggered3 = true;
            onThreshold3Reached.Invoke();
        }
    }
}
