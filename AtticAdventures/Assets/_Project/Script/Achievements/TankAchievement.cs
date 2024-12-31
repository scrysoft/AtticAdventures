using RealSoftGames.AdvancedAchievementSystem;
using UnityEngine;

namespace AtticAdventures
{
    public class TankAchievement : MonoBehaviour
    {
        public void Achievement()
        {
            AchievementManager.Instance.UpdateAchievement(AchievementList.Smothers_Secret_Weapon, OnAchievementCompleteCallback: OnAchievementComplete);
        }

        private void OnAchievementComplete()
        {
            Debug.Log($"You managed to destroy Sgt. Smother’s Tank.");
        }
    }
}
