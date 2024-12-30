///-----------------------------------------------------------------
///   Author : Jake Aquilina
///   Date   : 11/06/2024
///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace RealSoftGames.AdvancedAchievementSystem
{
    [System.Serializable]
    public class BaseAchievement
    {
        #region Constructors

        public BaseAchievement(string achievementName, CategoryList category, string description, Sprite image, int curValue, int neededValue, int achievementPoints, bool complete, List<AchievementKey> keys, AchievementReward reward)
        {
            this.image = image;
            this.achievementName = achievementName;
            this.category = category;
            this.description = description;
            this.currentValue = curValue;
            this.neededValue = neededValue;
            this.achievementPoints = achievementPoints;
            this.complete = complete;
            this.keys = keys;
        }

        // Default constructor needed for JSON deserialization
        public BaseAchievement() { }

        #endregion Constructors

        #region Serialized Variables

        [SerializeField] private string achievementName;
        [SerializeField] private AchievementList achievement;
        [SerializeField] private CategoryList category;
        [SerializeField] private string searchString;
        [SerializeField] private List<AchievementKey> keys;
        [TextArea, SerializeField] private string description;
        [SerializeField] private Sprite image;
        [SerializeField] private int currentValue = 0;
        [SerializeField] private int neededValue = 1;
        [SerializeField] private int achievementPoints;
        [SerializeField] private bool complete;
        [SerializeField] private string completeTimeDate;
        [SerializeField] private bool hiddenAchievement;
        [SerializeField] private bool requiresCompletedAchievement;
        [SerializeField] private AchievementList requiresCompletedAchievementID;
        [SerializeField] AchievementReward achievementReward;

        #endregion Serialized Variables

        #region Actions

        // Exclude Actions from JSON serialization as they are not supported
        [NonSerialized] private Action onUpdate;
        [NonSerialized] private Action onComplete;

        #endregion Actions

        #region Properties

        public string AchievementName { get => achievementName; set => achievementName = value; }
        public AchievementList Achievement { get => achievement; set => achievement = value; }
        public CategoryList Category { get => category; set => category = value; }
        public List<AchievementKey> Keys { get => keys; set => keys = value; }
        public string Description { get => description; set => description = value; }
        public Sprite Image { get => image; set => image = value; }
        public int CurrentValue { get => currentValue; set => currentValue = value; }
        public int NeededValue { get => neededValue; set => neededValue = value; }
        public int AchievementPoints { get => achievementPoints; set => achievementPoints = value; }
        public bool Complete { get => complete; set => complete = value; }
        public string CompleteTimeDate { get => completeTimeDate; set => completeTimeDate = value; }
        public bool HiddenAchievement { get => hiddenAchievement; set => hiddenAchievement = value; }
        public bool RequiresCompletedAchievement { get => requiresCompletedAchievement; set => requiresCompletedAchievement = value; }
        public AchievementList RequiresCompletedAchievementID { get => requiresCompletedAchievementID; set => requiresCompletedAchievementID = value; }
        public string SearchString { get => searchString; set => searchString = value; }
        public Action OnUpdate { get => onUpdate; set => onUpdate = value; }
        public Action OnComplete { get => onComplete; set => onComplete = value; }

        public AchievementReward AchievementReward { get => achievementReward; set => achievementReward = value; }

        #endregion Properties

        #region Custom Serialization Methods

        [Serializable]
        public class AchievementContainer
        {
            public List<AchievementData> Achievements = new List<AchievementData>();
        }

        [Serializable]
        public class AchievementData
        {
            public AchievementList Achievement;
            public int CurrentValue;
            public int NeededValue;
            public bool Complete;
            public string CompleteTimeDate;
        }

        public AchievementData ToData()
        {
            return new AchievementData
            {
                Achievement = this.achievement,
                CurrentValue = this.currentValue,
                NeededValue = this.neededValue,
                Complete = this.complete,
                CompleteTimeDate = this.completeTimeDate
            };
        }

        public void FromData(AchievementData data)
        {
            this.currentValue = data.CurrentValue;
            this.neededValue = data.NeededValue;
            this.complete = data.Complete;
            this.completeTimeDate = data.CompleteTimeDate;
        }

        #endregion Custom Serialization Methods
    }
}
