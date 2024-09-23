using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public GameObject rocketPrefab;
    public Transform spawnPoint;

    public void FireRocket()
    {
        if (rocketPrefab != null && spawnPoint != null)
        {
            Instantiate(rocketPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
