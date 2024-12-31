using RealSoftGames.AdvancedAchievementSystem;
using UnityEngine;

namespace AtticAdventures
{
    public class HighestPointAchievement : MonoBehaviour
    {
        public void Achievement()
        {
            AchievementManager.Instance.UpdateAchievement(AchievementList.King_of_the_World, OnAchievementCompleteCallback: OnAchievementComplete);
        }

        private void OnAchievementComplete()
        {
            Debug.Log($"You have reached the top of the valley.");
            WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.King_of_the_World.ToString()}");
        }
    }
}
