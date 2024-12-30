///-----------------------------------------------------------------
///   Author : Jake Aquilina
///   Date   : 16/06/2019 21:48
///-----------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RealSoftGames.AdvancedAchievementSystem
{
    [System.Serializable]
    public class AdvancedAchievementSystem : ScriptableObject
    {
        #region Enums

        public enum DisplayType
        {
            Value,
            Percentage
        }

        public enum DropDownMenu
        {
            All,
            Incomplete,
            Completed
        }

        #endregion Enums

        #region Variables

        [SerializeField] private List<Category> categories = new List<Category>();
        [SerializeField] private List<BaseAchievement> achievements = new List<BaseAchievement>();
        [SerializeField] private DisplayType display;
        [SerializeField] private DropDownMenu menu;
        [SerializeField] GameObject achievementSystemPrefab;
        [SerializeField] private GameObject categoriesBtnPrefab;
        [SerializeField] private GameObject achievementPrefab;
        [SerializeField] private AudioClip onCompleteAudio;
        [SerializeField] private string dateTimeFormat = "dd MMM yyyy hh:mmtt";
        [SerializeField] private Theme theme;

        [SerializeField] private List<string> categoryNames = new List<string>();
        [SerializeField] private List<string> achievementNames = new List<string>();
        [SerializeField] private List<string> achievementKeys = new List<string>();


        [SerializeField] private float displayTime = 5f;
        [SerializeField] private float fadeInTime = 1f;
        [SerializeField] private float fadeOutTime = 1f;

        #endregion Variables

        #region Properties
        public float DisplayTime { get => displayTime; set => displayTime = value; }
        public float FadeInTime { get => fadeInTime; set => fadeInTime = value; }
        public float FadeOutTime { get => fadeOutTime; set => fadeOutTime = value; }

        public List<Category> Categories { get => categories; set => categories = value; }
        public List<BaseAchievement> Achievements { get => achievements; set => achievements = value; }
        public DisplayType Display { get => display; set => display = value; }

        public DropDownMenu Menu { get => menu; set => menu = value; }

        public GameObject AchievementSystemPrefab { get => achievementSystemPrefab; set => achievementSystemPrefab = value; }
        public GameObject CategoriesBtnPrefab { get => categoriesBtnPrefab; set => categoriesBtnPrefab = value; }

        public GameObject AchievementPrefab { get => achievementPrefab; set => achievementPrefab = value; }

        public AudioClip OnCompleteAudio { get => onCompleteAudio; set => onCompleteAudio = value; }

        public string DateTimeFormat { get => dateTimeFormat; set => dateTimeFormat = value; }
        public Theme Theme { get => theme; set => theme = value; }

        public List<string> CategoryNames { get => categoryNames; set => categoryNames = value; }
        public List<string> AchievementNames { get => achievementNames; set => achievementNames = value; }
        public List<string> AchievementKeys { get => achievementKeys; set => achievementKeys = value; }

        #endregion Properties
    }
}