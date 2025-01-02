using AtticAdventures.Core;
using RealSoftGames.AdvancedAchievementSystem;
using UnityEngine;

namespace AtticAdventures
{
    public class EnemyAchievement : MonoBehaviour
    {
        public void Achievement()
        {
            AchievementManager.Instance.UpdateAchievementsByKey(AchievementKey.Enemy, OnAchievementCompleteCallback: OnAchievementComplete);
        }

        private void OnAchievementComplete(AchievementList achievement)
        {
            if (achievement == AchievementList.Thug)
            {
                Debug.Log("You defeated an Enemy.");
                WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Thug.ToString()}");
                GameManager.Instance.RecordPosiition($"{AchievementList.Thug.ToString()}");
            }

            if (achievement == AchievementList.Delinquent)
            {
                Debug.Log("You defeated 10 Enemies.");
                WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Delinquent.ToString()}");
                GameManager.Instance.RecordPosiition($"{AchievementList.Delinquent.ToString()}");
            }

            if (achievement == AchievementList.Warrior)
            {
                Debug.Log("You defeated 100 Enemies.");
                WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Warrior.ToString()}");
                GameManager.Instance.RecordPosiition($"{AchievementList.Warrior.ToString()}");
            }

            if (achievement == AchievementList.Inquisitor)
            {
                Debug.Log("You defeated 1000 Enemies.");
                WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Inquisitor.ToString()}");
                GameManager.Instance.RecordPosiition($"{AchievementList.Inquisitor.ToString()}");
            }
        }
    }
}
