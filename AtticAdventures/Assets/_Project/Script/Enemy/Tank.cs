using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour
{
    public GameObject decalPrefab;
    public float spawnDelay = 2f;
    public GameObject explosionPrefab;

    private Transform player;
    private Coroutine shootCoroutine;
    private bool allowShooting = true;

    public void Shoot()
    {
        if (!allowShooting) return;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (shootCoroutine == null)
        {
            shootCoroutine = StartCoroutine(ShootCoroutine(player.position));
        }
    }

    public void StopShoot()
    {
        allowShooting = false;
    }

    private IEnumerator ShootCoroutine(Vector3 playerPosition)
    {
        GameObject decalProjector = Instantiate(decalPrefab, playerPosition, Quaternion.identity);
        decalProjector.transform.GetChild(1).gameObject.SetActive(true);

        yield return new WaitForSeconds(spawnDelay);

        Opsive.Shared.Game.ObjectPoolBase.Instantiate(explosionPrefab, playerPosition, Quaternion.identity);

        decalProjector.SetActive(false);

        shootCoroutine = null;

        if (!allowShooting)
        {
            yield break;
        }
    }
}
