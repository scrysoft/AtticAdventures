using System.Collections;
using UnityEngine;

public class ShardDeactivator : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;
    [SerializeField] float fallSpeed = 0.5f;
    private float deactivateDelay;
    [SerializeField] float minimumDelay = 2f;
    [SerializeField] float maximumDelay = 3f;
    [SerializeField] float destroyDelay = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        deactivateDelay = Random.Range(minimumDelay, maximumDelay);

        StartCoroutine(DeactivateRigidbodyAndCollider());
    }

    IEnumerator DeactivateRigidbodyAndCollider()
    {
        yield return new WaitForSeconds(deactivateDelay);

        if (rb != null) rb.isKinematic = true;
        if (col != null) col.enabled = false;

        StartCoroutine(MoveDown());
    }

    IEnumerator MoveDown()
    {
        float elapsedTime = 0f;

        while (elapsedTime < destroyDelay)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
