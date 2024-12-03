using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    public float forceMultiplier = 10f;
    public Vector3 forceDirection = Vector3.up;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Kein Rigidbody gefunden! Bitte füge einen Rigidbody zu diesem Objekt hinzu.");
        }
    }

    public void ApplyForceToObject()
    {
        if (rb != null)
        {
            rb.AddForce(forceDirection.normalized * forceMultiplier, ForceMode.Impulse);
        }
    }
}
