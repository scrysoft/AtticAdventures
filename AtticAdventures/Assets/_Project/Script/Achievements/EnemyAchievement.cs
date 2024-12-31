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
            }

            if (achievement == AchievementList.Delinquent)
            {
                Debug.Log("You defeated 10 Enemies.");
            }

            if (achievement == AchievementList.Warrior)
            {
                Debug.Log("You defeated 100 Enemies.");
            }

            if (achievement == AchievementList.Inquisitor)
            {
                Debug.Log("You defeated 1000 Enemies.");
            }
        }
    }
}
