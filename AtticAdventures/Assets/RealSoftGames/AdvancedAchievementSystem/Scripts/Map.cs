using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RealSoftGames.AdvancedAchievementSystem
{
    public enum MapType
    {
        AchievementSystem = 0,
        AchievementSystemBackground = 1,
        AchievementCompletionText = 2,
        AchievementSystemHeadingText = 3,
        AchievementSearchFieldImage = 4,
        AchievementSearchFieldInputField = 5,
        AchievementSearchFieldInputFieldPlaceHolder = 6,
        AchievementSearchText = 7,
        AchievementSearchIconBackground = 8,
        AchievementSearchIcon = 9,
        CloseBackground = 10,
        CloseIcon = 11,

        AchievementSystemCategoryBackground = 12,
        AchievementSystemCategoryText = 13,
        AchievementSystemCategoryScrollViewRect = 14,
        AchievementSystemCategoryScrollViewMask = 15,
        AchievementSystemCategoryScrollBar = 16,
        AchievementSystemCategoryScrollBarHandle = 17,

        AchievementSystemAchievementBackground = 18,
        AchievementSystemAchievementScrollViewRect = 19,
        AchievementSystemAchievementScrollViewMask = 20,
        AchievementSystemAchievementScrollBar = 21,
        AchievementSystemAchievementScrollBarHandle = 22,

        AchievementsDropdownBackground = 23,
        DropdownArrow = 24,
        DropdownTemplateBackground = 25,
        DropdownTemplateViewMask = 26,
        DropdownItemBackground = 27,
        DropdownItemCheckmark = 28,
        DropdownScrollBar = 29,
        DropdownScrollBarHandle = 30,

        AchievementCompleteBackground = 31,
        AchievementCompleteText = 32,
        AchievementCompleteDescription = 33,
        AchievementCompleteIconBorder = 34,
        AchievementCompleteIcon = 35,
        AchievementCompleteAchievementPointsText = 36,

        CategoryButtonNormal = 37,
        CategoryButtonSelected = 38,
        CategoryButtonHighlighted = 39,
        CategoryText = 40,

        AchievementBackground = 41,
        AchievementCompleteOverlay = 42,
        AchievementIconBorder = 43,
        AchievementIcon = 44,
        AchievementAchievementPointsText = 45,
        AchievementProgressBackground = 46,
        AchievementProgressFill = 47,
        AchievementProgressSlider = 48,
        AchievementHeading = 49,
        AchievementDescription = 50,
        AchievementPoints = 51,
        AchievementProgressText = 52,

        AchievementSystemCanvas = 53,
        AchievementSystemCategoriesContent = 54,
        AchievementSystemAchievementContent = 55,
        AchievementComplete = 56,
        AchievementSystemCategoryHeadingText = 57
    }


    public class Map : MonoBehaviour
    {
        [SerializeField] private MapType mapType;
        [SerializeField] private Component component;
        [SerializeField] private bool hideFromThemeEditor;
        [SerializeField, ReadOnly] string referenceMapParent;

        public MapType MapType { get => mapType; set => mapType = value; }
        public Component Component { get => component; set => component = value; }
        public bool HideFromThemeEditor { get => hideFromThemeEditor; set => hideFromThemeEditor = value; }
        public string ReferenceMapParent { get => referenceMapParent; set => referenceMapParent = value; }

        // Use this method to get the specific component type stored in the 'component' field.
        public T GetValue<T>() where T : Component
        {
            return component as T;
        }
    }
}