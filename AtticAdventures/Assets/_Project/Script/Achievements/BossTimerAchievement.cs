using AtticAdventures.Core;
using RealSoftGames.AdvancedAchievementSystem;
using UnityEngine;

namespace AtticAdventures
{
    public class BossTimerAchievement : MonoBehaviour
    {
        public void Achievement()
        {
            AchievementManager.Instance.UpdateAchievement(AchievementList.Coup_in_a_Flash, OnAchievementCompleteCallback: OnAchievementComplete);
        }

        private void OnAchievementComplete()
        {
            Debug.Log($"You defeated the Final Boss of the Chapter in under 1 Minute.");
            WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Coup_in_a_Flash.ToString()}");
            GameManager.Instance.RecordPosiition($"{AchievementList.Coup_in_a_Flash.ToString()}");

        }
    }
}
