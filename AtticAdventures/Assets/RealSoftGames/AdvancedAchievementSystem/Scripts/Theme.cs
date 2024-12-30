using System.Collections.Generic;
using UnityEngine;

namespace RealSoftGames.AdvancedAchievementSystem
{
    [CreateAssetMenu(fileName = "Theme", menuName = "RealSoft Games/Advanced Achievement System/Theme")]
    [System.Serializable]
    public class Theme : ScriptableObject
    {
        [SerializeField] GameObject achievementSystemPrefab;
        [SerializeField] GameObject achievementPrefab;
        [SerializeField] GameObject categoryPrefab;
        [SerializeField] private List<MapData> themeMap = new List<MapData>();

        [SerializeField] private Color categoryButtonTextColor = Color.white;
        [SerializeField] private Color categoryTextColor = Color.white;
        [SerializeField] private Color achievementSystemHeadingTextColor = Color.white;
        [SerializeField] private Color achievementSystemCategoryHeadingTextColor = Color.white;
        [SerializeField] private Color achievementSystemAPHeadingTextColor = Color.white;
        [SerializeField] private Color achievementHeadingTextColor = Color.white;
        [SerializeField] private Color achievementDescriptionTextColor = Color.white;
        [SerializeField] private Color achievementPointsTextColor = Color.white;
        [SerializeField] private Color achievementProgressTextColor = Color.white;
        [SerializeField] private Color searchFieldPlaceHolderTextColor = Color.white;
        [SerializeField] private Color searchFieldTextColor = Color.white;
        [SerializeField] private Color achievementCompleteHeadingTextColor = Color.white;
        [SerializeField] private Color achievementCompleteDescriptionTextColor = Color.white;
        [SerializeField] private Color achievementCompleteAchievementPointsTextColor = Color.white;

        public GameObject AchievementPrefab { get => achievementPrefab; set => achievementPrefab = value; }
        public GameObject AchievementSystemPrefab { get => achievementSystemPrefab; set => achievementSystemPrefab = value; }
        public GameObject CategoryPrefab { get => categoryPrefab; set => categoryPrefab = value; }

        public List<MapData> ThemeMap { get => themeMap; set => themeMap = value; }
        public Color CategoryButtonTextColor { get => categoryButtonTextColor; set => categoryButtonTextColor = value; }
        public Color CategoryTextColor { get => categoryTextColor; set => categoryTextColor = value; }
        public Color AchievementSystemHeadingTextColor { get => achievementSystemHeadingTextColor; set => achievementSystemHeadingTextColor = value; }
        public Color AchievementSystemCategoryHeadingTextColor { get => achievementSystemCategoryHeadingTextColor; set => achievementSystemCategoryHeadingTextColor = value; }
        public Color AchievementSystemAPHeadingTextColor { get => achievementSystemAPHeadingTextColor; set => achievementSystemAPHeadingTextColor = value; }
        public Color AchievementHeadingTextColor { get => achievementHeadingTextColor; set => achievementHeadingTextColor = value; }
        public Color AchievementDescriptionTextColor { get => achievementDescriptionTextColor; set => achievementDescriptionTextColor = value; }
        public Color AchievementPointsTextColor { get => achievementPointsTextColor; set => achievementPointsTextColor = value; }
        public Color AchievementProgressTextColor { get => achievementProgressTextColor; set => achievementProgressTextColor = value; }
        public Color SearchFieldPlaceHolderTextColor { get => searchFieldPlaceHolderTextColor; set => searchFieldPlaceHolderTextColor = value; }
        public Color SearchFieldTextColor { get => searchFieldTextColor; set => searchFieldTextColor = value; }
        public Color AchievementCompleteHeadingTextColor { get => achievementCompleteHeadingTextColor; set => achievementCompleteHeadingTextColor = value; }
        public Color AchievementCompleteDescriptionTextColor { get => achievementCompleteDescriptionTextColor; set => achievementCompleteDescriptionTextColor = value; }
        public Color AchievementCompleteAchievementPointsTextColor { get => achievementCompleteAchievementPointsTextColor; set => achievementCompleteAchievementPointsTextColor = value; }

        [System.Serializable]
        public class MapData
        {
            public MapData() { }

            public MapData(MapType mapType)
            {
                this.mapType = mapType;
                spriteColor = new Color(1f, 1f, 1f, 1f);
            }

            [SerializeField] private MapType mapType;
            [SerializeField] private Sprite sprite;
            [SerializeField] private Color spriteColor = new Color(1f, 1f, 1f, 1f);
            [SerializeField, ReadOnly] private string referenceMapParent;

            public MapType MapType { get => mapType; set => mapType = value; }
            public Sprite Sprite { get => sprite; set => sprite = value; }
            public Color SpriteColor { get => spriteColor; set => spriteColor = value; }
            public string ReferenceMapParent { get => referenceMapParent; set => referenceMapParent = value; }
        }
    }
}
