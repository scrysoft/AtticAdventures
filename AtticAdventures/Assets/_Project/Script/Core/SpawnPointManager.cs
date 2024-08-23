using Opsive.UltimateCharacterController.Character;
using UnityEngine;

namespace AtticAdventures.Core
{
    public class SpawnPointManager : MonoBehaviour
    {
        [SerializeField] Transform[] spawnPoints = null;

        [SerializeField] Transform player = null;
        [SerializeField] Transform respawnPoint = null;

        private void Start()
        {
            int spawnPointIndex = GameManager.Instance.GetSpawnPointIndex();

            Vector3 spawnPoint = spawnPoints[spawnPointIndex].position;
            respawnPoint.position = spawnPoint;

            if (player != null)
            {
                player.GetComponent<UltimateCharacterLocomotion>().SetPosition(spawnPoint);
            }
        }

        
    }
}
