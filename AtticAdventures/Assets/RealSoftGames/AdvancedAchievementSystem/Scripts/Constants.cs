///-----------------------------------------------------------------
///   Author : Jake Aquilina
///   Date   : 28/07/2019 12:56
///-----------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RealSoftGames
{
    public class Constants
    {
        public static string ALL = "all";
        public static readonly string ACHIEVEMENT_POINTS_TEXT = "AP: ";
        public static readonly string EDITORPREFS_KEY_DATABASE_NAME = "DatabaseName";
        public static readonly string ACHIEVEMENT_SYSTEM_NAME = "AchievementSystem";
        public static readonly string REPLACE_CURRENT_VALUE = "{CurrentValue}";
        public static readonly string REPLACE_NEEDED_VALUE = "{NeededValue}";
        public static readonly string REPLACE_ACHIEVEMENT_POINTS = "{AchievementPoints}";
        public static readonly string EDITOR_SAVE_PATH = "/RealSoftGames/AchievementSystem/AchievementSaveData/";
        public static readonly string APPLICATION_SAVE_PATH = Application.dataPath + "/AchievementSaveData/";
        public static readonly string ACHIEVEMENT_COMPLETION_TEXT = "Progress: ";
        public static readonly string CATEGORY_BUTTON_PREFAB = "Category";
        public static readonly string ACHIEVEMENT_PREFAB = "Achievement";
        public static readonly string SPRITE_TRANSITION_TAB = "SpriteTransitionTab";
        public static readonly string THEME_PACK_LOCATION = "ThemePack"; //"Assets/RealSoftGames/AchievementSystem/_PREFABS/ThemePacks/"//SCI-FI RPG
        public static readonly string ADVANCED_ACHIEVEMENT_SYSTEM_PREFAB_LOCATION = "ThemePack/Template";

        public static readonly string ACHIEVEMENT_SYSTEM_PREFAB = "AchievementSystem";
        public static readonly string THEME_TEMPLATE = "Theme";
        public static string THEME_PACKS_ROOT_PATH = "Assets\\RealSoftGames\\AdvancedAchievementSystem\\ThemePacks";
    }
}