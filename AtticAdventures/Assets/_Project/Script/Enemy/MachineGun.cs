using System.Collections;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject sfxPrefab;
    public Transform firePoint;
    public int numberOfShots = 10;
    public float timeBetweenShots = 0.1f;
    public float bulletSpeed = 20f;

    private bool isShooting = false;

    public void Shoot()
    {
        if (!isShooting)
        {
            StartCoroutine(FireShots());
        }
    }

    private IEnumerator FireShots()
    {
        isShooting = true;

        for (int i = 0; i < numberOfShots; i++)
        {
            FireSingleShot();
            yield return new WaitForSeconds(timeBetweenShots);
        }

        isShooting = false;
    }

    private void FireSingleShot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.up * bulletSpeed;

        Instantiate(sfxPrefab, firePoint.position, Quaternion.identity);
    }
}
