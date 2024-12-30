using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RealSoftGames.AdvancedAchievementSystem
{
    public class Achievement : MonoBehaviour
    {
        [SerializeField] private ReferenceMap map;
        [SerializeField] private AchievementList achievement;
        [SerializeField] private CategoryList category;
        [SerializeField] private string searchString;

        public AchievementList _Achievement { get => achievement; set => achievement = value; }
        public CategoryList _Category { get => category; set => category = value; }
        public ReferenceMap Map { get => map; set => map = value; }
        public string SearchString { get => searchString; set => searchString = value; }

        private void Awake()
        {
            if (map == null)
                map = GetComponent<ReferenceMap>();
        }

        public void Intialize(BaseAchievement item)
        {
            achievement = item.Achievement;
            map.Maps.Find(i => i.MapType == MapType.AchievementHeading).GetValue<TMP_Text>().text = item.AchievementName;
            map.Maps.Find(i => i.MapType == MapType.AchievementAchievementPointsText).GetValue<TMP_Text>().text = $"AP:{item.AchievementPoints.ToString()}";
            map.Maps.Find(i => i.MapType == MapType.AchievementDescription).GetValue<TMP_Text>().text = item.Description;
            map.Maps.Find(i => i.MapType == MapType.AchievementIcon).GetValue<Image>().sprite = item.Image;
        }
    }
}