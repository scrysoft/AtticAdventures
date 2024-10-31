using System.Collections;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public AnimationCurve easeCurve;
    public float duration = 2.0f;

    public void RotateOverTime()
    {
        StartCoroutine(RotateCoroutine());
    }

    private IEnumerator RotateCoroutine()
    {
        float timeElapsed = 0f;
        Quaternion startRotation = transform.localRotation;
        Quaternion targetRotation = Quaternion.Euler(-90f, 0f, 0f) * startRotation;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            float easedT = easeCurve.Evaluate(t);

            transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, easedT);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = targetRotation;
    }
}
