using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    public float forceMultiplier = 10f;
    public Vector3 forceDirection = Vector3.up;
    public float forceHeightOffset = 1f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ApplyForceToObject()
    {
        if (rb != null)
        {
            Vector3 forcePosition = transform.position + Vector3.up * forceHeightOffset;

            rb.AddForceAtPosition(forceDirection.normalized * forceMultiplier, forcePosition, ForceMode.Impulse);

            Debug.DrawLine(forcePosition, forcePosition + forceDirection.normalized * forceMultiplier, Color.red, 2f);
        }
    }
}
