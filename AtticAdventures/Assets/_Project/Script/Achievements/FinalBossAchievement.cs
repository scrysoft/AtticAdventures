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
        }
    }
}
