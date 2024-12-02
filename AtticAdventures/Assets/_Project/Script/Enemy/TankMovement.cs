using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float patrolDistance = 5f;

    private Vector3 originPosition;
    private Transform player;
    private bool playerInZone = false;
    private bool isActive = true;

    private void Start()
    {
        originPosition = transform.position;
    }

    private void Update()
    {
        if (!isActive) return;

        if (playerInZone && player != null)
        {
            FollowPlayerAlongLocalXAxis();
        }
        else
        {
            ReturnToOrigin();
        }
    }

    private void FollowPlayerAlongLocalXAxis()
    {
        Vector3 playerLocalPosition = transform.InverseTransformPoint(player.position);
        float relativePlayerX = playerLocalPosition.x;

        float clampedX = Mathf.Clamp(relativePlayerX, -patrolDistance, patrolDistance);
        Vector3 targetWorldPosition = originPosition + transform.right * clampedX;

        if (Vector3.Distance(transform.position, targetWorldPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWorldPosition, movementSpeed * Time.deltaTime);
        }
    }

    private void ReturnToOrigin()
    {
        if (Vector3.Distance(transform.position, originPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originPosition, movementSpeed * Time.deltaTime);
        }
    }

    public void SetPlayerInZone(bool inZone, Transform playerTransform)
    {
        if (!isActive) return;

        playerInZone = inZone;
        player = playerTransform;
    }

    public void StopMovement()
    {
        isActive = false;
        playerInZone = false;
        player = null;
    }

    public void ResumeMovement()
    {
        isActive = true;
    }

    private void OnDrawGizmos()
    {
        if (!isActive) return;

        Gizmos.color = Color.green;

        Vector3 leftBound = transform.position + transform.right * -patrolDistance;
        Vector3 rightBound = transform.position + transform.right * patrolDistance;

        Gizmos.DrawLine(leftBound, rightBound);
        Gizmos.DrawWireSphere(leftBound, 0.2f);
        Gizmos.DrawWireSphere(rightBound, 0.2f);
    }
}
