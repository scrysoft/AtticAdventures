using Rewired;
using UnityEngine;

public class TargetResetAndFollow : MonoBehaviour
{
    [Header("Object References")]
    public GameObject targetObject;

    [Header("Offsets & Speeds")]
    public float heightOffset = 0f;
    public float followSpeed = 5f;
    public float resetSpeed = 2f;

    private Vector3 originalPosition;
    private bool isFollowing = false;
    private bool isResetting = false;
    private Transform playerTransform;

    private void Start()
    {
        if (targetObject != null)
        {
            originalPosition = targetObject.transform.position;
        }
    }

    private void Update()
    {
        if (isFollowing && playerTransform != null && targetObject != null)
        {
            Vector3 targetPosition = playerTransform.position;
            targetPosition.y += heightOffset;

            targetObject.transform.position = Vector3.Lerp(
                targetObject.transform.position,
                targetPosition,
                Time.deltaTime * followSpeed
            );
        }
        else if (isResetting && targetObject != null)
        {
            targetObject.transform.position = Vector3.Lerp(
                targetObject.transform.position,
                originalPosition,
                Time.deltaTime * resetSpeed
            );

            if (Vector3.Distance(targetObject.transform.position, originalPosition) < 0.01f)
            {
                targetObject.transform.position = originalPosition;
                isResetting = false;
            }
        }
    }

    public void StartFollowing()
    {
        if (targetObject == null) return;

        if (playerTransform == null)
        {
            GameObject pObj = GameObject.FindGameObjectWithTag("Player");
            if (pObj != null) playerTransform = pObj.transform;
            else return;
        }

        isFollowing = true;
        isResetting = false;
    }

    public void StopFollowing()
    {
        if (targetObject == null) return;

        isFollowing = false;
        isResetting = true;
        playerTransform = null;
    }
}
