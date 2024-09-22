using UnityEngine;
using UnityEngine.Events;

namespace AtticAdventures
{
    public class ShootingEvents : MonoBehaviour
    {
        [SerializeField] Transform spawnPoint;
        [SerializeField] GameObject prefab;

        public UnityEvent onShoot;

        public void Shoot()
        {
            if (onShoot != null)
            {
                onShoot.Invoke();
            }
        }

        public void SpawnPrefab()
        {
            if (prefab != null && spawnPoint != null)
            {
                Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }
}
