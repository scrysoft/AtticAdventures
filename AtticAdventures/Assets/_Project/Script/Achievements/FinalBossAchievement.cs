using AtticAdventures.Core;
using RealSoftGames.AdvancedAchievementSystem;
using UnityEngine;

namespace AtticAdventures
{
    public class FinalBossAchievement : MonoBehaviour
    {
        public void Achievement()
        {
            AchievementManager.Instance.UpdateAchievement(AchievementList.Military_Coup, OnAchievementCompleteCallback: OnAchievementComplete);
        }

        private void OnAchievementComplete()
        {
            Debug.Log($"Military Coup completed!");
            WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Military_Coup.ToString()}");
            GameManager.Instance.RecordPosiition($"{AchievementList.Military_Coup.ToString()}");
        }
    }
}
