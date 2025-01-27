using AtticAdventures.Core;
using RealSoftGames.AdvancedAchievementSystem;
using UnityEngine;

namespace AtticAdventures
{
    public class ProductAchievement : MonoBehaviour
    {
        public void Achievement()
        {
            AchievementManager.Instance.UpdateAchievement(AchievementList.First_Time_Customer, OnAchievementCompleteCallback: OnAchievementComplete);
        }

        private void OnAchievementComplete()
        {
            Debug.Log($"You have bought an Item in jarmaines shop.");
            WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.First_Time_Customer.ToString()}");
            GameManager.Instance.RecordPosiition($"{AchievementList.First_Time_Customer.ToString()}");
        }
    }
}
