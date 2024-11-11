using UnityEngine;

namespace AtticAdventures.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private int spawnPointIndex = 0;

        public int Score { get; private set; }

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

        public void SetSpawnPointIndex(int value)
        {
            spawnPointIndex = value;
        }

        public int GetSpawnPointIndex()
        {
            return spawnPointIndex;
        }

        public void AddScore(int score)
        {
            Score += score;
        }
    }
}
