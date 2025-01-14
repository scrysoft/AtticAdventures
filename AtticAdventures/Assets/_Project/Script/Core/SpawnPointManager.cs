using Opsive.UltimateCharacterController.Character;
using UnityEngine;

namespace AtticAdventures.Core
{
    public class SpawnPointManager : MonoBehaviour
    {
        [SerializeField] Transform[] spawnPoints = null;

        [SerializeField] Transform player = null;
        [SerializeField] Transform respawnPoint = null;

        private int spawnPointIndex = 0;

        private void Start()
        {
            spawnPointIndex = GameManager.Instance.GetSpawnPointIndex();

            if (spawnPoints == null || spawnPoints.Length <= spawnPointIndex || spawnPoints[spawnPointIndex] == null)
            {
                return;
            }

            Vector3 spawnPoint = spawnPoints[spawnPointIndex].position;

            if (respawnPoint != null)
            {
                respawnPoint.position = spawnPoint;
            }

            if (player != null)
            {
                var locomotion = player.GetComponent<UltimateCharacterLocomotion>();
                if (locomotion != null)
                {
                    locomotion.SetPosition(spawnPoint);
                }
            }
        }
    }
}