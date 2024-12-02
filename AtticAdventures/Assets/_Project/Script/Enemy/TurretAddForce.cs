using UnityEngine;

public class TurretAddForce : MonoBehaviour
{
    public float force = 500f;
    public float drehmoment = 100f;

    public void ShotInTheAir()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(Vector3.up * force);

            Vector3 zufälligesDrehmoment = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ) * drehmoment;

            rb.AddTorque(zufälligesDrehmoment, ForceMode.Impulse);
        }
    }
}
