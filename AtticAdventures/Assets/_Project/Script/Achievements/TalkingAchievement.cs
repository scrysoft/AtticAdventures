using RealSoftGames.AdvancedAchievementSystem;
using UnityEngine;

namespace AtticAdventures
{
    public class TalkingAchievement : MonoBehaviour
    {
        public void Achievement(int value)
        {
            switch (value)
            {
                case 0:
                    AchievementManager.Instance.UpdateAchievement(AchievementList.Talkative, OnAchievementCompleteCallback: OnAchievementTalkativeComplete);
                    break;
                case 1:
                    AchievementManager.Instance.UpdateAchievement(AchievementList.Talkative, OnAchievementCompleteCallback: OnAchievementTalkativeComplete);
                    AchievementManager.Instance.UpdateAchievement(AchievementList.School_Nurse, OnAchievementCompleteCallback: OnAchievementSchoolNurseComplete);
                    break;
                case 2:
                    AchievementManager.Instance.UpdateAchievement(AchievementList.Talkative, OnAchievementCompleteCallback: OnAchievementTalkativeComplete);
                    AchievementManager.Instance.UpdateAchievement(AchievementList.Strategy_Meeting, OnAchievementCompleteCallback: OnAchievementStrategyMeetingComplete);
                    break;
                case 3:
                    AchievementManager.Instance.UpdateAchievement(AchievementList.Talkative, OnAchievementCompleteCallback: OnAchievementTalkativeComplete);
                    AchievementManager.Instance.UpdateAchievement(AchievementList.Window_Shopping, OnAchievementCompleteCallback: OnAchievementWindowShoppingComplete);
                    break;
            }
        }

        private void OnAchievementWindowShoppingComplete()
        {
            Debug.Log($"You talked to jarmaine, the Merchant.");
        }

        private void OnAchievementStrategyMeetingComplete()
        {
            Debug.Log($"You talked to Pam, leader of the Rebellion.");
        }

        private void OnAchievementSchoolNurseComplete()
        {
            Debug.Log($"You talked to Pippa, the Healer.");
        }

        private void OnAchievementTalkativeComplete()
        {
            Debug.Log($"You talked to someone.");
        }
    }
}
