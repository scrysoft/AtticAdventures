using UnityEngine;

namespace AtticAdventures.SpawnSystem
{
    public class LinearSpawnPointStrategy : ISpawnPointStrategy
    {
        private int index = 0;
        private Transform[] spawnPoints;

        public LinearSpawnPointStrategy(Transform[] spawnPoints)
        {
            this.spawnPoints = spawnPoints;
        }

        public Transform NextSpawnPoint()
        {
            Transform result = spawnPoints[index];
            index = (index + 1) % spawnPoints.Length;
            return result;
        }
    }
}
