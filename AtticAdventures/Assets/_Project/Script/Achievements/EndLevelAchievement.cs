using AtticAdventures.Core;
using RealSoftGames.AdvancedAchievementSystem;
using UnityEngine;

namespace AtticAdventures
{
    public class EndLevelAchievement : MonoBehaviour
    {
        public void Achievement()
        {
            AchievementManager.Instance.UpdateAchievement(AchievementList.To_be_continued, OnAchievementCompleteCallback: OnAchievementComplete);
        }

        private void OnAchievementComplete()
        {
            Debug.Log($"You reached the Gate to the next level.");
            WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.To_be_continued.ToString()}");
            GameManager.Instance.RecordPosiition($"{AchievementList.To_be_continued.ToString()}");

        }
    }
}
