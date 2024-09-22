using System.Collections;
using UnityEngine;

public class Mortar : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] Transform shootPoint;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float height = 10f;
    [SerializeField] float targetHeightOffset = 10f;
    [SerializeField] int numberOfPrefabsToSpawn = 3;
    [SerializeField] float spawnRadius = 2f;
    [SerializeField] GameObject explosionPrefab;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Shoot()
    {
        if (player == null) return;

        Vector3 targetPosition = player.transform.position + Vector3.up * targetHeightOffset;

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        StartCoroutine(MoveProjectileInArc(projectile, targetPosition));
    }

    private IEnumerator MoveProjectileInArc(GameObject projectile, Vector3 target)
    {
        Vector3 startPosition = projectile.transform.position;
        float distance = Vector3.Distance(startPosition, target);
        float flightDuration = distance / projectileSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < flightDuration)
        {
            elapsedTime += Time.deltaTime;

            float progress = elapsedTime / flightDuration;

            Vector3 currentPosition = Vector3.Lerp(startPosition, target, progress);

            float arcHeight = height * 4 * (progress - progress * progress);
            currentPosition.y += arcHeight;

            projectile.transform.position = currentPosition;

            projectile.transform.LookAt(target);

            yield return null;
        }

        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, projectile.transform.position, Quaternion.identity);
        }

        Destroy(projectile);

        for (int i = 0; i < numberOfPrefabsToSpawn; i++)
        {
            Vector3 spawnPosition = target + Random.insideUnitSphere * spawnRadius;
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
