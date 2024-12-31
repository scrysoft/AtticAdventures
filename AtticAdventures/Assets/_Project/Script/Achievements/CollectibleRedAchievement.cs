using RealSoftGames.AdvancedAchievementSystem;
using UnityEngine;

namespace AtticAdventures
{
    public class CollectibleRedAchievement : MonoBehaviour
    {
        public void Achievement()
        {
            AchievementManager.Instance.UpdateAchievementsByKey(AchievementKey.RedCollectible, OnAchievementCompleteCallback: OnAchievementComplete);
        }

        private void OnAchievementComplete(AchievementList achievement)
        {
            if (achievement == AchievementList.Surprise)
            {
                Debug.Log("You found at least one Red Collectible Capsule containing Enemies.");
            }

            if (achievement == AchievementList.Asking_for_trouble)
            {
                Debug.Log("You found all Red Collectible Capsules containing Enemies.");
            }
        }
    }
}
