using AtticAdventures.Core;
using RealSoftGames.AdvancedAchievementSystem;
using UnityEngine;

namespace AtticAdventures
{
    public class CollectibleBeadsAchievement : MonoBehaviour
    {
        public void Achievement()
        {
            AchievementManager.Instance.UpdateAchievementsByKey(AchievementKey.BeadsCollectible, OnAchievementCompleteCallback: OnAchievementComplete);
        }

        private void OnAchievementComplete(AchievementList achievement)
        {
            if (achievement == AchievementList.Trust_Fund)
            {
                Debug.Log("You collected 100 Beads.");
                WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Trust_Fund.ToString()}");
                GameManager.Instance.RecordPosiition($"{AchievementList.Trust_Fund.ToString()}");
            }

            if (achievement == AchievementList.Bead_Tycoon)
            {
                Debug.Log("You collected 1000 Beads.");
                WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Bead_Tycoon.ToString()}");
                GameManager.Instance.RecordPosiition($"{AchievementList.Bead_Tycoon.ToString()}");
            }
        }
    }
}
