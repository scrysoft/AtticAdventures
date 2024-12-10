using UnityEngine;

public class UpDownMovement : MonoBehaviour
{
    private float amplitude = 0.3f;
    private float speed = 1f;

    private float deactivationTime = 3f;

    public GameObject spawnPrefab;

    private Vector3 startPosition;
    private float timer = 0f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(timer * speed * Mathf.PI) * amplitude;
        transform.position = startPosition + new Vector3(0, yOffset, 0);

        timer += Time.deltaTime;

        if (timer >= deactivationTime)
        {
            SpawnPrefab();
            gameObject.SetActive(false);
        }
    }

    void SpawnPrefab()
    {
        if (spawnPrefab != null)
        {
            Vector3 spawnPosition = startPosition != null ? startPosition : transform.position;
            Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
