using AtticAdventures.Core;
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
            WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Window_Shopping.ToString()}");

        }

        private void OnAchievementStrategyMeetingComplete()
        {
            Debug.Log($"You talked to Pam, leader of the Rebellion.");
            WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Strategy_Meeting.ToString()}");

        }

        private void OnAchievementSchoolNurseComplete()
        {
            Debug.Log($"You talked to Pippa, the Healer.");
            WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.School_Nurse.ToString()}");

        }

        private void OnAchievementTalkativeComplete()
        {
            Debug.Log($"You talked to someone.");
            WriteToFile.Instance.WriteAchievementWithTimestamp($"{AchievementList.Talkative.ToString()}");
            GameManager.Instance.RecordPosiition($"{AchievementList.Talkative.ToString()}");
        }
    }
}
