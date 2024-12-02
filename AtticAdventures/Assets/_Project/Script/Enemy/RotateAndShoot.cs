using UnityEngine;
using UnityEngine.Events;

public class RotateAndShoot : MonoBehaviour
{
    private Transform player;
    public UnityEvent onShoot;
    public LayerMask lineOfSightMask;
    public float shootInterval = 5f;
    public Transform rayCastStartingPoint;

    private bool shouldRotate = false;
    private bool isActive = true; // Steuerung der Skript-Aktivität
    private float shootTimer = 0f;

    public void StartRotationAndShoot(bool start, Transform playerTransform)
    {
        if (!isActive) return; // Keine Aktionen, wenn inaktiv

        shouldRotate = start;
        player = playerTransform;
    }

    void Update()
    {
        if (!isActive || !shouldRotate || player == null) return;

        RotateTowardsPlayer();
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            shootTimer = 0f;
            CheckAndShoot();
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
        }
    }

    private void CheckAndShoot()
    {
        Vector3 startPosition = rayCastStartingPoint.position;
        Vector3 direction = (player.position + new Vector3(0, 1f, 0) - startPosition).normalized;

        Debug.DrawRay(startPosition, direction * 100f, Color.red, 1f);

        if (Physics.Raycast(startPosition, direction, out RaycastHit hit, Mathf.Infinity, lineOfSightMask))
        {
            if (hit.transform.CompareTag("Player"))
            {
                onShoot?.Invoke();
            }
        }
    }

    public void StopRotationAndShoot()
    {
        isActive = false;
        shouldRotate = false;
        player = null;
    }

    public void ResumeRotationAndShoot()
    {
        isActive = true;
    }
}
