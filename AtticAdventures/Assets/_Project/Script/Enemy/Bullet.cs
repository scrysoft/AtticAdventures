using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffectPlayerPrefab;
    public GameObject decalPrefab;
    public float lifetime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Opsive.Shared.Game.ObjectPoolBase.Instantiate(hitEffectPlayerPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        else
        {
            Opsive.Shared.Game.ObjectPoolBase.Instantiate(decalPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
