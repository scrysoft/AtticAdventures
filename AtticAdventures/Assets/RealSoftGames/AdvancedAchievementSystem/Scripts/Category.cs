///-----------------------------------------------------------------
///   Author : Jake Aquilina
///   Date   : 16/06/2019 21:48
///-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace RealSoftGames.AdvancedAchievementSystem
{
    [System.Serializable]
    public class Category
    {
        public enum CategoryType
        {
            MainCategory,
            SubCategory
        }

        public string buttonName;
        private CategoryType categorType = CategoryType.MainCategory;
        [SerializeField] private CategoryList category;
        public List<SubCategory> subCategories;

        public CategoryType categoryType { get => categorType; }
        public CategoryList _Category { get => category; }

        [System.Serializable]
        public class SubCategory
        {
            #region Variables

            [SerializeField, ReadOnly] private CategoryType categoryType = CategoryType.SubCategory;
            [SerializeField] private CategoryList category;
            [SerializeField, Range(0, 1f)] private float incompleteAlpha = 0;
            [SerializeField, Range(0, 1f)] private float completeAlpha = 0.5f;
            [SerializeField] private Color color;
            [SerializeField] private Sprite image;

            #endregion Variables

            #region Properties

            public CategoryList Category { get => category; set => category = value; }

            public float IncompleteAlpha { get => incompleteAlpha; set => incompleteAlpha = value; }

            public float CompleteAlpha { get => completeAlpha; set => completeAlpha = value; }

            public Color Color { get => color; set => color = value; }

            public Sprite Image { get => image; set => image = value; }

            public CategoryType CategoryType { get => categoryType; }

            #endregion Properties
        }
    }
}