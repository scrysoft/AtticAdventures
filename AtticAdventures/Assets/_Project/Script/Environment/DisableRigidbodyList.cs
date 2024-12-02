using UnityEngine;

public class RigidbodyController : MonoBehaviour
{
    public float forceAmount = 10f;

    public void DisableKinematicInChildren()
    {
        foreach (Transform child in transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = false;

                Vector3 forceDirection = Vector3.back;
                rb.AddForce(forceDirection * forceAmount, ForceMode.Impulse);
            }
        }
    }
}
