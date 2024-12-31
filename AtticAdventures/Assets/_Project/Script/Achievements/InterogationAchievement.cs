using RealSoftGames.AdvancedAchievementSystem;
using UnityEngine;

namespace AtticAdventures
{
    public class InterogationAchievement : MonoBehaviour
    {
        public void Achievement()
        {
            AchievementManager.Instance.UpdateAchievement(AchievementList.Broken_Silence, OnAchievementCompleteCallback: OnAchievementComplete);
        }

        private void OnAchievementComplete()
        {
            Debug.Log($"Extract a prisoner's closely guarded secret to unveil the dark truth.");
            WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Broken_Silence.ToString()}");

        }
    }
}
