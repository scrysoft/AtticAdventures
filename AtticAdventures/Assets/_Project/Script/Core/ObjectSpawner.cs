using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AtticAdventures
{
    public class ObjectSpawner : MonoBehaviour
    {
        public GameObject[] objectsToSpawn;
        public int numberOfObjectsToSpawn = 10;
        public float spawnRadius = 5f;
        public float forceMagnitude = 10f;

        void Start()
        {
            SpawnObjects();
        }

        void SpawnObjects()
        {
            for (int i = 0; i < numberOfObjectsToSpawn; i++)
            {
                GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

                Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;
                randomPosition.y = transform.position.y;

                GameObject spawnedObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);

                Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;
                    rb.AddForce(randomDirection * forceMagnitude, ForceMode.Impulse);
                }
            }
        }
    }
}
