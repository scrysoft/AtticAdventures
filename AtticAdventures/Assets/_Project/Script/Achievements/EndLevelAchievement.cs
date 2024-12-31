using RealSoftGames.AdvancedAchievementSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AtticAdventures
{
    public class EndLevelAchievement : MonoBehaviour
    {
        public void Achievement()
        {
            AchievementManager.Instance.UpdateAchievement(AchievementList.To_be_continued, OnAchievementCompleteCallback: OnAchievementComplete);
        }

        private void OnAchievementComplete()
        {
            Debug.Log($"You reached the Gate to the next level.");
        }
    }
}
