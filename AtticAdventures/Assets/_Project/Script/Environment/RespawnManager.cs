using UnityEngine;
using System.Collections.Generic;

namespace AtticAdventures
{
    public class RespawnManager : MonoBehaviour
    {
        public static RespawnManager Instance { get; private set; }

        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetSpawnPoint(int index)
        {
            if (spawnPoints == null || spawnPoints.Count == 0)
            {
                return;
            }

            if (index < 0 || index >= spawnPoints.Count)
            {
                return;
            }

            spawnPoint.position = spawnPoints[index].position;
        }
    }
}
