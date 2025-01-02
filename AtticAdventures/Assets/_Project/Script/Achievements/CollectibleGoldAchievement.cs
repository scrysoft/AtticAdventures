using AtticAdventures.Core;
using RealSoftGames.AdvancedAchievementSystem;
using System;
using UnityEngine;

namespace AtticAdventures
{
    public class CollectibleGoldAchievement : MonoBehaviour
    {
        public void Achievement(int value)
        {
            switch (value)
            {
                case 0:
                    AchievementManager.Instance.UpdateAchievement(AchievementList.Unreachable_Treasure, OnAchievementCompleteCallback: OnAchievementUnreachableTreasureComplete);
                    break;
                case 1:
                    AchievementManager.Instance.UpdateAchievement(AchievementList.Daddys_Old_Records, OnAchievementCompleteCallback: OnAchievementOldRecordsComplete);
                    break;
                case 2:
                    AchievementManager.Instance.UpdateAchievement(AchievementList.Under_the_Dome, OnAchievementCompleteCallback: OnAchievementUnderTheDomeComplete);
                    break;
                case 3:
                    AchievementManager.Instance.UpdateAchievement(AchievementList.Tactical_Warfare, OnAchievementCompleteCallback: OnAchievementTacticalWarfareComplete);
                    break;
                case 4:
                    AchievementManager.Instance.UpdateAchievement(AchievementList.Under_new_Management, OnAchievementCompleteCallback: OnAchievementUnderNewManagementComplete);
                    break;
            }

            AchievementManager.Instance.UpdateAchievementsByKey(AchievementKey.GoldCollectible, OnAchievementCompleteCallback: OnAchievementAllGoldCollectiblesComplete);
        }

        private void OnAchievementAllGoldCollectiblesComplete(AchievementList achievement)
        {
            if(achievement == AchievementList.Parliament_of_Owls)
            {
                Debug.Log("Found all the Special Collectibles in the Polymere Valley");
                WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Parliament_of_Owls.ToString()}");
                GameManager.Instance.RecordPosiition($"{AchievementList.Parliament_of_Owls.ToString()}");
            }
        }

        private void OnAchievementUnderNewManagementComplete()
        {
            Debug.Log("Find the Collectible by toppling a tyrant.");
            WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Under_new_Management.ToString()}");
            GameManager.Instance.RecordPosiition($"{AchievementList.Under_new_Management.ToString()}");
        }

        private void OnAchievementTacticalWarfareComplete()
        {
            Debug.Log("Find the Collectible guarded by a tank.");
            WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Tactical_Warfare.ToString()}");
            GameManager.Instance.RecordPosiition($"{AchievementList.Tactical_Warfare.ToString()}");
        }

        private void OnAchievementUnderTheDomeComplete()
        {
            Debug.Log("Find the Collectible in the fish bowl.");
            WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Under_the_Dome.ToString()}");
            GameManager.Instance.RecordPosiition($"{AchievementList.Under_the_Dome.ToString()}");
        }

        private void OnAchievementOldRecordsComplete()
        {
            Debug.Log("Found the Collectible in the portable CD-Player.");
            WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Daddys_Old_Records.ToString()}");
            GameManager.Instance.RecordPosiition($"{AchievementList.Daddys_Old_Records.ToString()}");
        }

        private void OnAchievementUnreachableTreasureComplete()
        {
            Debug.Log("Found the Collectible at the highest point of polymere valley.");
            WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Unreachable_Treasure.ToString()}");
            GameManager.Instance.RecordPosiition($"{AchievementList.Unreachable_Treasure.ToString()}");
        }
    }
}
