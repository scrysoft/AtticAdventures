using UnityEngine;
using UnityEngine.Events;

public class GrenadeFragment : MonoBehaviour
{
    public GameObject explosion;
    public GameObject groundDecay;
    public GameObject decalProjector;

    public UnityEvent onCollisionEvent;
    public Transform destructionParent;

    private void Start()
    {
        if(decalProjector != null) 
        {
            decalProjector.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Enemy")) return;

        decalProjector.SetActive(false);

        Vector3 spawnPosition = collision.contacts[0].point;

        Opsive.Shared.Game.ObjectPoolBase.Instantiate(explosion, spawnPosition, Quaternion.identity);

        RaycastHit hit;
        if (Physics.Raycast(spawnPosition + Vector3.up * 2f, Vector3.down, out hit))
        {
            spawnPosition = hit.point;
        }

        Quaternion upwardRotation = Quaternion.Euler(90f, 0f, 0f);
        Instantiate(groundDecay, spawnPosition, upwardRotation);

        onCollisionEvent.Invoke();

        DestroyUpToParent();
    }

    void DestroyUpToParent()
    {
        Transform current = transform;

        while (current != null && current != destructionParent)
        {
            Transform nextParent = current.parent;
            Destroy(current.gameObject);
            current = nextParent;
        }
    }
}
