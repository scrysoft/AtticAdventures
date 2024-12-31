using RealSoftGames.AdvancedAchievementSystem;
using UnityEngine;

namespace AtticAdventures
{
    public class CollectibleBlueAchievement : MonoBehaviour
    {
        public void Achievement()
        {
            AchievementManager.Instance.UpdateAchievementsByKey(AchievementKey.BlueCollectible, OnAchievementCompleteCallback: OnAchievementComplete);
        }

        private void OnAchievementComplete(AchievementList achievement)
        {
            if(achievement == AchievementList.Lucky_Guess)
            {
                Debug.Log("You found at least one Blue Collectible Capsule containing Beads.");
                WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Lucky_Guess.ToString()}");

            }

            if (achievement == AchievementList.Treasure_Hunter)
            {
                Debug.Log("You found all Blue Collectible Capsules containing Beads.");
                WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Treasure_Hunter.ToString()}");

            }
        }
    }
}
