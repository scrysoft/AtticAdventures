using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AutomaticDoor : MonoBehaviour
{
    public AnimationCurve easeCurve;
    public float duration = 2.0f;
    public Vector3 eulerRotation = new Vector3(-90f, 0f, 0f);
    public UnityEvent onTargetRotationReached;

    private bool isEnabled = true;

    public void RotateOverTime()
    {
        if (isEnabled)
        {
            StartCoroutine(RotateCoroutine());
        }
    }

    public void SetActivity(bool value)
    {
        isEnabled = value;
    }

    public void SetXRotation(float value)
    {
        eulerRotation.x = value;
    }

    public void SetYRotation(float value)
    {
        eulerRotation.y = value;
    }

    public void SetZRotation(float value)
    {
        eulerRotation.z = value;
    }

    private IEnumerator RotateCoroutine()
    {
        float timeElapsed = 0f;
        Quaternion startRotation = transform.localRotation;
        Quaternion targetRotation = Quaternion.Euler(eulerRotation) * startRotation;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            float easedT = easeCurve.Evaluate(t);

            transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, easedT);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = targetRotation;
        SetActivity(false);
        onTargetRotationReached?.Invoke();
    }
}
