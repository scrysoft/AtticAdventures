using UnityEngine;

public class VideoOnAnimator : MonoBehaviour
{
    [SerializeField] private Material materialInstance;
    [SerializeField] private float transitionDuration = 1f;
    private float currentValue;
    private float targetValue;
    private float timePassed;
    private bool isAnimating;

    private void Awake()
    {
        materialInstance = GetComponent<MeshRenderer>().material;

        if (materialInstance != null)
        {
            currentValue = materialInstance.GetFloat("_VideoOn");
            targetValue = currentValue;
        }
    }

    public void SetVideoOn(float newValue)
    {
        if (materialInstance == null) return;
        targetValue = Mathf.Clamp01(newValue);
        timePassed = 0f;
        isAnimating = true;
    }

    private void Update()
    {
        if (!isAnimating || materialInstance == null) return;
        timePassed += Time.deltaTime;
        float t = Mathf.Clamp01(timePassed / transitionDuration);
        t = t * t * (3f - 2f * t);
        float newCurrentValue = Mathf.Lerp(currentValue, targetValue, t);
        materialInstance.SetFloat("_VideoOn", newCurrentValue);
        if (Mathf.Approximately(newCurrentValue, targetValue))
        {
            newCurrentValue = targetValue;
            isAnimating = false;
        }
        currentValue = newCurrentValue;
    }
}
