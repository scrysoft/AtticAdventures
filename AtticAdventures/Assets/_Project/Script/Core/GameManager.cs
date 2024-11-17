using DG.Tweening;
using UnityEngine;

namespace AtticAdventures.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private int spawnPointIndex = 0;
        private int tweeners = 3125;
        private int sequences = 50;

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

        private void Start()
        {
            // Set max Tweeners and max Sequences for animation purposes
            SetMaxTweens(tweeners, sequences);
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

        private void SetMaxTweens(int tweeners, int sequences)
        {
            DOTween.SetTweensCapacity(tweeners, sequences);
        }
    }
}
