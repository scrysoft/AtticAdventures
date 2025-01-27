using AtticAdventures.Core;
using System.Collections;
using TMPro;
using UnityEngine;

namespace AtticAdventures.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;

        private void Start()
        {
            UpdateScore();
        }

        public void UpdateScore()
        {
            StartCoroutine(UpdateScoreNextFrame());
        }

        private IEnumerator UpdateScoreNextFrame()
        {
            yield return null;
            scoreText.text = GameManager.Instance.Score.ToString();
        }
    }
}
