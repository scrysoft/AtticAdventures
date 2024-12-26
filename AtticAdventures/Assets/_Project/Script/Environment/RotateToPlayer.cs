using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class RotateToPlayer : MonoBehaviour
{
    public Transform targetObject;
    public float rotationDuration = 1f;
    public AnimationCurve easeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public UnityEvent onRotateComplete;
    public UnityEvent onResetComplete;

    private Quaternion originalRotation;
    private Transform player;
    private Coroutine rotateCoroutine;

    void Start()
    {
        if (targetObject == null) targetObject = transform;
        originalRotation = targetObject.rotation;
    }

    public void RotateTowardsPlayer()
    {
        if (player == null)
        {
            GameObject pObj = GameObject.FindGameObjectWithTag("Player");
            if (pObj != null) player = pObj.transform;
            else return;
        }

        if (rotateCoroutine != null) StopCoroutine(rotateCoroutine);
        rotateCoroutine = StartCoroutine(RotateTowardsPlayerCoroutine());
    }

    IEnumerator RotateTowardsPlayerCoroutine()
    {
        Quaternion startRotation = targetObject.rotation;
        Vector3 direction = (player.position - targetObject.position).normalized;
        Quaternion endRotation = Quaternion.LookRotation(direction, Vector3.up);

        float elapsed = 0f;
        while (elapsed < rotationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / rotationDuration);
            float curveValue = easeCurve.Evaluate(t);
            targetObject.rotation = Quaternion.Slerp(startRotation, endRotation, curveValue);
            yield return null;
        }

        targetObject.rotation = endRotation;
        rotateCoroutine = null;
        onRotateComplete?.Invoke();
    }

    public void ResetRotation()
    {
        if (rotateCoroutine != null) StopCoroutine(rotateCoroutine);
        rotateCoroutine = StartCoroutine(ResetRotationCoroutine());
    }

    IEnumerator ResetRotationCoroutine()
    {
        Quaternion startRotation = targetObject.rotation;
        float elapsed = 0f;
        while (elapsed < rotationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / rotationDuration);
            float curveValue = easeCurve.Evaluate(t);
            targetObject.rotation = Quaternion.Slerp(startRotation, originalRotation, curveValue);
            yield return null;
        }

        targetObject.rotation = originalRotation;
        rotateCoroutine = null;
        onResetComplete?.Invoke();
    }
}
