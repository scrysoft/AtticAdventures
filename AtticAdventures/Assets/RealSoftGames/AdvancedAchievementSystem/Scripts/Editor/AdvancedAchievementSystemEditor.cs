///-----------------------------------------------------------------
///   Author : Jake Aquilina
///   Date   : 16/06/2019 21:48
///-----------------------------------------------------------------

using System;
using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Object = UnityEngine.Object;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

namespace RealSoftGames.AdvancedAchievementSystem
{
    [CustomEditor(typeof(AdvancedAchievementSystem))]
    public class AdvancedAchievementSystemEditor : EditorWindowTemplate
    {
        #region Variables

        private class ControlPanel
        {
            public string FieldName;
            public Settings Setting;
        }

        private enum Settings
        { Installation, Database, General, Categories, Achievements, AchievementComplete, Theme }

        private ControlPanel[] Controls = new ControlPanel[]
        {
            new ControlPanel { FieldName = "Database", Setting = Settings.Database },
            new ControlPanel { FieldName = "General", Setting = Settings.General },
            new ControlPanel { FieldName = "Categories", Setting = Settings.Categories },
            new ControlPanel { FieldName = "Achievements", Setting = Settings.Achievements },
            new ControlPanel { FieldName = "Achievement \nComplete", Setting = Settings.AchievementComplete },
            new ControlPanel { FieldName = "Theme", Setting = Settings.Theme },
        };

        private Settings settings = Settings.Installation;
        private Color selectedColor = Color.grey;
        private Color deselectedColor = Color.white;

        //Achievement Complete Simulation
        private float startTime;
        private float duration;
        private CanvasGroup alphaGroup;
        private enum FadeState { None, FadingIn, Displaying, FadingOut }
        private FadeState fadeState = FadeState.None;



        private bool GenerateCategoryEnumFoldout, GenerateAchievementEnumFoldout, GenerateAchievementKeyEnumFoldout, HeadingColors;

        private bool achievementColorsFoldout, achievementCompleteColorsFoldout, searchFieldFoldout;
        private bool achievementSystemEnabled = false;
        private AchievementManager achievementManagerSceneInstance;
        private EventSystem eventSystem;

        private float windowHeight = 0;

        private Vector2 scrollPositionCategoryNames, scrollPositionAchievementNames, scrollPositionAchievementKeys, generalTabScroll, scrollPositionThemeMap;

        private CategoryBtn.SpriteTransition spriteTransitionTab;

        [SerializeField] public static string databaseName = "";
        [SerializeField] public Theme theme;
        public string txt;

        private AdvancedAchievementSystem Database = null;

        private string dbLocation = "RealSoftGames/AdvancedAchievementSystem/Database/";
        private string themePackLocation = "Assets/RealSoftGames/AchievementSystem/_PREFABS/ThemePacks/";
        private string newDirectoryName = "NewThemePack";
        //private string generatedFileLocation = "Assets\\RealSoftGames\\AchievementSystem\\GeneratedFiles\\";

        private bool CreatingDB;

        #region Serialization

        private SerializedObject so = null, themeSerializedObject;

        private SerializedProperty
          display,
          categories,
          themeProp,
          themeMapProperty,
          categoryNames,
          achievementNames,
          achievementKeys;

        private Dictionary<int, ReorderableList> subCategoriesLists = new Dictionary<int, ReorderableList>();
        private Dictionary<int, ReorderableList> achievementKeysLists = new Dictionary<int, ReorderableList>();
        private string achievementSearchString = "";
        private List<string> errorMessages = new List<string>();

        private ReorderableList achievementList = null, categoriesList, themeMapList;
        private Vector2 scrollPos, categoriesScrollPos, themeScrollBar;

        #endregion Serialization

        private AdvancedAchievementSystemEditor window;

        private GameObject
         achievementSystemCanvas,
         achievementSystemCategoriesContent,
         achievementSystemAchievementContent;

        private AudioClip onCompleteAudio;
        private string dateTimeFormat;

        private string searchString = "";
        private Dictionary<string, ReorderableList> reorderableLists = new Dictionary<string, ReorderableList>();
        private Dictionary<string, List<Theme.MapData>> originalThemeMaps = new Dictionary<string, List<Theme.MapData>>();
        private Dictionary<MapType, List<string>> duplicateMapTypes = new Dictionary<MapType, List<string>>();
        private Dictionary<string, bool> foldoutStates = new Dictionary<string, bool>();

        private bool DatabaseExists
        {
            get
            {
                string[] temp = Directory.GetFiles(Application.dataPath + "/" + dbLocation);

                foreach (string t in temp)
                    if (t == Application.dataPath + "/" + dbLocation + databaseName + ".asset")
                        return true;

                return false;
            }
        }

        #endregion Variables

        [MenuItem("Tools/RealSoft Games/Advanced Achievement System")]
        public new static void ShowWindow()
        {
            var window = GetWindow<AdvancedAchievementSystemEditor>("Advanced Achievement System");
            window.name = "Advanced Achievement System";
        }

        protected override void OnEnable()
        {
            NavigationPanelWidth = 150f;
            base.OnEnable();
            achievementManagerSceneInstance = FindFirstObjectByType<AchievementManager>();
            eventSystem = FindFirstObjectByType<EventSystem>();
            window = (AdvancedAchievementSystemEditor)EditorWindow.GetWindow(typeof(AdvancedAchievementSystemEditor));

            if (!string.IsNullOrEmpty(EditorPrefs.GetString(Constants.THEME_PACK_LOCATION)))
            {
                themePackLocation = EditorPrefs.GetString(Constants.THEME_PACK_LOCATION);
                Debug.Log($"Loading Themepack location: {themePackLocation}");
            }

            //If Advanced Achievement System is not yet installed dont continue
            if (achievementManagerSceneInstance == null)
            {
                settings = Settings.Installation;
                return;
            }

            InitialiseEditorWindow();
        }

        protected override void OnDestroy()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        protected override void EditorGUILAyoutLeftPanel()
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fontStyle = FontStyle.Bold,
                fontSize = 12,
                fixedHeight = 35
            };

            if (achievementManagerSceneInstance == null)
            {
                settings = Settings.Installation;

                if (GUILayout.Button("Installation", buttonStyle, GUILayout.ExpandWidth(true)))
                {
                    settings = Settings.Installation;
                }
            }
            else if (Database == null)
            {
                settings = Settings.Database;
                if (GUILayout.Button("Database", buttonStyle, GUILayout.ExpandWidth(true)))
                {
                    settings = Settings.Database;
                }
            }
            else if (Database.GetType().GetProperty("Theme").GetValue(Database, null) == null)
            {
                settings = Settings.Database;
                if (GUILayout.Button("Database", buttonStyle, GUILayout.ExpandWidth(true)))
                {
                    settings = Settings.Database;
                }
            }
            else
            {
                foreach (var control in Controls)
                {
                    GUI.backgroundColor = control.Setting == settings ? selectedColor : deselectedColor;

                    if (GUILayout.Button(control.FieldName, buttonStyle, GUILayout.ExpandWidth(true)))
                    {
                        settings = control.Setting;
                    }
                }
                GUI.backgroundColor = deselectedColor;
            }
        }

        protected override void EditorGUILAyoutMainBody()
        {
            if (so != null)
                so.Update();

            if (achievementManagerSceneInstance == null)
                settings = Settings.Installation;
            else if (Database == null)
                settings = Settings.Database;

            switch (settings)
            {
                case Settings.Installation:
                    if (achievementManagerSceneInstance == null)
                        DisplayInstallationGUI();
                    else
                        EditorApplication.delayCall += () => settings = Settings.Database;
                    break;

                case Settings.General:
                    DrawGeneralTabGUI();
                    EditorGUILayout.Space();
                    GUILayout.BeginHorizontal();
                    DrawGenerateCategoriesButton();
                    DrawGenerateAchievementsButton();
                    GUILayout.EndHorizontal();
                    EditorGUILayout.Space(20);

                    DrawSearchAchievementColors();
                    EditorGUILayout.Space(20);

                    generalTabScroll = EditorGUILayout.BeginScrollView(generalTabScroll);

                    DrawGenerateCategoryEnums();
                    EditorGUILayout.Space(20);
                    DrawGenerateAchievementEnums();
                    EditorGUILayout.Space(20);
                    DrawGenerateAchievementKeyEnums();
                    EditorGUILayout.Space(20);

                    EditorGUILayout.EndScrollView();

                    break;

                case Settings.Database:
                    DisplayDatabaseGUI();
                    break;

                case Settings.Categories:
                    DrawGenerateCategoriesButton();
                    DrawCategoriesList();
                    break;

                case Settings.Achievements:
                    DrawDisplayTypeGUI();
                    DrawGenerateAchievementsButton();
                    DrawAchievementsList();
                    break;

                case Settings.AchievementComplete:
                    DrawAchievementComplete();
                    break;

                case Settings.Theme:
                    DrawThemeTabGUI();
                    break;

                default:
                    break;
            }

            if (EditorGUI.EndChangeCheck())
            {
                if (so != null)
                    so.ApplyModifiedProperties();

                // Force repaint to update the GUI
                Repaint();
            }
        }

        protected override void Update()
        {
            base.Update();

            if (alphaGroup == null || fadeState == FadeState.None)
                return;

            float elapsed = (float)EditorApplication.timeSinceStartup - startTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float remainingTime = duration - elapsed;

            switch (fadeState)
            {
                case FadeState.FadingIn:
                    alphaGroup.alpha = Mathf.Lerp(0, 1, t);
                    if (t >= 1)
                    {
                        fadeState = FadeState.Displaying;
                        startTime = (float)EditorApplication.timeSinceStartup;
                        duration = achievementManagerSceneInstance.DisplayTime;
                    }
                    break;

                case FadeState.Displaying:
                    if (elapsed >= duration)
                    {
                        fadeState = FadeState.FadingOut;
                        startTime = (float)EditorApplication.timeSinceStartup;
                        duration = achievementManagerSceneInstance.FadeOutTime;
                    }
                    break;

                case FadeState.FadingOut:
                    alphaGroup.alpha = Mathf.Lerp(1, 0, t);
                    if (t >= 1)
                    {
                        fadeState = FadeState.None;
                    }
                    break;
            }

            Repaint();
        }


        private void InitialiseEditorWindow()
        {
            if (!string.IsNullOrEmpty(EditorPrefs.GetString(Constants.EDITORPREFS_KEY_DATABASE_NAME)))
                databaseName = EditorPrefs.GetString(Constants.EDITORPREFS_KEY_DATABASE_NAME);
            spriteTransitionTab = (CategoryBtn.SpriteTransition)EditorPrefs.GetInt(Constants.SPRITE_TRANSITION_TAB);

            bool databaseLoaded = LoadDatabase();
            if (!databaseLoaded)
                return;

            Database.AchievementPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(themePackLocation + "/" + Constants.ACHIEVEMENT_PREFAB + ".prefab");
            Database.CategoriesBtnPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(themePackLocation + "/" + Constants.CATEGORY_BUTTON_PREFAB + ".prefab");

            if (achievementManagerSceneInstance != null)
            {
                achievementManagerSceneInstance.Map.GetComponentFromReferenceMapComponent<RectTransform>(MapType.AchievementSystemBackground).localScale = Vector3.one;
                achievementSystemCanvas = achievementManagerSceneInstance.GetComponent<ReferenceMap>().GetComponentFromReferenceMapComponent<Transform>(MapType.AchievementSystemCanvas).gameObject;
                achievementSystemCategoriesContent = achievementManagerSceneInstance.GetComponent<ReferenceMap>().GetComponentFromReferenceMapComponent<Transform>(MapType.AchievementSystemCategoriesContent).gameObject;
                achievementSystemAchievementContent = achievementManagerSceneInstance.GetComponent<ReferenceMap>().GetComponentFromReferenceMapComponent<Transform>(MapType.AchievementSystemAchievementContent).gameObject;

                achievementManagerSceneInstance.ToggleGroup = achievementSystemCategoriesContent.GetComponent<ToggleGroup>();
                achievementManagerSceneInstance.SearchField = achievementManagerSceneInstance.GetComponent<ReferenceMap>().GetType<TMP_InputField>(MapType.AchievementSearchFieldInputField);
            }

            if (Database != null)
            {
                if (Database.Theme != null && themeProp != null && themeProp.objectReferenceValue != null)
                {
                    InitializeTheme();
                }
                else
                {
                    if (Database == null)
                        Debug.LogWarning("Database is null");
                    else if (!DatabaseExists)
                        Debug.LogWarning("Database does not exists");
                    else if (Database.Theme == null)
                        Debug.LogWarning("Theme is not yet selected, select one first or create one to continue");
                    else if (themeProp == null)
                        Debug.LogWarning("themeProp is null");
                    else if (themeProp != null && themeProp.objectReferenceValue == null)
                        Debug.LogWarning("themeProp value is null");

                    if (Database.Theme == null)
                    {
                        if (TryGetThemeAtPath(themePackLocation, out var theme))
                            Database.Theme = theme;
                        InitializeTheme();
                    }
                    else
                        ShowThemeSelectionPrompt();
                }
            }

            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void InitializeSerializedObjects()
        {
            so = new SerializedObject(Database);
            achievementList = new ReorderableList(so, so.FindProperty("achievements"), true, true, true, true);
            categoriesList = new ReorderableList(so, so.FindProperty("categories"), true, true, true, true);
            display = so.FindProperty("display");
            categories = so.FindProperty("categories");

            onCompleteAudio = so.FindProperty("onCompleteAudio").objectReferenceValue as AudioClip;
            dateTimeFormat = so.FindProperty("dateTimeFormat").stringValue as string;
            categoryNames = so.FindProperty("categoryNames");
            achievementNames = so.FindProperty("achievementNames");
            achievementKeys = so.FindProperty("achievementKeys");

            themeProp = so.FindProperty("theme");

            EditorPrefs.SetString("DatabaseName", databaseName);

            achievementSystemCanvas = achievementManagerSceneInstance.transform.FindDeepChild("AchievementSystemCanvas").gameObject;
            achievementSystemCategoriesContent = achievementManagerSceneInstance.transform.FindDeepChild("AchievementSystemCategoriesContent").gameObject;
            achievementSystemAchievementContent = achievementManagerSceneInstance.transform.FindDeepChild("AchievementSystemAchievementContent").gameObject;
        }

        private void InitializeTheme()
        {
            themeSerializedObject = new SerializedObject(Database.Theme);
            themeProp = so.FindProperty("theme");

            if (themeProp != null)
            {
                themeMapProperty = themeSerializedObject.FindProperty("themeMap");

                if (themeMapProperty == null)
                {
                    Debug.LogError("themeMapProperty is null. Ensure 'themeMap' exists within the 'theme' object.");
                }

                theme = (Theme)themeProp.objectReferenceValue;
                if (themeProp != null && themeProp.objectReferenceValue != null)
                    themeSerializedObject = new SerializedObject(themeProp.objectReferenceValue);

                reorderableLists = new Dictionary<string, ReorderableList>();
                originalThemeMaps = new Dictionary<string, List<Theme.MapData>>();
                duplicateMapTypes = new Dictionary<MapType, List<string>>();

                InitializeThemeMap();

                achievementList = new ReorderableList(so, so.FindProperty("achievements"), true, true, true, true);

            }
            else
                Debug.LogError("themeProp is null. Ensure 'theme' exists within the target object.");
        }

        private void ShowThemeSelectionPrompt(Action OnThemeSelected = null)
        {
            var inputConfigs = new List<InputPromptWindow.InputConfig>
            {
                new InputPromptWindow.InputConfig
                {
                    label = "Select a Theme",
                    type = InputPromptWindow.InputType.ObjectField,
                    defaultValue = null,
                    ObjectType = typeof(Theme)
                }
            };
            var buttonConfigs = new List<InputPromptWindow.ButtonConfig>
                {
                    new InputPromptWindow.ButtonConfig
                    {
                        label = "Confirm",
                        action = (List<object> inputs) =>
                        {
                            Theme selectedTheme = inputs[0] as Theme;
                            if (selectedTheme != null)
                            {
                                Database.Theme = selectedTheme;

                                InitializeSerializedObjects();

                                themeProp.objectReferenceValue = selectedTheme;
                                themeSerializedObject = new SerializedObject(selectedTheme);

                                InitializeTheme();
                                OnThemeSelected?.Invoke();
                            }
                            else
                            {
                                //inputs[0] = null;
                                Debug.LogError("No Theme was selected. Please select a theme to continue.");
                            }
                        },
                        shouldCloseCondition = (List<object> inputs) => {  return inputs[0] != null; }
                    }
                };

            InputPromptWindow.ShowWindow(
                "Theme Selection",
                "Please select a theme to continue.",
                inputConfigs,
                buttonConfigs);
        }

        private void DrawGeneralTabGUI()
        {
            if (so != null)
            {
                achievementSystemCanvas = Utilities.EditorGUILayoutField("Achievement System Canvas", achievementSystemCanvas);
                achievementSystemCategoriesContent = Utilities.EditorGUILayoutField("Categories Content", achievementSystemCategoriesContent);
                achievementSystemAchievementContent = Utilities.EditorGUILayoutField("Achievement Content", achievementSystemAchievementContent);

                Database.CategoriesBtnPrefab = Utilities.EditorGUILayoutField("Categories Button Prefab", Database.CategoriesBtnPrefab);
                Database.AchievementPrefab = Utilities.EditorGUILayoutField("Achievement Prefab", Database.AchievementPrefab);
                Database.OnCompleteAudio = Utilities.EditorGUILayoutField("On Complete Audio", onCompleteAudio);
                Database.DateTimeFormat = Utilities.EditorGUILayoutField("DateTime Format", so.FindProperty("dateTimeFormat").stringValue);
                EditorGUILayout.LabelField(new GUIContent("DateTime Format: " + DateTime.Now.ToString(dateTimeFormat)));
                EditorGUILayout.Space(5);

                if (!achievementSystemEnabled)
                {
                    if (GUILayout.Button("Enable Achievement System"))
                        ChangeAchievmentSystemEnabledState(true);
                }
                else
                {
                    if (GUILayout.Button("Dissable Achievement System"))
                        ChangeAchievmentSystemEnabledState(false);
                }

                if (achievementManagerSceneInstance?.AchievementCompleteAlphaGroup != null)
                    if (achievementManagerSceneInstance.AchievementCompleteAlphaGroup.alpha == 0)
                    {
                        if (GUILayout.Button("Enable Achievement Complete"))
                            achievementManagerSceneInstance.AchievementCompleteAlphaGroup.alpha = 1;
                    }
                    else
                    {
                        if (GUILayout.Button("Dissable Achievement Complete"))
                            achievementManagerSceneInstance.AchievementCompleteAlphaGroup.alpha = 0;
                    }
            }
        }

        #region Theme Settings

        private void InitializeThemeMap()
        {
            try
            {
                if (themeMapProperty == null)
                {
                    Debug.LogError("themeMapProperty is null");
                    return;
                }

                themeMapProperty.ClearArray();
                duplicateMapTypes.Clear();

                AddMapsFromReferenceMap(theme.AchievementSystemPrefab, themeMapProperty);
                AddMapsFromReferenceMap(theme.AchievementPrefab, themeMapProperty);
                AddMapsFromReferenceMap(theme.CategoryPrefab, themeMapProperty);

                // Check for duplicates
                for (int i = 0; i < themeMapProperty.arraySize; i++)
                {
                    SerializedProperty mapDataProperty = themeMapProperty.GetArrayElementAtIndex(i);

                    if (mapDataProperty == null)
                    {
                        Debug.LogError($"mapDataProperty is null at index {i}");
                        continue;
                    }

                    SerializedProperty mapTypeProperty = mapDataProperty.FindPropertyRelative("mapType");
                    if (mapTypeProperty == null)
                    {
                        Debug.LogError($"mapTypeProperty is null in mapDataProperty at index {i}");
                        continue;
                    }

                    MapType mapType = (MapType)mapTypeProperty.enumValueIndex;
                    if (duplicateMapTypes.ContainsKey(mapType))
                    {
                        duplicateMapTypes[mapType].Add(mapType.ToString());
                    }
                    else
                    {
                        duplicateMapTypes[mapType] = new List<string> { mapType.ToString() };
                    }
                }

                themeSerializedObject.ApplyModifiedProperties();
                InitializeReorderableLists();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error initializing theme map: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private void AddMapsFromReferenceMap(GameObject prefab, SerializedProperty themeMapProperty)
        {
            try
            {
                if (prefab == null)
                {
                    Debug.LogWarning("Prefab is null, skipping...");
                    return;
                }

                var referenceMaps = prefab.GetComponentsInChildren<ReferenceMap>();
                if (referenceMaps == null || referenceMaps.Length == 0)
                {
                    Debug.LogWarning($"No reference maps found in prefab: {prefab.name}");
                    return;
                }

                foreach (var referenceMap in referenceMaps)
                {
                    if (referenceMap == null)
                    {
                        Debug.LogWarning("Reference map is null, skipping...");
                        continue;
                    }

                    foreach (var map in referenceMap.Maps)
                    {
                        if (map == null)
                        {
                            Debug.LogWarning("Map is null, skipping...");
                            continue;
                        }

                        if (map.HideFromThemeEditor) continue;

                        // Determine the correct parent reference map for the heading
                        string parentReferenceMapName = referenceMap.ReferenceMapName;
                        SerializedProperty existingMapDataProperty = null;

                        for (int i = 0; i < themeMapProperty.arraySize; i++)
                        {
                            SerializedProperty mapDataProperty = themeMapProperty.GetArrayElementAtIndex(i);
                            if (mapDataProperty.FindPropertyRelative("mapType").enumValueIndex == (int)map.MapType &&
                                mapDataProperty.FindPropertyRelative("referenceMapParent").stringValue == parentReferenceMapName)
                            {
                                existingMapDataProperty = mapDataProperty;
                                break;
                            }
                        }

                        if (existingMapDataProperty != null)
                        {
                            if (map.TryGetComponent<Image>(out var imageComponent))
                            {
                                existingMapDataProperty.FindPropertyRelative("sprite").objectReferenceValue = imageComponent.sprite;
                                existingMapDataProperty.FindPropertyRelative("spriteColor").colorValue = imageComponent.color;
                                //Debug.Log($"Updated existing map data for {map.MapType} in {parentReferenceMapName}");
                            }
                            else
                            {
                                Debug.LogError($"Component 'Image' not found on {map.gameObject.name}");
                            }
                        }
                        else
                        {
                            if (map.TryGetComponent<Image>(out var imageComponent))
                            {
                                int newIndex = themeMapProperty.arraySize;
                                themeMapProperty.InsertArrayElementAtIndex(newIndex);
                                SerializedProperty newMapDataProperty = themeMapProperty.GetArrayElementAtIndex(newIndex);

                                newMapDataProperty.FindPropertyRelative("mapType").enumValueIndex = (int)map.MapType;
                                newMapDataProperty.FindPropertyRelative("sprite").objectReferenceValue = imageComponent.sprite;
                                newMapDataProperty.FindPropertyRelative("spriteColor").colorValue = imageComponent.color;
                                newMapDataProperty.FindPropertyRelative("referenceMapParent").stringValue = parentReferenceMapName;

                                //Debug.Log($"Added new map data for {map.MapType} in {parentReferenceMapName}");
                            }
                            else
                            {
                                Debug.LogError($"Component 'Image' not found on {map.gameObject.name}");
                            }
                        }
                    }
                }

                themeSerializedObject.ApplyModifiedProperties();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error adding maps from reference map for prefab: {prefab?.name}\n{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void FilterReorderableList(string key)
        {
            if (!originalThemeMaps.ContainsKey(key)) return;

            var originalList = originalThemeMaps[key];
            var filteredMaps = new List<Theme.MapData>();

            foreach (var mapData in originalList)
            {
                if (mapData == null) continue;

                var matchesSearch = mapData.MapType.ToString().ToLower().Contains(searchString.ToLower());

                if (matchesSearch)
                {
                    filteredMaps.Add(mapData);
                }
            }

            reorderableLists[key].list = filteredMaps;
        }

        private void DrawThemeTabGUI()
        {
            // Update themeSerializedObject if themeProp reference has changed
            if (themeProp.objectReferenceValue != null && (themeSerializedObject == null || themeSerializedObject.targetObject != themeProp.objectReferenceValue))
                themeSerializedObject = new SerializedObject(themeProp.objectReferenceValue);

            Utilities.EditorGUILayoutField("Loaded Theme Pack", themeProp.objectReferenceValue, readOnly: true);
            if (GUILayout.Button("Select Theme Pack"))
            {
                LoadThemePack();
            }

            EditorGUILayout.Space(5);

            if (GUILayout.Button("Apply theme changes to prefabs"))
            {
                bool proceed = EditorUtility.DisplayDialog("Apply Theme to prefabs and scene instance",
                     $"Are you sure you want to update the prefabs in the database with the theme changes? These changes will apply to the ({Path.GetFileName(themePackLocation)}) Theme pack.",
                      "OK", "Cancel");
                if (proceed)
                {
                    if (SetTheme())
                    {
                        InitializeThemeMap();
                        InitializeReorderableLists();
                    }
                    else
                        Debug.LogError("Error Setting Theme");
                    themeSerializedObject.Update();
                }
            }

            EditorGUILayout.Space(5);

            // Draw the color fields grouped by headings
            DrawColorFields();

            EditorGUILayout.Space(10);
            if (GUILayout.Button("Initialize Map List"))
            {
                InitializeThemeMap();
                InitializeReorderableLists();
                themeSerializedObject.Update(); // Update the serialized object after initialization
            }

            // Search field
            EditorGUILayout.LabelField("Search", EditorStyles.boldLabel);
            searchString = EditorGUILayout.TextField(searchString).Trim().Replace(" ", "");

            EditorGUILayout.Space(5);

            // Display ReorderableLists with foldout sections
            foreach (var key in reorderableLists.Keys.ToList())
            {
                if (!foldoutStates.ContainsKey(key))
                    foldoutStates[key] = true;

                foldoutStates[key] = EditorGUILayout.Foldout(foldoutStates[key], key, true);
                if (foldoutStates[key])
                {
                    FilterReorderableList(key);
                    reorderableLists[key].DoLayoutList();
                }
            }

            themeSerializedObject.ApplyModifiedProperties();
        }

        private void DrawColorFields()
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Category Colors", EditorStyles.boldLabel);

            theme.CategoryButtonTextColor = Utilities.EditorGUILayoutField("Category Button Text Color", theme.CategoryButtonTextColor);
            theme.CategoryTextColor = Utilities.EditorGUILayoutField("Category Text Color", theme.CategoryTextColor);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Achievement System Colors", EditorStyles.boldLabel);

            theme.AchievementSystemHeadingTextColor = Utilities.EditorGUILayoutField("Achievement System Heading Text Color", theme.AchievementSystemHeadingTextColor);
            theme.AchievementSystemCategoryHeadingTextColor = Utilities.EditorGUILayoutField("Achievement System Category Heading Text Color", theme.AchievementSystemCategoryHeadingTextColor);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Achievement Colors", EditorStyles.boldLabel);

            theme.AchievementHeadingTextColor = Utilities.EditorGUILayoutField("Achievement Heading Text Color", theme.AchievementHeadingTextColor);
            theme.AchievementDescriptionTextColor = Utilities.EditorGUILayoutField("Achievement Description Text Color", theme.AchievementDescriptionTextColor);
            theme.AchievementPointsTextColor = Utilities.EditorGUILayoutField("Achievement Points Text Color", theme.AchievementPointsTextColor);
            theme.AchievementProgressTextColor = Utilities.EditorGUILayoutField("Achievement Progress Text Color", theme.AchievementProgressTextColor);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Search Field Colors", EditorStyles.boldLabel);

            theme.SearchFieldPlaceHolderTextColor = Utilities.EditorGUILayoutField("Search Field Place Holder Text Color", theme.SearchFieldPlaceHolderTextColor);
            theme.SearchFieldTextColor = Utilities.EditorGUILayoutField("Search Field Text Color", theme.SearchFieldTextColor);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Achievement Complete Colors", EditorStyles.boldLabel);

            theme.AchievementCompleteHeadingTextColor = Utilities.EditorGUILayoutField("Achievement Complete Heading Text Color", theme.AchievementCompleteHeadingTextColor);
            theme.AchievementCompleteDescriptionTextColor = Utilities.EditorGUILayoutField("Achievement Complete Description Text Color", theme.AchievementCompleteDescriptionTextColor);
            theme.AchievementCompleteAchievementPointsTextColor = Utilities.EditorGUILayoutField("Achievement Complete Achievement Points Text Color", theme.AchievementCompleteAchievementPointsTextColor);
        }

        private void InitializeReorderableLists()
        {
            reorderableLists.Clear();
            originalThemeMaps.Clear();

            var referenceMaps = FindReferenceMaps();
            foreach (var refMap in referenceMaps)
            {
                AddReorderableList(refMap);
            }
        }

        private List<ReferenceMap> FindReferenceMaps()
        {
            List<ReferenceMap> referenceMaps = new List<ReferenceMap>();
            if (theme.AchievementSystemPrefab != null)
                referenceMaps.AddRange(theme.AchievementSystemPrefab.GetComponentsInChildren<ReferenceMap>());
            if (theme.AchievementPrefab != null)
                referenceMaps.AddRange(theme.AchievementPrefab.GetComponentsInChildren<ReferenceMap>());
            if (theme.CategoryPrefab != null)
                referenceMaps.AddRange(theme.CategoryPrefab.GetComponentsInChildren<ReferenceMap>());
            return referenceMaps;
        }

        private void AddReorderableList(ReferenceMap referenceMap)
        {
            if (referenceMap == null || referenceMap.Maps == null)
            {
                return;
            }

            var key = referenceMap.ReferenceMapName;
            var filteredThemeMap = theme.ThemeMap.FindAll(mapData => referenceMap.Maps.Exists(map => map.MapType == mapData.MapType && mapData.ReferenceMapParent == key));

            var list = new ReorderableList(filteredThemeMap, typeof(Theme.MapData), true, true, false, false)
            {
                drawHeaderCallback = rect =>
                {
                    EditorGUI.LabelField(rect, key);
                },
                drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    var listToDraw = (List<Theme.MapData>)reorderableLists[key].list;

                    if (index >= listToDraw.Count) return;

                    var element = listToDraw[index];
                    rect.y += 2;
                    float lineHeight = EditorGUIUtility.singleLineHeight + 4;

                    bool duplicateFound = duplicateMapTypes.ContainsKey(element.MapType) && duplicateMapTypes[element.MapType].Contains(key);

                    // Draw MapType and check for duplicates
                    EditorGUI.LabelField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), "Map Type:");
                    var mapTypeString = Utilities.ToCamelCase(element.MapType.ToString());
                    var mapTypeColor = duplicateFound ? Color.red : GUI.contentColor;
                    EditorGUI.LabelField(new Rect(rect.x + 105, rect.y, rect.width - 105, EditorGUIUtility.singleLineHeight), mapTypeString, new GUIStyle { normal = new GUIStyleState { textColor = mapTypeColor } });

                    if (duplicateFound)
                    {
                        rect.y += lineHeight;
                        EditorGUI.HelpBox(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight * 2), $"Duplicate MapType found in: {string.Join(", ", duplicateMapTypes[element.MapType])}", MessageType.Error);
                        rect.y += lineHeight;
                    }

                    rect.y += lineHeight;

                    // Draw Sprite
                    EditorGUI.LabelField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), "Sprite:");
                    element.Sprite = (Sprite)EditorGUI.ObjectField(new Rect(rect.x + 105, rect.y, rect.width - 105, EditorGUIUtility.singleLineHeight), element.Sprite, typeof(Sprite), true);

                    // Draw Sprite Color
                    rect.y += lineHeight;
                    EditorGUI.LabelField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), "Sprite Color:");
                    element.SpriteColor = EditorGUI.ColorField(new Rect(rect.x + 105, rect.y, rect.width - 105, EditorGUIUtility.singleLineHeight), element.SpriteColor);

                    rect.y += lineHeight;

                    // Adjust height if there's a duplicate error message
                    if (duplicateFound)
                        rect.y += lineHeight * 2;
                },
                elementHeightCallback = index =>
                {
                    var listToDraw = (List<Theme.MapData>)reorderableLists[key].list;

                    if (index >= listToDraw.Count) return 0;

                    float lineHeight = EditorGUIUtility.singleLineHeight + 2;
                    float height = lineHeight * 4; // base height

                    if (duplicateMapTypes.ContainsKey(listToDraw[index].MapType) && duplicateMapTypes[listToDraw[index].MapType].Contains(key))
                    {
                        height += lineHeight * 2; // extra space for error message
                    }

                    return height;
                }
            };

            reorderableLists[key] = list;
            originalThemeMaps[key] = new List<Theme.MapData>(filteredThemeMap);
        }

        #endregion Theme Settings

        private void DrawGenerateCategoryEnums()
        {
            GenerateCategoryEnumFoldout = EditorGUILayout.Foldout(GenerateCategoryEnumFoldout, "Generate Category Enums", true);
            if (GenerateCategoryEnumFoldout)
            {
                DrawCategoryNamesList();

                if (GUILayout.Button("Generate Category Enums"))
                {
                    var enumNames = new HashSet<string>();
                    bool hasError = false;
                    string errorMessage = "";

                    foreach (var name in Database.CategoryNames)
                    {
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            errorMessage = "Enum name is empty or whitespace.";
                            hasError = true;
                            break;
                        }
                        if (!System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(name))
                        {
                            errorMessage = $"Invalid enum name: {name}";
                            hasError = true;
                            break;
                        }
                        if (enumNames.Contains(name))
                        {
                            errorMessage = $"Duplicate enum name found: {name}";
                            hasError = true;
                            break;
                        }
                        enumNames.Add(name);
                    }

                    if (hasError)
                    {
                        EditorUtility.DisplayDialog("Error Generating Enums", errorMessage, "OK");
                    }
                    else
                    {
                        string assetDirectoryPath = Application.dataPath + "/RealSoftGames/AdvancedAchievementSystem/Scripts/Generated";
                        Utilities.GenerateEnumScript("CategoryList", Database.CategoryNames, assetDirectoryPath, "CategoriesEnums", new string[] { "AdvancedAchievementSystem" }, new string[] { "All" });
                        UnityEditor.AssetDatabase.Refresh();
                        EditorUtility.DisplayDialog("Enums Generated", "The enums have been successfully generated.", "OK");
                    }
                }
            }
        }

        private void DrawCategoryNamesList()
        {
            so.Update();
            EditorGUILayout.LabelField("Category Names", EditorStyles.boldLabel);
            EditorGUI.BeginChangeCheck();

            // Calculate the total height of all elements
            float totalHeight = categoryNames.arraySize * EditorGUIUtility.singleLineHeight;
            // Calculate extra height for padding between elements
            float extraHeight = categoryNames.arraySize * EditorGUIUtility.standardVerticalSpacing;
            // Define a minimum and maximum height for the scroll view
            float minHeight = 100f; // Minimum height for the scroll view
            float maxHeight = Mathf.Min(300f, totalHeight + extraHeight); // Max height based on content

            // Begin a scroll view with dynamic height
            scrollPositionCategoryNames = EditorGUILayout.BeginScrollView(scrollPositionCategoryNames, GUILayout.MinHeight(minHeight), GUILayout.MaxHeight(maxHeight));

            // Iterate over all the category names and make a property field for each
            for (int i = 0; i < categoryNames.arraySize; i++)
            {
                SerializedProperty property = categoryNames.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(property);
            }

            // End the scroll view
            EditorGUILayout.EndScrollView();

            // Display a warning message
            EditorGUILayout.HelpBox("Warning: Removing fields that have been previously implemented in your project may cause errors. Please manually adjust the enum values accordingly if you remove fields.", MessageType.Warning);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add New Field"))
            {
                categoryNames.InsertArrayElementAtIndex(categoryNames.arraySize);
                SerializedProperty newElement = categoryNames.GetArrayElementAtIndex(categoryNames.arraySize - 1);
                newElement.stringValue = "";
            }

            if (categoryNames.arraySize > 0 && GUILayout.Button("Remove Last Field"))
            {
                bool result = EditorUtility.DisplayDialog("Remove Field", "Are you sure you want to remove this field? \n\nAny achievements or categories associated with this field will default back to element 0", "Yes", "No");
                if (result)
                    categoryNames.DeleteArrayElementAtIndex(categoryNames.arraySize - 1);
            }
            GUILayout.EndHorizontal();

            so.ApplyModifiedProperties();
        }

        private void DrawAchievementNamesList()
        {
            so.Update();
            EditorGUILayout.LabelField("Achievement Names", EditorStyles.boldLabel);
            EditorGUI.BeginChangeCheck();

            // Calculate the total height of all elements
            float elementHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing; // Height per element (including spacing)
            float totalContentHeight = elementHeight * achievementNames.arraySize; // Total content height
            float minHeight = 100f; // Minimum height for the scroll view
            float maxHeight = 300f; // Maximum height for the scroll view

            // Calculate the height of the scroll view based on content
            float scrollViewHeight = Mathf.Clamp(totalContentHeight, minHeight, maxHeight);

            // Begin a scroll view with dynamic height based on the content
            scrollPositionAchievementNames = EditorGUILayout.BeginScrollView(scrollPositionAchievementNames, GUILayout.MinHeight(minHeight), GUILayout.MaxHeight(scrollViewHeight));

            // Iterate over all the achievement names and make a property field for each
            for (int i = 0; i < achievementNames.arraySize; i++)
            {
                SerializedProperty property = achievementNames.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(property);
            }

            // End the scroll view
            EditorGUILayout.EndScrollView();

            // Display a warning message
            EditorGUILayout.HelpBox("Warning: Removing fields that have been previously implemented in your project may cause errors. Please manually adjust the enum values accordingly if you remove fields.", MessageType.Warning);

            // Horizontal group for buttons
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add New Field"))
            {
                achievementNames.InsertArrayElementAtIndex(achievementNames.arraySize);
                SerializedProperty newElement = achievementNames.GetArrayElementAtIndex(achievementNames.arraySize - 1);
                newElement.stringValue = "";
            }
            if (achievementNames.arraySize > 0 && GUILayout.Button("Remove Last Field"))
            {
                bool result = EditorUtility.DisplayDialog("Remove Field", "Are you sure you want to remove this field? \n\nAny achievements or categories associated with this field will default back to element 0", "Yes", "No");
                if (result)
                    achievementNames.DeleteArrayElementAtIndex(achievementNames.arraySize - 1);
            }
            GUILayout.EndHorizontal();

            so.ApplyModifiedProperties();
        }

        private void DrawAchievementKeysList()
        {
            so.Update();
            EditorGUILayout.LabelField("Achievement Keys", EditorStyles.boldLabel);
            EditorGUI.BeginChangeCheck();

            // Calculate the total height of all elements
            float elementHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing; // Height per element (including spacing)
            float totalContentHeight = elementHeight * achievementNames.arraySize; // Total content height
            float minHeight = 100f; // Minimum height for the scroll view
            float maxHeight = 300f; // Maximum height for the scroll view

            // Calculate the height of the scroll view based on content
            float scrollViewHeight = Mathf.Clamp(totalContentHeight, minHeight, maxHeight);

            // Begin a scroll view with dynamic height based on the content
            scrollPositionAchievementKeys = EditorGUILayout.BeginScrollView(scrollPositionAchievementKeys, GUILayout.MinHeight(minHeight), GUILayout.MaxHeight(scrollViewHeight));

            // Iterate over all the achievement names and make a property field for each
            for (int i = 0; i < achievementKeys.arraySize; i++)
            {
                SerializedProperty property = achievementKeys.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(property);
            }

            // End the scroll view
            EditorGUILayout.EndScrollView();

            // Display a warning message
            EditorGUILayout.HelpBox("Warning: Removing fields that have been previously implemented in your project may cause errors. Please manually adjust the enum values accordingly if you remove fields.", MessageType.Warning);

            // Horizontal group for buttons
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add New Field"))
            {
                achievementKeys.InsertArrayElementAtIndex(achievementKeys.arraySize);
                SerializedProperty newElement = achievementKeys.GetArrayElementAtIndex(achievementKeys.arraySize - 1);
                newElement.stringValue = "";
            }
            if (achievementKeys.arraySize > 0 && GUILayout.Button("Remove Last Field"))
            {
                bool result = EditorUtility.DisplayDialog("Remove Field", "Are you sure you want to remove this field? \n\nAny achievements or categories associated with this field will default back to element 0", "Yes", "No");
                if (result)
                    achievementKeys.DeleteArrayElementAtIndex(achievementKeys.arraySize - 1);
            }
            GUILayout.EndHorizontal();

            so.ApplyModifiedProperties();
        }

        private void DrawGenerateAchievementEnums()
        {
            GenerateAchievementEnumFoldout = EditorGUILayout.Foldout(GenerateAchievementEnumFoldout, "Generate Achievement Enums", true);
            if (GenerateAchievementEnumFoldout)
            {
                DrawAchievementNamesList();

                if (GUILayout.Button("Generate AchievementName Enums"))
                {
                    var enumNames = new HashSet<string>();
                    bool hasError = false;
                    string errorMessage = "";

                    foreach (var name in Database.AchievementNames)
                    {
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            errorMessage = "Enum name is empty or whitespace.";
                            hasError = true;
                            break;
                        }
                        if (!System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(name))
                        {
                            errorMessage = $"Invalid enum name: {name}";
                            hasError = true;
                            break;
                        }
                        if (enumNames.Contains(name))
                        {
                            errorMessage = $"Duplicate enum name found: {name}";
                            hasError = true;
                            break;
                        }
                        enumNames.Add(name);
                    }

                    if (hasError)
                    {
                        EditorUtility.DisplayDialog("Error Generating Enums", errorMessage, "OK");
                    }
                    else
                    {
                        string assetDirectoryPath = Application.dataPath + "/RealSoftGames/AdvancedAchievementSystem/Scripts/Generated";
                        Utilities.GenerateEnumScript("AchievementList", Database.AchievementNames, assetDirectoryPath, "AchievementsEnums", new string[] { "AdvancedAchievementSystem" });
                        UnityEditor.AssetDatabase.Refresh();
                        EditorUtility.DisplayDialog("Enums Generated", "The enums have been successfully generated.", "OK");
                    }
                }
            }
        }

        private void DrawGenerateAchievementKeyEnums()
        {
            GenerateAchievementKeyEnumFoldout = EditorGUILayout.Foldout(GenerateAchievementKeyEnumFoldout, "Generate Achievement Key Enums", true);
            if (GenerateAchievementKeyEnumFoldout)
            {
                DrawAchievementKeysList();

                if (GUILayout.Button("Generate Achievement Key Enums"))
                {
                    var enumNames = new HashSet<string>();
                    bool hasError = false;
                    string errorMessage = "";

                    foreach (var name in Database.AchievementKeys)
                    {
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            errorMessage = "Enum name is empty or whitespace.";
                            hasError = true;
                            break;
                        }
                        if (!System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(name))
                        {
                            errorMessage = $"Invalid enum name: {name}";
                            hasError = true;
                            break;
                        }
                        if (enumNames.Contains(name))
                        {
                            errorMessage = $"Duplicate enum name found: {name}";
                            hasError = true;
                            break;
                        }
                        enumNames.Add(name);
                    }

                    if (hasError)
                    {
                        EditorUtility.DisplayDialog("Error Generating Enums", errorMessage, "OK");
                    }
                    else
                    {
                        string assetDirectoryPath = Application.dataPath + "/RealSoftGames/AdvancedAchievementSystem/Scripts/Generated";
                        Utilities.GenerateEnumScript("AchievementKey", Database.AchievementKeys, assetDirectoryPath, "AchievementKeysEnums", new string[] { "AdvancedAchievementSystem" });
                        UnityEditor.AssetDatabase.Refresh();
                        EditorUtility.DisplayDialog("Enums Generated", "The enums have been successfully generated.", "OK");
                    }
                }
            }
        }

        private void DrawGenerateCategoriesButton()
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fontStyle = FontStyle.Bold,
                fontSize = 12,
                fixedHeight = 25
            };

            if (GUILayout.Button(new GUIContent("Generate Categories"), buttonStyle))
                GenerateCategories();
        }

        private void DrawGenerateAchievementsButton()
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fontStyle = FontStyle.Bold,
                fontSize = 12,
                fixedHeight = 25
            };

            if (GUILayout.Button(new GUIContent("Generate Achievements"), buttonStyle))
                GenerateAchievements();
        }

        private void DrawDisplayTypeGUI()
        {
            if (so != null)
            {
                Utilities.SearchableEnumPopup(display, "Display Type");
                so.ApplyModifiedProperties();
                so.Update();
            }
            EditorGUILayout.Space();
        }

        private void DrawSearchAchievementColors()
        {
            themeSerializedObject.Update();
            searchFieldFoldout = EditorGUILayout.Foldout(searchFieldFoldout, "Search Field Colors", true);
            if (searchFieldFoldout)
            {
                Database.Theme.SearchFieldPlaceHolderTextColor = Utilities.EditorGUILayoutField("Search Placeholder Text Color", Database.Theme.SearchFieldPlaceHolderTextColor);
                Database.Theme.SearchFieldTextColor = Utilities.EditorGUILayoutField("Search Field Text Color", Database.Theme.SearchFieldTextColor);

                EditorGUILayout.Space();
                if (GUILayout.Button("Apply Searchfield Colors"))
                {
                    achievementManagerSceneInstance.Map.Maps.Find(m => m.MapType == MapType.AchievementSearchText).GetValue<TMP_Text>().color = Database.Theme.SearchFieldTextColor;
                    achievementManagerSceneInstance.Map.Maps.Find(m => m.MapType == MapType.AchievementSearchFieldInputFieldPlaceHolder).GetValue<TMP_Text>().color = Database.Theme.SearchFieldPlaceHolderTextColor;
                }
            }

            HeadingColors = EditorGUILayout.Foldout(HeadingColors, "Heading Collors", true);
            if (HeadingColors)
            {
                Utilities.EditorGUILayoutField<Color>(themeSerializedObject.FindProperty("achievementSystemCategoryHeadingTextColor"), "Category Heading Text Color");
                Utilities.EditorGUILayoutField<Color>(themeSerializedObject.FindProperty("achievementSystemHeadingTextColor"), "Achievement System Heading Text Color");
                Utilities.EditorGUILayoutField<Color>(themeSerializedObject.FindProperty("achievementSystemAPHeadingTextColor"), "Achievement System Achievement Points Text Color");

                EditorGUILayout.Space();
                if (GUILayout.Button("Apply Heading Colors"))
                {
                    achievementManagerSceneInstance.Map.GetType<TMP_Text>(MapType.AchievementSystemHeadingText).color = Database.Theme.AchievementSystemHeadingTextColor;
                    achievementManagerSceneInstance.Map.GetType<TMP_Text>(MapType.AchievementSystemCategoryHeadingText).color = Database.Theme.AchievementSystemCategoryHeadingTextColor;
                    achievementManagerSceneInstance.Map.GetType<TMP_Text>(MapType.AchievementCompletionText).color = Database.Theme.AchievementSystemAPHeadingTextColor;
                }
            }
            EditorGUILayout.Space();
            themeSerializedObject.ApplyModifiedProperties();
        }


        #region Draw Achievement Complete

        void DrawAchievementComplete()
        {
            if (achievementManagerSceneInstance?.AchievementCompleteAlphaGroup != null)
                if (achievementManagerSceneInstance.AchievementCompleteAlphaGroup.alpha == 0)
                {
                    if (GUILayout.Button("Enable Achievement Complete"))
                        achievementManagerSceneInstance.AchievementCompleteAlphaGroup.alpha = 1;
                }
                else
                {
                    if (GUILayout.Button("Disable Achievement Complete"))
                        achievementManagerSceneInstance.AchievementCompleteAlphaGroup.alpha = 0;
                }

            if (GUILayout.Button("Simulate Achievement Completion"))
            {
                StartSimulation();
            }

            if (fadeState != FadeState.None)
            {
                string stateLabel = "";
                switch (fadeState)
                {
                    case FadeState.FadingIn:
                        stateLabel = "Fading In";
                        break;
                    case FadeState.Displaying:
                        stateLabel = "Displaying";
                        break;
                    case FadeState.FadingOut:
                        stateLabel = "Fading Out";
                        break;
                }

                float remainingTime = Mathf.Max(0, duration - ((float)EditorApplication.timeSinceStartup - startTime));
                EditorGUILayout.LabelField($"{stateLabel} - {remainingTime:F2} seconds remaining");
            }

            GameObject achievementSystemPrefab = so.FindProperty("achievementSystemPrefab").objectReferenceValue as GameObject;
            if (achievementSystemPrefab == null)
            {
                EditorGUILayout.HelpBox("Achievement System Prefab is not assigned.", MessageType.Error);
                return;
            }

            var achievementManagerPrefab = Database.AchievementSystemPrefab.GetComponent<AchievementManager>();
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(so.FindProperty("displayTime"));
            EditorGUILayout.PropertyField(so.FindProperty("fadeInTime"));
            EditorGUILayout.PropertyField(so.FindProperty("fadeOutTime"));

            if (EditorGUI.EndChangeCheck())
            {
                achievementManagerPrefab.DisplayTime = so.FindProperty("displayTime").floatValue;
                achievementManagerPrefab.FadeInTime = so.FindProperty("fadeInTime").floatValue;
                achievementManagerPrefab.FadeOutTime = so.FindProperty("fadeOutTime").floatValue;

                achievementManagerSceneInstance.DisplayTime = so.FindProperty("displayTime").floatValue;
                achievementManagerSceneInstance.FadeInTime = so.FindProperty("fadeInTime").floatValue;
                achievementManagerSceneInstance.FadeOutTime = so.FindProperty("fadeOutTime").floatValue;
                so.ApplyModifiedProperties();
                PrefabUtility.ApplyPrefabInstance(achievementManagerSceneInstance.gameObject, InteractionMode.UserAction);
            }
        }

        private void StartSimulation()
        {
            if (achievementManagerSceneInstance?.AchievementCompleteAlphaGroup == null)
            {
                Debug.LogError("AchievementCompleteAlphaGroup is not assigned.");
                return;
            }

            alphaGroup = achievementManagerSceneInstance.AchievementCompleteAlphaGroup;
            fadeState = FadeState.FadingIn;
            startTime = (float)EditorApplication.timeSinceStartup;
            duration = achievementManagerSceneInstance.FadeInTime;
        }

        #endregion Draw Achievement Complete


        #region  Draw Achievements

        private void DrawAchievementsList()
        {
            if (so != null)
            {
                so.Update(); // Ensure the SerializedObject is up-to-date


                GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 12,
                    fixedHeight = 25
                };


                // Draw the search field and process any changes
                EditorGUILayout.Space(5);
                if (GUILayout.Button("Sort Achievements", buttonStyle))
                    SortAchievements();

                EditorGUILayout.Space(10);
                DrawSearchField();
                EditorGUILayout.Space(5);

                achievementList.drawHeaderCallback = (Rect rect) =>
                          {
                              EditorGUI.LabelField(rect, "Achievements");
                          };

                achievementList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    if (index >= so.FindProperty("achievements").arraySize) return;

                    var element = so.FindProperty("achievements").GetArrayElementAtIndex(index);

                    // Skip drawing this element entirely if it doesn't match the filter
                    if (!element.FindPropertyRelative("achievementName").stringValue.Contains(achievementSearchString, StringComparison.OrdinalIgnoreCase))
                        return;

                    rect.y += 2;

                    // Check for error conditions
                    bool hasError = element.FindPropertyRelative("complete").boolValue ||
                                    HasDuplicateKeys(element.FindPropertyRelative("keys")) ||
                                    element.FindPropertyRelative("neededValue").intValue < 1 ||
                                    element.FindPropertyRelative("achievementPoints").intValue < 0 ||
                                    element.FindPropertyRelative("currentValue").intValue > 0;

                    // Set the color for the heading
                    Color originalColor = GUI.color;
                    if (hasError)
                        GUI.color = Color.red;


                    element.isExpanded = Utilities.EditorGUIFoldout(ref rect, element, $"ID [{index}] {element.FindPropertyRelative("achievementName").stringValue}", element.isExpanded);

                    // Restore the original color
                    GUI.color = originalColor;

                    if (element.isExpanded)
                    {
                        EditorGUI.indentLevel += 2;

                        windowHeight += 85f;

                        Utilities.EditorGUIField(ref rect, element.FindPropertyRelative("achievementName"), "Achievement Name");
                        Utilities.SearchableEnumPopup(ref rect, element.FindPropertyRelative("achievement"), "Achievement");
                        Utilities.EditorGUITextArea(ref rect, element.FindPropertyRelative("description"), "Description", 4);
                        Utilities.SearchableEnumPopup(ref rect, element.FindPropertyRelative("category"), "Category");
                        Utilities.EditorGUILabel(ref rect, $"Achievement Search Key: {element.FindPropertyRelative("searchString").stringValue}");
                        //Utilities.EditorGUIIntSlider(ref rect, element.FindPropertyRelative("currentValue"), 0, element.FindPropertyRelative("neededValue").intValue, "Current Value");

                        if (element.FindPropertyRelative("neededValue").intValue < 1)
                            GUI.color = Color.red;
                        Utilities.EditorGUIField(ref rect, element.FindPropertyRelative("neededValue"), "Needed Value");
                        GUI.color = originalColor;
                        if (element.FindPropertyRelative("achievementPoints").intValue < 0)
                            GUI.color = Color.red;
                        Utilities.EditorGUIField(ref rect, element.FindPropertyRelative("achievementPoints"), "Achievement Points");
                        GUI.color = originalColor;

                        Utilities.EditorGUIField(ref rect, element.FindPropertyRelative("hiddenAchievement"), "Hidden Achievement");
                        //Utilities.EditorGUIField(ref rect, element.FindPropertyRelative("complete"), "Achievement Complete");
                        Utilities.EditorGUIField(ref rect, element.FindPropertyRelative("image"), "Image");
                        Utilities.EditorGUIField(ref rect, element.FindPropertyRelative("requiresCompletedAchievement"), "Requires Completed Achievement");
                        if (element.FindPropertyRelative("requiresCompletedAchievement").boolValue)
                        {
                            EditorGUI.indentLevel++;
                            Utilities.SearchableEnumPopup(ref rect, element.FindPropertyRelative("requiresCompletedAchievementID"), "Required Achievement");
                            EditorGUI.indentLevel--;
                        }
                        element.FindPropertyRelative("achievementReward").objectReferenceValue = Utilities.EditorGUIField(ref rect, "Achievement Reward", (AchievementReward)element.FindPropertyRelative("achievementReward").objectReferenceValue);


                        if (HasDuplicateKeys(element.FindPropertyRelative("keys")))
                            GUI.color = Color.red;

                        SerializedProperty keysProp = element.FindPropertyRelative("keys");
                        ReorderableList keysList = GetOrCreateAchievementKeysList(index, keysProp);
                        keysList.DoList(new Rect(rect.x, rect.y, rect.width, keysList.GetHeight()));
                        GUI.color = originalColor;
                        EditorGUI.indentLevel -= 2;
                    }
                };

                achievementList.elementHeightCallback = index =>
                {
                    var element = achievementList.serializedProperty.GetArrayElementAtIndex(index);
                    string achievementName = element.FindPropertyRelative("achievementName").stringValue;

                    // Only allocate space for elements that match the filter
                    if (!achievementName.Contains(achievementSearchString, StringComparison.OrdinalIgnoreCase))
                        return 0;  // Ensure this is 0 to completely hide non-matching elements

                    float baseHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    if (!element.isExpanded)
                        return baseHeight;

                    float expandedHeight = baseHeight + (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * (element.FindPropertyRelative("requiresCompletedAchievement").boolValue ? 14 : 13);
                    if (element.isExpanded)
                    {
                        expandedHeight += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                        if (achievementKeysLists.TryGetValue(index, out ReorderableList keysList))
                        {
                            expandedHeight += keysList.GetHeight() + EditorGUIUtility.standardVerticalSpacing * 2;
                        }
                    }
                    return expandedHeight;
                };


                achievementList.DoLayoutList(); // Render the list based on current filter
                so.ApplyModifiedProperties(); // Apply any changes made by the inspector

                if (errorMessages.Count > 0)
                {
                    EditorGUILayout.HelpBox($"Errors found in the following achievements: {string.Join(", ", errorMessages)}", MessageType.Error);
                }
            }
        }

        private void DrawSearchField()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Search:", GUILayout.Width(50));
            string newSearchString = EditorGUILayout.TextField(achievementSearchString, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndHorizontal();

            if (newSearchString != achievementSearchString)
            {
                achievementSearchString = newSearchString;
                achievementList.index = -1;  // Reset selection
                achievementList.serializedProperty.serializedObject.Update();  // Refresh the serialized object
            }
        }

        private void SortAchievements()
        {
            // Step 1: Sort the achievements in the Database
            Database.Achievements.Sort((a, b) => a.AchievementName.CompareTo(b.AchievementName));

            // Step 2: Apply the sorted order to SerializedProperty
            SerializedProperty achievements = so.FindProperty("achievements");

            for (int i = 0; i < Database.Achievements.Count; i++)
            {
                SerializedProperty prop = achievements.GetArrayElementAtIndex(i);
                // Assuming each achievement has a unique ID or some identifier that you can use to match
                BaseAchievement sortedAchievement = Database.Achievements[i];

                // Update properties
                prop.FindPropertyRelative("achievementName").stringValue = sortedAchievement.AchievementName;
                prop.FindPropertyRelative("category").enumValueIndex = (int)sortedAchievement.Category;
                // Apply other properties similarly...
            }

            so.ApplyModifiedProperties();  // Apply changes to the serialized object
        }

        private bool HasDuplicateKeys(SerializedProperty keysProp)
        {
            HashSet<int> keysSet = new HashSet<int>();
            for (int i = 0; i < keysProp.arraySize; i++)
            {
                var keyProp = keysProp.GetArrayElementAtIndex(i);
                if (keyProp.propertyType == SerializedPropertyType.Enum)
                {
                    int key = keyProp.enumValueIndex;
                    if (!keysSet.Add(key))
                    {
                        return true; // Duplicate found
                    }
                }
            }
            return false;
        }

        private ReorderableList GetOrCreateAchievementKeysList(int index, SerializedProperty keysProp)
        {
            if (!achievementKeysLists.TryGetValue(index, out ReorderableList keysList))
            {
                keysList = new ReorderableList(keysProp.serializedObject, keysProp, true, true, true, true);
                keysList.drawElementCallback = (Rect keyRect, int keyIndex, bool isActive, bool isFocused) =>
                {
                    SerializedProperty keyElement = keysProp.GetArrayElementAtIndex(keyIndex);
                    keyRect.y += 2;
                    Utilities.SearchableEnumPopup(ref keyRect, keyElement, "Achievement Key");
                };

                keysList.drawHeaderCallback = (Rect keyRect) =>
                {
                    EditorGUI.LabelField(keyRect, "Keys");
                };

                achievementKeysLists[index] = keysList;
            }
            return keysList;
        }

        #endregion Draw Achievements

        private void DrawCategoriesList()
        {
            categoriesList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Category");

            categoriesList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                SerializedProperty categoryElement = categoriesList.serializedProperty.GetArrayElementAtIndex(index);

                rect.y += 2;
                rect.height = (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);

                categoryElement.isExpanded = Utilities.EditorGUIFoldout(ref rect, categoryElement, categoryElement.FindPropertyRelative("buttonName").stringValue, categoryElement.isExpanded);

                if (categoryElement.isExpanded)
                {
                    Utilities.EditorGUIField(ref rect, categoryElement.FindPropertyRelative("buttonName"), "Button Name");
                    Utilities.SearchableEnumPopup(ref rect, categoryElement.FindPropertyRelative("category"), "Category");
                    SerializedProperty subCategoriesProp = categoryElement.FindPropertyRelative("subCategories");
                    ReorderableList subCategoriesList = GetOrCreateSubCategoriesList(index, subCategoriesProp);

                    subCategoriesList.DoList(new Rect(rect.x, rect.y, rect.width, subCategoriesList.GetHeight()));
                }
            };

            categoriesList.elementHeightCallback = index =>
            {
                SerializedProperty categoryElement = categoriesList.serializedProperty.GetArrayElementAtIndex(index);
                if (!categoryElement.isExpanded)
                    return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                float height = (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 3; // Base height for category name and button name fields

                if (subCategoriesLists.TryGetValue(index, out ReorderableList subCategoriesList))
                    height += subCategoriesList.GetHeight() + EditorGUIUtility.standardVerticalSpacing; // Add the height of the subcategories achievementList

                return height;
            };

            #region Draw Categories List

            if (so != null)
            {
                #region Colors

                EditorGUILayout.Space();

                spriteTransitionTab = (CategoryBtn.SpriteTransition)EditorGUILayout.EnumPopup(new GUIContent("Sprite Transition Type", "The type of category button transition settings"), spriteTransitionTab);
                if (EditorPrefs.GetInt(Constants.SPRITE_TRANSITION_TAB) != (int)spriteTransitionTab)
                    EditorPrefs.SetInt(Constants.SPRITE_TRANSITION_TAB, (int)spriteTransitionTab);
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                if (spriteTransitionTab == CategoryBtn.SpriteTransition.ColorTint)
                {
                    Database.Theme.ThemeMap.Find(i => i.MapType == MapType.CategoryButtonNormal).SpriteColor = Utilities.EditorGUILayoutField("Category Button Normal Color", Database.Theme.ThemeMap.Find(i => i.MapType == MapType.CategoryButtonNormal).SpriteColor);

                    Database.Theme.ThemeMap.Find(i => i.MapType == MapType.CategoryButtonHighlighted).SpriteColor = Utilities.EditorGUILayoutField("Category Button Highlighted Color", Database.Theme.ThemeMap.Find(i => i.MapType == MapType.CategoryButtonHighlighted).SpriteColor);

                    Database.Theme.ThemeMap.Find(i => i.MapType == MapType.CategoryButtonSelected).SpriteColor = Utilities.EditorGUILayoutField("Category Button Selected Color", Database.Theme.ThemeMap.Find(i => i.MapType == MapType.CategoryButtonSelected).SpriteColor);
                }
                else if (spriteTransitionTab == CategoryBtn.SpriteTransition.SpriteSwap)
                {
                    Database.Theme.ThemeMap.Find(i => i.MapType == MapType.CategoryButtonNormal).Sprite = Utilities.EditorGUILayoutField("Category Button Normal Sprite", Database.Theme.ThemeMap.Find(i => i.MapType == MapType.CategoryButtonNormal).Sprite);
                    Database.Theme.ThemeMap.Find(i => i.MapType == MapType.CategoryButtonHighlighted).Sprite = Utilities.EditorGUILayoutField("Category Button Highlighted Sprite", Database.Theme.ThemeMap.Find(i => i.MapType == MapType.CategoryButtonHighlighted).Sprite);
                    Database.Theme.ThemeMap.Find(i => i.MapType == MapType.CategoryButtonSelected).Sprite = Utilities.EditorGUILayoutField("Category Button Selected Sprite", Database.Theme.ThemeMap.Find(i => i.MapType == MapType.CategoryButtonSelected).Sprite);
                }
                EditorGUILayout.Space();

                Database.Theme.CategoryButtonTextColor = Utilities.EditorGUILayoutField("Category Button Text Color", Database.Theme.CategoryButtonTextColor);

                #endregion Colors

                #region Collapse Buttons

                if (so != null)
                {
                    #region Category Elements

                    GUILayout.BeginHorizontal();

                    if (GUILayout.Button(new GUIContent("Open Categories")))
                    {
                        for (int i = 0; i < categoriesList.serializedProperty.arraySize; i++)
                        {
                            categoriesList.serializedProperty.GetArrayElementAtIndex(i).isExpanded = true;
                        }
                    }

                    if (GUILayout.Button(new GUIContent("Collapse Categories")))
                    {
                        for (int i = 0; i < categoriesList.serializedProperty.arraySize; i++)
                        {
                            categoriesList.serializedProperty.GetArrayElementAtIndex(i).isExpanded = false;
                        }
                    }
                    GUILayout.EndHorizontal();

                    #endregion Category Elements

                    #region Sub Category Elements

                    GUILayout.BeginHorizontal();

                    if (GUILayout.Button(new GUIContent("Open Sub Categories")))
                    {
                        for (int i = 0; i < categoriesList.serializedProperty.arraySize; i++)
                        {
                            for (int c = 0; c < categoriesList.serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("subCategories").arraySize; c++)
                            {
                                categoriesList.serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("subCategories").GetArrayElementAtIndex(c).isExpanded = true;
                            }
                        }
                    }

                    if (GUILayout.Button(new GUIContent("Collapse Sub Categories")))
                    {
                        for (int i = 0; i < categoriesList.serializedProperty.arraySize; i++)
                        {
                            for (int c = 0; c < categoriesList.serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("subCategories").arraySize; c++)
                            {
                                categoriesList.serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("subCategories").GetArrayElementAtIndex(c).isExpanded = false;
                            }
                        }
                    }
                    GUILayout.EndHorizontal();

                    #endregion Sub Category Elements
                }

                #endregion Collapse Buttons

                if (categoriesList != null)
                {
                    so.Update();

                    GUILayout.BeginVertical();

                    categoriesScrollPos = EditorGUILayout.BeginScrollView(categoriesScrollPos, GUILayout.ExpandHeight(true));
                    categoriesList.DoLayoutList();
                    EditorGUILayout.EndScrollView();

                    GUILayout.EndVertical();
                    so.ApplyModifiedProperties();
                }
            }

            #endregion Draw Categories List
        }

        private ReorderableList GetOrCreateSubCategoriesList(int index, SerializedProperty subCategoriesProp)
        {
            if (!subCategoriesLists.TryGetValue(index, out ReorderableList subCategoriesList))
            {
                subCategoriesList = new ReorderableList(subCategoriesProp.serializedObject, subCategoriesProp, true, true, true, true)
                {
                    drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Sub Categories"),

                    drawElementCallback = (Rect rect, int i, bool isActive, bool isFocused) =>
                    {
                        SerializedProperty subCategoryElement = subCategoriesProp.GetArrayElementAtIndex(i);
                        // Get the height based on whether the element is expanded or not

                        subCategoryElement.isExpanded = Utilities.EditorGUIFoldout(ref rect, subCategoryElement, ((CategoryList)subCategoryElement.FindPropertyRelative("category").enumValueIndex).ToString(), subCategoryElement.isExpanded);

                        if (subCategoryElement.isExpanded)
                        {
                            Utilities.SearchableEnumPopup(ref rect, subCategoryElement.FindPropertyRelative("categoryType"), "Category Type", true);

                            Utilities.SearchableEnumPopup(ref rect, subCategoryElement.FindPropertyRelative("category"), "Sub Category");
                            Utilities.EditorGUIFloatSlider(ref rect, subCategoryElement.FindPropertyRelative("incompleteAlpha"), 0, 1f, "Incomplete Alpha");
                            Utilities.EditorGUIFloatSlider(ref rect, subCategoryElement.FindPropertyRelative("completeAlpha"), 0, 1f, "Complete Alpha");
                            Utilities.EditorGUIField(ref rect, subCategoryElement.FindPropertyRelative("color"), "Achievement Complete Overlay Color");
                            Utilities.EditorGUIField(ref rect, subCategoryElement.FindPropertyRelative("image"), "Achievement Complete Overlay");
                        }
                        else
                        {
                            rect.height = (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing); // This ensures the rect has the correct height for collapsed items.
                        }
                    },
                    // Define the elementHeightCallback here
                    elementHeightCallback = i =>
                    {
                        SerializedProperty element = subCategoriesProp.GetArrayElementAtIndex(i);
                        bool isElementExpanded = element.isExpanded;
                        // Calculate the height based on whether the element is expanded or not
                        return isElementExpanded ?
                            (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 7 :
                            (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
                    }

                };

                subCategoriesLists[index] = subCategoriesList;
            }

            return subCategoriesList;
        }

        private void DisplayDatabaseGUI()
        {
            #region Create/Load Buttons

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Create Database"))
            {
                if (databaseName == string.Empty)
                {
                    EditorGUILayout.HelpBox("DatabaseName is empty!", MessageType.Error);
                    CreatingDB = true;
                    return;
                }

                if (DatabaseExists)
                {
                    if (EditorUtility.DisplayDialog("Advanced Achievement System", "Database already Exists, Would you like to override it?", "Ok", "Cancel"))
                        CreateDatabase();
                }
                else
                    CreateDatabase();
            }

            if (GUILayout.Button("Load Database"))
            {
                SelectDatabase();
            }

            GUILayout.EndHorizontal();

            Database = Utilities.EditorGUILayoutField("Loaded Database:", Database, readOnly: true);

            #region dbName Error checking

            if (databaseName == string.Empty && CreatingDB)
                EditorGUILayout.HelpBox("Database Name is not filled out!", MessageType.Error);
            else if (CreatingDB)
                CreatingDB = false;

            databaseName = Utilities.EditorGUILayoutField("Database Name:", databaseName);

            #endregion dbName Error checking

            if (so != null)
                Database.Theme = Utilities.EditorGUILayoutField("Theme", Database.Theme);

            if (GUILayout.Button("Apply Theme"))
            {
                string themeName = "";
                if (Database != null)
                    if (Database.Theme != null)
                        themeName = Database.Theme.name;

                bool dialogue = EditorUtility.DisplayDialog("Apply theme to current scene instance",
                                $"Would you like to override the current theme for this Database: ({databaseName}) with the selected theme template: ({themeName})? \n\n This will override the prefabs assigned in the database, it is recommended to create a new Theme Template prior to this action as it cannot be undone",
                                "Yes",
                                "No");

                if (dialogue)
                    ApplyTheme();
            }

            GUILayout.Label("New Theme Pack Directory", EditorStyles.boldLabel);
            newDirectoryName = Utilities.EditorGUILayoutField("Directory Name:", newDirectoryName);

            if (GUILayout.Button("Generate Theme Pack From Template"))
            {
                bool dialogue = EditorUtility.DisplayDialog("Create a new Theme Pack",
                               $"WARNING! This action will delete the current Achievement System Database in your scene and create the newly generated instance from {newDirectoryName} and override the Achievement and category prefabs in the active database, this changes the current theme pack",
                               "Yes",
                               "No");

                if (dialogue == true)
                    if (CreateDirectory(newDirectoryName))
                        GenerateThemePackFromThemeTemplate(newDirectoryName);
            }

            #endregion Create/Load Buttons
        }

        private void DisplayInstallationGUI()
        {
            if (achievementManagerSceneInstance == null)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                //themePackLocation = Application.dataPath + dbLocation + databaseName + ".Database";

                if (GUILayout.Button("Load Theme Pack"))
                    LoadThemePack();

                EditorGUILayout.LabelField(new GUIContent("Location: " + themePackLocation, ""));

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                if (GUILayout.Button("Install Advanced Achievement System", GUILayout.Height(50f)))
                {
                    eventSystem = FindFirstObjectByType<EventSystem>();

                    if (eventSystem == null)
                    {
                        var es = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/RealSoftGames/AdvancedAchievementSystem/ThemePacks/EventSystem.prefab");
                        eventSystem = PrefabUtility.InstantiatePrefab(es) as EventSystem;
                    }

                    if (achievementManagerSceneInstance == null)
                    {
                        // Display the dialog
                        bool result = EditorUtility.DisplayDialog(
                            "Select or Create a Dataabse", // The title of the dialog window
                            "Select or create a database to use with the installation", "Select Database", "Create Database");

                        if (result)
                        {
                            SelectDatabase();
                            Database.AchievementSystemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(themePackLocation + "/" + Constants.ACHIEVEMENT_SYSTEM_PREFAB + ".prefab");
                            Database.AchievementPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(themePackLocation + "/" + Constants.ACHIEVEMENT_PREFAB + ".prefab");
                            Database.CategoriesBtnPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(themePackLocation + "/" + Constants.CATEGORY_BUTTON_PREFAB + ".prefab");
                            OnEnable();
                        }
                        else
                        {
                            var inputConfigs = new List<InputPromptWindow.InputConfig>
                                {
                                          new InputPromptWindow.InputConfig
                                          {
                                              label = "Database Name:",
                                              type = InputPromptWindow.InputType.TextField,
                                              defaultValue = "DefaultDatabase"
                                          },
                                };

                            var buttonConfigs = new List<InputPromptWindow.ButtonConfig>
                                {
                                    new InputPromptWindow.ButtonConfig
                                    {
                                        label = "Create",
                                        action = (List<object> inputs) =>
                                        {
                                            Debug.Log($"Database Name: {inputs[0]}");
                                            databaseName = inputs[0].ToString();
                                            CreateDatabase();

                                            Database.AchievementSystemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(themePackLocation + "/" + Constants.ACHIEVEMENT_SYSTEM_PREFAB + ".prefab");
                                            Database.AchievementPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(themePackLocation + "/" + Constants.ACHIEVEMENT_PREFAB + ".prefab");
                                            Database.CategoriesBtnPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(themePackLocation + "/" + Constants.CATEGORY_BUTTON_PREFAB + ".prefab");
                                            so.ApplyModifiedProperties();

                                            OnEnable();
                                            settings = Settings.Database;
                                        },
                                        shouldCloseCondition = (List<object> inputs) =>
                                        {
                                            return true;
                                        }
                                    }
                                };

                            InputPromptWindow.ShowWindow(
                                "Create New Database",
                                "Please provide the database name",
                                inputConfigs,
                                buttonConfigs);
                        }
                    }
                }
            }
        }

        private void InstantiateAchievementSystemPrefab()
        {
            GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(themePackLocation + "/AchievementSystem.prefab");
            if (asset == null)
            {
                EditorUtility.DisplayDialog("Advanced Achievement System", $"Theme Pack Location Required. \nCurrent location is set to \n\n{themePackLocation}", "OK");
                return;
            }

            GameObject go = PrefabUtility.InstantiatePrefab(asset) as GameObject;
            go.name = Constants.ACHIEVEMENT_SYSTEM_NAME;
            achievementManagerSceneInstance = go.GetComponent<AchievementManager>();
        }

        #region Database

        private string GetDatabaseAssetPath(string databaseName)
        {
            return $"Assets/{dbLocation}{databaseName}.asset";
        }

        private bool LoadDatabase(string databaseName = "")
        {
            bool initialized = false;

            if (string.IsNullOrEmpty(databaseName))
                databaseName = EditorPrefs.GetString(Constants.EDITORPREFS_KEY_DATABASE_NAME);

            string path = GetDatabaseAssetPath(databaseName);
            if (path == string.Empty)
            {
                Debug.Log("No Database Selected!");
                return false;
            }

            Database = AssetDatabase.LoadAssetAtPath<AdvancedAchievementSystem>(path);
            if (Database == null)
            {
                var buttonConfigs = new List<InputPromptWindow.ButtonConfig>
                {
                    new InputPromptWindow.ButtonConfig
                    {
                        label = "Select Database",
                        action = (List<object> inputs) =>
                        {
                            SelectDB();
                        },
                        shouldCloseCondition = (List<object> inputs) =>
                        {
                              return true;
                        }
                    },
                    new InputPromptWindow.ButtonConfig
                    {
                        label = "Create New Database",
                        action = (List<object> inputs) =>
                        {
                            CreateDB();
                        },
                        shouldCloseCondition = (List<object> inputs) =>
                        {
                            return true;
                        }
                    }
                };

                void SelectDB()
                {
                    InputPromptWindow.ShowWindow(
                       "Select a Database",
                       "Please provide the database for Advanced Achievement System",
                       new List<InputPromptWindow.InputConfig>
                       {
                              new InputPromptWindow.InputConfig
                              {
                                  label = "Select a Database:",
                                  type = InputPromptWindow.InputType.ObjectField,
                                  defaultValue = null,
                                  ObjectType = typeof(AdvancedAchievementSystem)
                              },
                       },
                       new List<InputPromptWindow.ButtonConfig>
                       {
                        new InputPromptWindow.ButtonConfig
                        {
                            label = "Confirm",
                            action = (List<object> inputs) =>
                            {
                                // The first input is expected to be the selected DB object
                                AdvancedAchievementSystem selectedDB = inputs[0] as AdvancedAchievementSystem;
                                if (selectedDB != null)
                                {
                                    Debug.Log($"Selected Database: {selectedDB.name}");
                                    Database = selectedDB;
                                    SelectDatabase();
                                    Initialize();
                                }
                                else
                                    Debug.LogError("No Database was selected or the selected object is not a Database.");
                            },
                            shouldCloseCondition = (List<object> inputs) =>
                            {
                                return inputs[0] != null;
                            }
                        },
                            new InputPromptWindow.ButtonConfig
                            {
                                label = "Cancel",
                                action = (List<object> inputs) =>
                                {
                                    Debug.Log("Database selection canceled.");
                                },
                                shouldCloseCondition = (List<object> inputs) =>
                                {
                                    return true;
                                }
                        }
                       });
                }
                void CreateDB()
                {
                    InputPromptWindow.ShowWindow(
                     "No Database Selected",
                     "Please Create a Database",
                     new List<InputPromptWindow.InputConfig>
                     {
                     new InputPromptWindow.InputConfig
                     {
                         label = "Database Name:",
                         type = InputPromptWindow.InputType.TextField,
                         defaultValue = "",
                     }
                     },
                     new List<InputPromptWindow.ButtonConfig>
                     {
                     new InputPromptWindow.ButtonConfig
                     {
                         label = "Confirm",
                         action = (List<object> inputs) =>
                         {
                             string dbName = inputs[0] as string;

                             if (!string.IsNullOrEmpty(dbName))
                             {
                                 Debug.Log($"Creating {dbName} Database");
                                 CreateDatabase(dbName);
                                 Initialize();
                             }
                             else
                                 Debug.LogError("Database name cannot be empty");
                         },
                         shouldCloseCondition = (List<object> inputs) =>
                         {
                             return inputs[0] != null;
                         }
                     },
                         new InputPromptWindow.ButtonConfig
                         {
                             label = "Cancel",
                             action = (List<object> inputs) =>
                             {
                                 Debug.Log("Database selection canceled.");
                             },
                             shouldCloseCondition = (List<object> inputs) =>
                             {
                                 return true;
                             }
                     } //Cancel Button
                     });
                }

                return false;
            }

            bool Initialize()
            {
                if (Database.Theme == null)
                {
                    if (TryGetThemeAtPath(themePackLocation, out var theme))
                        Database.Theme = theme;
                    else
                        ShowThemeSelectionPrompt(() =>
                        {
                            achievementManagerSceneInstance.DatabaseToLoad = Database;
                            if (achievementManagerSceneInstance == null)
                                achievementManagerSceneInstance = FindFirstObjectByType<AchievementManager>();
                            if (achievementManagerSceneInstance != null)
                                Utilities.ApplySelectedPrefabChanges(achievementManagerSceneInstance.gameObject);
                            else
                                Debug.LogError($"No AchievementManager in scene");
                        });
                }

                if (achievementManagerSceneInstance == null)
                    achievementManagerSceneInstance = FindFirstObjectByType<AchievementManager>();
                if (achievementManagerSceneInstance != null)
                    Utilities.ApplySelectedPrefabChanges(achievementManagerSceneInstance.gameObject);
                else
                    InstantiateAchievementSystemPrefab();

                achievementManagerSceneInstance.DatabaseToLoad = Database;

                if (Database != null)
                {
                    InitializeSerializedObjects();
                    InitializeTheme();
                }
                else
                {
                    Debug.LogError($"Database at {path} does not exist!");
                    return false;
                }
                return true;
            }
            initialized = Initialize();

            return true;
        }

        private bool SelectDatabase()
        {
            string path = EditorUtility.OpenFilePanel("Advanced Achievement System Database", Application.dataPath, "asset");

            if (string.IsNullOrEmpty(path))
            {
                Debug.Log("No Database selected!");
                return false;
            }

            // Convert absolute path to relative
            string relativePath = ConvertAbsoluteToRelativePath(path);
            databaseName = GetObjectNameFromRelativePath(relativePath);
            EditorPrefs.SetString(Constants.EDITORPREFS_KEY_DATABASE_NAME, databaseName);
            return LoadDatabase(GetObjectNameFromRelativePath(relativePath));
        }

        private string ConvertAbsoluteToRelativePath(string absolutePath)
        {
            // Convert absolute path to relative
            return absolutePath.Replace(Application.dataPath, "Assets");
        }

        private string GetObjectNameFromRelativePath(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        private string GetObjectNameFromFilePanel(string path)
        {
            // Convert absolute path to relative
            string relativePath = ConvertAbsoluteToRelativePath(path);
            return Path.GetFileNameWithoutExtension(relativePath);
        }

        private void CreateDatabase(string dbName = "")
        {
            if (!string.IsNullOrEmpty(dbName))
                databaseName = dbName;

            Database = ScriptableObject.CreateInstance<AdvancedAchievementSystem>();
            //Debug.Log("Trying to create Database: " + Application.dataPath + dbLocation.Replace("\\Assets", "") + dbName + ".Database");
            //AssetDatabase.CreateAsset(Database, "Assets\\" + dbLocation + dbName + ".Database");
            AssetDatabase.CreateAsset(Database, "Assets/" + dbLocation + databaseName + ".asset");
            AssetDatabase.SaveAssets();

            EditorPrefs.SetString(Constants.EDITORPREFS_KEY_DATABASE_NAME, databaseName);
            LoadDatabase(dbName);
        }

        #endregion Database

        private void LoadThemePack()
        {
            if (File.Exists(themePackLocation))
                themePackLocation = EditorUtility.OpenFolderPanel("Theme Pack Location", themePackLocation, "");
            else
                themePackLocation = EditorUtility.OpenFolderPanel("Theme Pack Location", Application.dataPath, "");

            themePackLocation = "Assets" + themePackLocation.Replace(Application.dataPath, "");

            GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(themePackLocation + "/AchievementSystem.prefab");

            if (asset == null)
            {
                Debug.Log("No theme pack selceted!");
                return;
            }
            else
            {
                Debug.Log($"Saving Them pack location: {themePackLocation} ");
                EditorPrefs.SetString(Constants.THEME_PACK_LOCATION, themePackLocation);
            }
        }

        private void GenerateThemePackFromThemeTemplate(string newDirName)
        {
            if (Database == null)
            {
                Debug.Log("Database does not have an assigned Theme");
                return;
            }

            string destinationPath = Path.Combine($"{Constants.THEME_PACKS_ROOT_PATH}", newDirName);
            string sourceDirectory = Path.Combine(Constants.THEME_PACKS_ROOT_PATH, "Template");
            string[] prefabs =
                    {
                      Constants.ACHIEVEMENT_SYSTEM_PREFAB,
                      Constants.CATEGORY_BUTTON_PREFAB,
                      Constants.ACHIEVEMENT_PREFAB
                    };

            //First Check that the Template Files Exist
            foreach (var item in prefabs)
            {
                string fullPathToPrefab = Path.Combine(sourceDirectory, $"{item}.prefab");

                if (!File.Exists(fullPathToPrefab))
                {
                    Debug.LogError("The source prefab does not exist at the specified path: " + fullPathToPrefab);
                    return;
                }
            }

            //Check if the theme template exists
            string fullPathToAsset = Path.Combine(sourceDirectory, $"{Constants.THEME_TEMPLATE}.asset");
            if (!File.Exists(fullPathToAsset))
            {
                Debug.LogError("The source Database does not exist at the specified path: " + fullPathToAsset);
                return;
            }

            //Duplicate the prefabs
            foreach (var item in prefabs)
            {
                string fullPathToPrefab = Path.Combine(sourceDirectory, $"{item}.prefab");
                string destinationFullPath = Path.Combine(destinationPath, $"{item}.prefab");
                // Perform the copy
                if (AssetDatabase.CopyAsset(fullPathToPrefab, destinationFullPath))
                {
                    Debug.Log("Prefab copied successfully from " + fullPathToPrefab + " to " + destinationFullPath);
                }
                else
                {
                    Debug.LogError("Failed to copy prefab. It may already exist at the destination.");
                }
            }

            //Duplicate the theme template
            string PathToAsset = Path.Combine(sourceDirectory, $"{Constants.THEME_TEMPLATE}.asset");
            string destinationPathAsset = Path.Combine(destinationPath, $"{newDirName}_Theme.asset");
            if (AssetDatabase.CopyAsset(PathToAsset, destinationPathAsset))
            {
                Debug.Log("Theme copied successfully from " + PathToAsset + " to " + destinationPathAsset);
            }
            else
            {
                Debug.LogError("Failed to copy prefab. It may already exist at the destination.");
            }

            // Refresh the AssetDatabase to show the new prefab in the Unity Editor
            AssetDatabase.Refresh();
            //Destroy the current achievement system instance in the scene
            DestroyImmediate(GameObject.FindFirstObjectByType<AchievementManager>().gameObject);

            GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(destinationPath + "/AchievementSystem.prefab");
            if (asset == null)
            {
                EditorUtility.DisplayDialog("Advanced Achievement System", $"Theme Pack Location Error.", "OK");
                GUIUtility.ExitGUI();
                return;
            }

            //Create an instance of the new achievement system
            GameObject go = PrefabUtility.InstantiatePrefab(asset) as GameObject;

            go.name = Constants.ACHIEVEMENT_SYSTEM_NAME;
            OnEnable();

            Database.Theme = AssetDatabase.LoadAssetAtPath<Theme>(destinationPathAsset);
            string achievementPrefabPath = Path.Combine(destinationPath, $"{Constants.ACHIEVEMENT_PREFAB}.prefab").Replace("\\", "/");
            string categoryBtnPrefabPath = Path.Combine(destinationPath, $"{Constants.CATEGORY_BUTTON_PREFAB}.prefab").Replace("\\", "/");

            Database.AchievementPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(achievementPrefabPath);
            Database.CategoriesBtnPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(categoryBtnPrefabPath);
            Database.AchievementSystemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(destinationPath + "/AchievementSystem.prefab");
            Database.Theme.AchievementSystemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(destinationPath + "/AchievementSystem.prefab");
            Database.Theme.AchievementPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(achievementPrefabPath);
            Database.Theme.CategoryPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(categoryBtnPrefabPath);

            if (Database.AchievementPrefab == null)
            {
                Debug.LogError("Failed to load AchievementPrefab at path: " + achievementPrefabPath);
            }

            if (Database.CategoriesBtnPrefab == null)
            {
                Debug.LogError("Failed to load CategoriesBtnPrefab at path: " + categoryBtnPrefabPath);
            }
        }

        private void ApplyTheme()
        {
            if (achievementManagerSceneInstance == null)
            {
                EditorUtility.DisplayDialog("No active Achievement System!",
                               $"No active Achievement System! in scene, install one before continuing",
                               "OK");
                return;
            }
            if (!DatabaseExists)
            {
                EditorUtility.DisplayDialog("No Database Loaded!",
                         $"No Database Loaded, Load one before continuing",
                         "OK");
                return;
            }

            if (Database.Theme == null)
            {
                EditorApplication.delayCall += () =>
                {
                    var inputConfigs = new List<InputPromptWindow.InputConfig>
                    {
                              new InputPromptWindow.InputConfig
                              {
                                  label = "Select a Theme to Load",
                                  type = InputPromptWindow.InputType.ObjectField,
                                  defaultValue = null,
                                  ObjectType = typeof(Theme)
                              },
                    };
                    var buttonConfigs = new List<InputPromptWindow.ButtonConfig>
                    {
                        new InputPromptWindow.ButtonConfig
                        {
                            label = "Confirm",
                            action = (List<object> inputs) =>
                            {
                                Theme selectedTheme = inputs[0] as Theme;
                                if (selectedTheme != null)
                                {
                                    Debug.Log($"Selected Theme: {selectedTheme.name}");
                                    Database.Theme = selectedTheme;
                                }
                                else
                                    Debug.LogError("No Theme was selected or the selected object is not a Theme.");

                                if (Database.Theme != null)
                                    SetTheme();
                            },
                            shouldCloseCondition = inputs => { return true; }
                        }
                    };

                    InputPromptWindow.ShowWindow(
                        "Select a Theme Template",
                        "Please provide the Theme Template to prepopulate the achievement system",
                        inputConfigs,
                        buttonConfigs);
                };
            }
            else
                SetTheme();
        }

        private List<Map> FindReferenceMaps(MapType mapType)
        {
            List<Map> combinedMaps = new List<Map>();
            combinedMaps.AddRange(Database.AchievementSystemPrefab.GetComponent<ReferenceMap>().Maps);
            combinedMaps.AddRange(achievementManagerSceneInstance.GetComponent<ReferenceMap>().Maps);
            combinedMaps.AddRange(Database.AchievementPrefab.GetComponent<ReferenceMap>().Maps);
            combinedMaps.AddRange(Database.CategoriesBtnPrefab.GetComponent<ReferenceMap>().Maps);

            return combinedMaps.FindAll(i => i.MapType == mapType);
        }

        private bool SetTheme()
        {
            try
            {
                //Clear all categories
                ToggleGroup toggleGroup = achievementManagerSceneInstance.GetComponent<ReferenceMap>().GetComponentFromReferenceMapComponent<ToggleGroup>(MapType.AchievementSystemCategoriesContent);
                for (int i = achievementSystemCategoriesContent.transform.childCount - 1; i >= 0; i--)
                {
                    toggleGroup.UnregisterToggle(achievementSystemCategoriesContent.transform.GetChild(i).gameObject.GetComponent<UnityEngine.UI.Toggle>());
                    DestroyImmediate(achievementSystemCategoriesContent.transform.GetChild(i).gameObject);
                }

                //Clear all achievements
                for (int i = achievementSystemAchievementContent.transform.childCount - 1; i >= 0; i--)
                    DestroyImmediate(achievementSystemAchievementContent.transform.GetChild(i).gameObject);
                achievementManagerSceneInstance.Achievements.Clear();

                foreach (var item in Database.Theme.ThemeMap)
                {
                    var references = FindReferenceMaps(item.MapType);

                    if (references != null && references.Count > 0)
                    {
                        foreach (var reference in references)
                        {
                            if (reference != null)
                            {
                                var target = reference.GetValue<Image>();
                                if (target != null)
                                {
                                    // If we find a match from the reference map, apply the sprite and color properties
                                    target.sprite = item.Sprite;
                                    target.color = item.SpriteColor;
                                    EditorUtility.SetDirty(target);
                                }
                                else
                                    Debug.LogError("target is null!");
                            }
                            else
                                Debug.LogError("Reference is null!");
                        }
                    }
                    else
                    {
                        Debug.LogError($"{item.MapType} Not Found on ReferenceMap!");
                    }
                }

                // Apply and save changes to ScriptableObject
                EditorUtility.SetDirty(Database.Theme);
                EditorUtility.SetDirty(Database);

                ApplyPrefabChanges(Database.AchievementSystemPrefab);
                ApplyPrefabChanges(Database.AchievementPrefab);
                ApplyPrefabChanges(Database.CategoriesBtnPrefab);

                // Apply and save changes to the scene instance
                if (achievementManagerSceneInstance != null)
                    EditorUtility.SetDirty(achievementManagerSceneInstance);

                AssetDatabase.SaveAssets();

                GenerateCategories();
                GenerateAchievements();
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error in SetTheme: {ex.Message}\n{ex.StackTrace}");
                return false;
            }
        }

        private void ApplyPrefabChanges(GameObject prefab)
        {
            try
            {
                var prefabRoot = PrefabUtility.GetOutermostPrefabInstanceRoot(prefab);
                if (prefabRoot != null)
                {
                    // Apply changes to the prefab
                    PrefabUtility.ApplyPrefabInstance(prefabRoot, InteractionMode.UserAction);

                    // Mark the prefab as dirty
                    EditorUtility.SetDirty(prefab);
                    Debug.Log($"Applied changes to prefab: {prefab.name}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error applying prefab changes to {prefab.name}: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private static bool CreateDirectory(string directoryName)
        {
            string fullPath = Path.Combine(Constants.THEME_PACKS_ROOT_PATH, directoryName);

            // Check if the directory exists
            if (AssetDatabase.IsValidFolder(fullPath))
            {
                bool replace = EditorUtility.DisplayDialog("Directory Exists",
                    "The directory already exists. Would you like to replace it?",
                    "Yes", "No");

                if (replace)
                {
                    AssetDatabase.DeleteAsset(fullPath);
                    AssetDatabase.Refresh();
                }
                else
                {
                    // If user chooses not to replace, cancel the operation
                    return false;
                }
            }

            AssetDatabase.CreateFolder(Constants.THEME_PACKS_ROOT_PATH, directoryName);
            AssetDatabase.Refresh();

            Debug.Log($"Directory '{fullPath}' created successfully.");
            return true;
        }

        private void GenerateCategories()
        {
            if (Database.Categories.Count == 0)
            {
                EditorUtility.DisplayDialog("Database Categories",
                           "Database does not contain any categories",
                           "OK");
                return;
            }

            #region Error Checking for All Category

            bool foundAllCategory = false;
            bool foundInvalidSubCategory = false;
            string invalidCategoryName = string.Empty;
            bool showError = EditorPrefs.GetBool("ShowCategoryError", true);

            foreach (var category in Database.Categories)
            {
                if (category._Category == CategoryList.All)
                {
                    foundAllCategory = true;
                    if (category.subCategories.Count != 1 || category.subCategories[0].Category != CategoryList.All)
                    {
                        if (showError)
                        {
                            bool dontShowAgain = EditorUtility.DisplayDialogComplex("Misconfiguration in 'All' Category",
                                "The 'All' category should only contain one subcategory, which should also be 'All'.",
                                "OK",
                                "Don't Show This Message Again",
                                "") == 1;

                            if (dontShowAgain)
                            {
                                EditorPrefs.SetBool("ShowCategoryError", false);
                            }
                        }
                        return;
                    }
                }

                if (category._Category != CategoryList.All && category.subCategories.Any(i => i.Category == CategoryList.All))
                {
                    foundInvalidSubCategory = true;
                    invalidCategoryName = category._Category.ToString();
                }
            }

            if (!foundAllCategory)
            {
                if (showError)
                {
                    bool dontShowAgain = EditorUtility.DisplayDialogComplex("Database Categories is missing All Category",
                        "Database does not contain an 'All' category. " +
                        "Without an 'All' category, the category selection will only filter by the selected categories, " +
                        "and by default, the first category will be selected. This means you will not be able to see " +
                        "all available achievements at once. Please add an 'All' category to ensure all achievements " +
                        "can be viewed together.",
                        "OK",
                        "Don't Show This Message Again",
                        "") == 1;

                    if (dontShowAgain)
                    {
                        EditorPrefs.SetBool("ShowCategoryError", false);
                    }
                }
            }
            else if (foundInvalidSubCategory)
            {
                if (showError)
                {
                    bool dontShowAgain = EditorUtility.DisplayDialogComplex("Invalid Subcategory Configuration",
                        $"The category '{invalidCategoryName}' contains 'All' as a subcategory along with other subcategories. " +
                        "The 'All' subcategory is a special case that does not apply a filter unless used in conjunction with the Search input field. " +
                        "Please adjust the subcategories accordingly.",
                        "OK",
                        "Don't Show This Message Again",
                        "") == 1;

                    if (dontShowAgain)
                    {
                        EditorPrefs.SetBool("ShowCategoryError", false);
                    }
                }
            }

            #endregion Error Checking for All Category

            ChangeAchievmentSystemEnabledState(true);

            ToggleGroup toggleGroup = achievementManagerSceneInstance.GetComponent<ReferenceMap>().GetComponentFromReferenceMapComponent<ToggleGroup>(MapType.AchievementSystemCategoriesContent);
            for (int i = achievementSystemCategoriesContent.transform.childCount - 1; i >= 0; i--)
            {
                toggleGroup.UnregisterToggle(achievementSystemCategoriesContent.transform.GetChild(i).gameObject.GetComponent<UnityEngine.UI.Toggle>());
                DestroyImmediate(achievementSystemCategoriesContent.transform.GetChild(i).gameObject);
            }

            achievementManagerSceneInstance.Categories.Clear();

            if (Database.CategoriesBtnPrefab != null)
            {
                GameObject all = null; //Change this to first Category

                for (int i = 0; i < Database.Categories.Count; i++)
                {
                    GameObject go = Instantiate(Database.Theme.CategoryPrefab, achievementSystemCategoriesContent.transform);
                    var reference = go.GetComponent<ReferenceMap>();
                    var btn = go.GetComponent<CategoryBtn>();
                    achievementManagerSceneInstance.Categories.Add(btn);
                    go.name = Database.Categories[i].buttonName;

                    reference.GetType<TMP_Text>(MapType.CategoryText).text = Database.Categories[i].buttonName;
                    reference.GetType<TMP_Text>(MapType.CategoryText).color = Database.Theme.CategoryButtonTextColor;
                    go.GetComponent<UnityEngine.UI.Toggle>().group = toggleGroup;

                    for (int c = 0; c < Database.Categories[i].subCategories.Count; c++)
                        btn.Categories.Add(Database.Categories[i].subCategories[c].Category);

                    btn.AchievementContent = achievementSystemAchievementContent;
                    btn.CategoriesContent = achievementSystemCategoriesContent;
                    btn.SearchField = achievementManagerSceneInstance.Map.GetComponentFromReferenceMapComponent<TMP_InputField>(MapType.AchievementSearchFieldInputField);
                    btn.Transition = spriteTransitionTab;

                    btn.Normal = Database.Theme.ThemeMap.Find(i => i.MapType == MapType.CategoryButtonNormal).SpriteColor;
                    btn.Highlighted = Database.Theme.ThemeMap.Find(i => i.MapType == MapType.CategoryButtonHighlighted).SpriteColor;
                    btn.Pressed = Database.Theme.ThemeMap.Find(i => i.MapType == MapType.CategoryButtonSelected).SpriteColor;
                    btn.NormalSpeite = Database.Theme.ThemeMap.Find(i => i.MapType == MapType.CategoryButtonNormal).Sprite;
                    btn.HighlightedSprite = Database.Theme.ThemeMap.Find(i => i.MapType == MapType.CategoryButtonHighlighted).Sprite;
                    btn.SelectedSprite = Database.Theme.ThemeMap.Find(i => i.MapType == MapType.CategoryButtonSelected).Sprite;

                    if (spriteTransitionTab == CategoryBtn.SpriteTransition.ColorTint)
                        btn.SetNormal();

                    toggleGroup.RegisterToggle(btn.GetComponent<UnityEngine.UI.Toggle>());
                    //if (i == 0 && go.GetComponent<CategoryBtn>().Categories[0] == CategoryList.All)
                    if (i == 0)
                        all = go;
                }

                toggleGroup.RegisterToggle(all.GetComponent<UnityEngine.UI.Toggle>());
                all.GetComponent<CategoryBtn>().Toggle = all.GetComponent<UnityEngine.UI.Toggle>();
                all.GetComponent<CategoryBtn>().SetupPressedState();
                Utilities.ApplySelectedPrefabChanges(achievementManagerSceneInstance.gameObject);
            }
            else
                Debug.LogError("Categories Button Prefab not filled out!");
        }

        public void GenerateAchievements()
        {
            if (Database.Categories.Count == 0)
            {
                EditorUtility.DisplayDialog("Database Achievements",
                           "Database does not contain any Achievements",
                           "OK");
                return;
            }

            ChangeAchievmentSystemEnabledState(true);
            if (so == null)
            {
                Debug.LogError("SO is NULL!");
                return;
            }

            for (int i = achievementSystemAchievementContent.transform.childCount - 1; i >= 0; i--)
                DestroyImmediate(achievementSystemAchievementContent.transform.GetChild(i).gameObject);
            achievementManagerSceneInstance.Achievements.Clear();

            if (Database.AchievementPrefab != null)
            {
                for (int i = 0; i < so.FindProperty("achievements").arraySize; i++)
                {
                    GameObject go = Instantiate(Database.Theme.AchievementPrefab, achievementSystemAchievementContent.transform);
                    var reference = go.GetComponent<ReferenceMap>();
                    var ach = go.GetComponent<Achievement>();

                    CategoryList category = (CategoryList)so.FindProperty("achievements").GetArrayElementAtIndex(i).FindPropertyRelative("category").enumValueIndex;

                    go.name = category.ToString() +
                        " - " +
                        FindReplace(so.FindProperty("achievements").GetArrayElementAtIndex(i), ((AchievementList)so.FindProperty("achievements").GetArrayElementAtIndex(i).FindPropertyRelative("achievement").enumValueIndex).ToString());

                    ach._Achievement = Database.Achievements[i].Achievement;
                    ach._Category = Database.Achievements[i].Category;
                    achievementManagerSceneInstance.Achievements.Add(ach._Achievement, ach);
                    Database.Achievements[i].SearchString = FindReplace(Database.Achievements[i], $"{ach._Category.ToString()} {Database.Achievements[i].AchievementName} {Database.Achievements[i].Description}").ToLower();
                    ach.SearchString = Database.Achievements[i].SearchString;

                    var achievementHeading = reference.Maps.Find(i => i.MapType == MapType.AchievementHeading).GetValue<TMP_Text>();
                    achievementHeading.text = Utilities.FindReplace(Database.Achievements[i], Database.Achievements[i].AchievementName);
                    achievementHeading.color = Database.Theme.AchievementHeadingTextColor;

                    var achievementDescription = reference.Maps.Find(i => i.MapType == MapType.AchievementDescription).GetValue<TMP_Text>();
                    achievementDescription.text = Utilities.FindReplace(Database.Achievements[i], Database.Achievements[i].Description);
                    achievementDescription.color = Database.Theme.AchievementDescriptionTextColor;

                    reference.Maps.Find(i => i.MapType == MapType.AchievementIcon).GetValue<Image>().sprite = Database.Achievements[i].Image;

                    var achievementPoints = reference.Maps.Find(i => i.MapType == MapType.AchievementAchievementPointsText).GetValue<TMP_Text>();
                    achievementPoints.text = $"AP: {Database.Achievements[i].AchievementPoints}";
                    achievementPoints.color = Database.Theme.AchievementPointsTextColor;

                    string progress = "";
                    if (Database.Display == AdvancedAchievementSystem.DisplayType.Value)
                    {
                        progress = so.FindProperty("achievements").GetArrayElementAtIndex(i).FindPropertyRelative("currentValue").intValue.ToString() + "/" +
                            so.FindProperty("achievements").GetArrayElementAtIndex(i).FindPropertyRelative("neededValue").intValue.ToString();
                    }
                    else
                        progress = "0%";

                    var progressText = reference.Maps.Find(i => i.MapType == MapType.AchievementProgressText).GetValue<TMP_Text>();
                    progressText.text = progress;
                    progressText.color = Database.Theme.AchievementProgressTextColor;
                    Slider slider = reference.Maps.Find(i => i.MapType == MapType.AchievementProgressSlider).GetValue<Slider>();

                    slider.maxValue = Database.Achievements[i].NeededValue;
                    slider.value = Database.Achievements[i].CurrentValue;

                    bool found = false;
                    //Loop through category buttons
                    for (int x = 0; x < Database.Categories.Count; x++)
                    {
                        //Loop through each category within the category buttons to find a category match
                        for (int c = 0; c < Database.Categories[x].subCategories.Count; c++)
                        {
                            if (Database.Categories[x].subCategories[c].Category == category)
                            {
                                found = true;
                                reference.Maps.Find(i => i.MapType == MapType.AchievementCompleteOverlay).GetValue<Image>().sprite = Database.Categories[x].subCategories[c].Image;
                                Color color = Database.Categories[x].subCategories[c].Color;

                                if (!Database.Achievements[i].Complete)
                                {
                                    var complete_overlay = reference.Maps.Find(i => i.MapType == MapType.AchievementCompleteOverlay).GetValue<Image>();
                                    complete_overlay.color =
                                    new Color(color.r,
                                    color.g,
                                    color.b,
                                    Database.Categories[x].subCategories[c].IncompleteAlpha);
                                }
                                else
                                {
                                    var complete_overlay = reference.Maps.Find(i => i.MapType == MapType.AchievementCompleteOverlay).GetValue<Image>();
                                    complete_overlay.color =
                                    new Color(color.r,
                                    color.g,
                                    color.b,
                                    Database.Categories[x].subCategories[c].CompleteAlpha);
                                }
                                break;
                            }
                        }

                        if (found)
                            break;
                    }

                    if (Database.Achievements[i].HiddenAchievement)
                        go.SetActive(false);
                }
                Utilities.ApplySelectedPrefabChanges(achievementManagerSceneInstance.gameObject);
                achievementManagerSceneInstance.Map.Maps.Find(i => i.MapType == MapType.AchievementSystemAchievementScrollViewRect).GetValue<ScrollRect>().CalculateLayoutInputVertical();
            }
            else
            {
                Debug.LogError("Achievement Prefab not filled out!");
            }
        }

        private string FindReplace(SerializedProperty achievement, string text)
        {
            text = text.Replace(Constants.REPLACE_CURRENT_VALUE, achievement.FindPropertyRelative("currentValue").intValue.ToString());
            text = text.Replace(Constants.REPLACE_NEEDED_VALUE, achievement.FindPropertyRelative("neededValue").intValue.ToString());
            text = text.Replace(Constants.REPLACE_ACHIEVEMENT_POINTS, achievement.FindPropertyRelative("achievementPoints").intValue.ToString());

            return text;
        }

        private string FindReplace(RealSoftGames.AdvancedAchievementSystem.BaseAchievement achievement, string text)
        {
            text = text.Replace(Constants.REPLACE_CURRENT_VALUE, achievement.CurrentValue.ToString());
            text = text.Replace(Constants.REPLACE_NEEDED_VALUE, achievement.NeededValue.ToString());
            text = text.Replace(Constants.REPLACE_ACHIEVEMENT_POINTS, achievement.AchievementPoints.ToString());

            return text;
        }

        private void ChangeAchievmentSystemEnabledState(bool state)
        {
            if (achievementManagerSceneInstance != null)
                achievementManagerSceneInstance.GetComponent<ReferenceMap>().GetComponentFromReferenceMapComponent<RectTransform>(MapType.AchievementSystemBackground).localScale = state == true ? Vector3.one : Vector3.zero;
            else
            {
                achievementManagerSceneInstance = FindFirstObjectByType<AchievementManager>();
                achievementManagerSceneInstance.GetComponent<ReferenceMap>().GetComponentFromReferenceMapComponent<RectTransform>(MapType.AchievementSystemBackground).localScale = state == true ? Vector3.one : Vector3.zero;
            }
            achievementSystemEnabled = state;
        }

        public static bool TryGetThemeAtPath(string path, out Theme theme)
        {
            theme = null;

            // Ensure the path ends with a "/"
            if (!path.EndsWith("/"))
            {
                path += "/";
            }

            // Get all asset paths at the specified location
            string[] assetGuids = AssetDatabase.FindAssets("t:Theme", new[] { path });

            // Iterate through the found assets
            foreach (string guid in assetGuids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                Theme foundTheme = AssetDatabase.LoadAssetAtPath<Theme>(assetPath);
                if (foundTheme != null)
                {
                    theme = foundTheme;
                    return true;
                }
            }

            // No Theme found
            return false;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
                ChangeAchievmentSystemEnabledState(false);
            else if (state == PlayModeStateChange.EnteredEditMode)
                OnEnable();
        }
    }
}