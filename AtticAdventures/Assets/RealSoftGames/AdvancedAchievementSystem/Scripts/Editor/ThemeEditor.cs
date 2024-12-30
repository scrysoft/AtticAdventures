using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

namespace RealSoftGames.AdvancedAchievementSystem
{
    [CustomEditor(typeof(Theme))]
    public class ThemeEditor : Editor
    {
        private Theme theme;
        private string searchString = "";
        private Dictionary<string, ReorderableList> reorderableLists;
        private Dictionary<string, List<Theme.MapData>> originalThemeMaps;
        private Dictionary<MapType, List<string>> duplicateMapTypes;

        private void OnEnable()
        {
            theme = (Theme)target;

            reorderableLists = new Dictionary<string, ReorderableList>();
            originalThemeMaps = new Dictionary<string, List<Theme.MapData>>();
            duplicateMapTypes = new Dictionary<MapType, List<string>>();

            InitializeThemeMap();
            InitializeReorderableLists();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();



            //EditorGUILayout.Space();
            //if (GUILayout.Button("Copy From Theme Pack"))
            //{
            //    CopyFromThemePack();
            //}

            //EditorGUILayout.Space();
            //theme.CopyFromThemeMap = (Theme)EditorGUILayout.ObjectField("Copy From Theme Map", theme.CopyFromThemeMap, typeof(Theme), false);

            // Display Prefab and Color fields
            EditorGUILayout.Space();
            theme.AchievementSystemPrefab = (GameObject)EditorGUILayout.ObjectField("Achievement System Prefab", theme.AchievementSystemPrefab, typeof(GameObject), false);
            theme.AchievementPrefab = (GameObject)EditorGUILayout.ObjectField("Achievement Prefab", theme.AchievementPrefab, typeof(GameObject), false);
            theme.CategoryPrefab = (GameObject)EditorGUILayout.ObjectField("Category Prefab", theme.CategoryPrefab, typeof(GameObject), false);

            EditorGUILayout.Space();
            theme.CategoryButtonTextColor = EditorGUILayout.ColorField("Category Button Text Color", theme.CategoryButtonTextColor);
            theme.CategoryTextColor = EditorGUILayout.ColorField("Category Text Color", theme.CategoryTextColor);
            theme.AchievementSystemHeadingTextColor = EditorGUILayout.ColorField("Achievement System Heading Text Color", theme.AchievementSystemHeadingTextColor);
            theme.AchievementSystemCategoryHeadingTextColor = EditorGUILayout.ColorField("Achievement System Category Heading Text Color", theme.AchievementSystemCategoryHeadingTextColor);
            theme.AchievementHeadingTextColor = EditorGUILayout.ColorField("Achievement Heading Text Color", theme.AchievementHeadingTextColor);
            theme.AchievementDescriptionTextColor = EditorGUILayout.ColorField("Achievement Description Text Color", theme.AchievementDescriptionTextColor);
            theme.AchievementPointsTextColor = EditorGUILayout.ColorField("Achievement Points Text Color", theme.AchievementPointsTextColor);
            theme.AchievementProgressTextColor = EditorGUILayout.ColorField("Achievement Progress Text Color", theme.AchievementProgressTextColor);
            theme.SearchFieldPlaceHolderTextColor = EditorGUILayout.ColorField("Search Field Place Holder Text Color", theme.SearchFieldPlaceHolderTextColor);
            theme.SearchFieldTextColor = EditorGUILayout.ColorField("Search Field Text Color", theme.SearchFieldTextColor);
            theme.AchievementCompleteHeadingTextColor = EditorGUILayout.ColorField("Achievement Complete Heading Text Color", theme.AchievementCompleteHeadingTextColor);
            theme.AchievementCompleteDescriptionTextColor = EditorGUILayout.ColorField("Achievement Complete Description Text Color", theme.AchievementCompleteDescriptionTextColor);
            theme.AchievementCompleteAchievementPointsTextColor = EditorGUILayout.ColorField("Achievement Complete Achievement Points Text Color", theme.AchievementCompleteAchievementPointsTextColor);

            // Buttons
            EditorGUILayout.Space();
            if (GUILayout.Button("Initialize Map List"))
            {
                InitializeThemeMap();
                InitializeReorderableLists();
            }

            // Search field
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Search", EditorStyles.boldLabel);
            searchString = EditorGUILayout.TextField(searchString).Trim().Replace(" ", "");

            // Display ReorderableLists
            EditorGUILayout.Space();
            foreach (var key in reorderableLists.Keys.ToList())
            {
                FilterReorderableList(key);
                reorderableLists[key].DoLayoutList();
            }

            serializedObject.ApplyModifiedProperties();
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
            var filteredThemeMap = theme.ThemeMap.FindAll(mapData => referenceMap.Maps.Exists(map => map.MapType == mapData.MapType));

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
                    // Draw Reference Map Parent                     
                    EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), $"Reference Map Parent: ");
                    EditorGUI.LabelField(new Rect(rect.x + 135, rect.y, rect.width, EditorGUIUtility.singleLineHeight), $"{element.ReferenceMapParent}", new GUIStyle { normal = new GUIStyleState { textColor = Color.yellow } });

                    rect.y += lineHeight;
                    // Adjust height if there's a duplicate error message
                    if (duplicateFound)
                        rect.y += lineHeight * 2;

                },
                elementHeightCallback = index =>
                {
                    var listToDraw = (List<Theme.MapData>)reorderableLists[key].list;

                    if (index >= listToDraw.Count) return 0;

                    float lineHeight = EditorGUIUtility.singleLineHeight + 3;
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

        private void InitializeThemeMap()
        {
            theme.ThemeMap.Clear();
            duplicateMapTypes.Clear();

            AddMapsFromReferenceMap(theme.AchievementSystemPrefab);
            AddMapsFromReferenceMap(theme.AchievementPrefab);
            AddMapsFromReferenceMap(theme.CategoryPrefab);

            // Check for duplicates
            foreach (var mapData in theme.ThemeMap)
            {
                if (duplicateMapTypes.ContainsKey(mapData.MapType))
                {
                    duplicateMapTypes[mapData.MapType].Add(mapData.MapType.ToString());
                }
                else
                {
                    duplicateMapTypes[mapData.MapType] = new List<string> { mapData.MapType.ToString() };
                }
            }
        }

        private void AddMapsFromReferenceMap(GameObject prefab)
        {
            if (prefab == null) return;

            var referenceMaps = prefab.GetComponentsInChildren<ReferenceMap>();
            foreach (var referenceMap in referenceMaps)
            {
                foreach (var map in referenceMap.Maps)
                {
                    if (map.HideFromThemeEditor) continue;

                    var existingMapData = theme.ThemeMap.Find(m => m.MapType == map.MapType);
                    if (existingMapData != null)
                    {
                        if (map.TryGetComponent(typeof(Image), out var comp))
                        {
                            existingMapData.Sprite = map.GetComponent<Image>()?.sprite;
                            existingMapData.SpriteColor = map.GetComponent<Image>()?.color ?? Color.white;
                            existingMapData.ReferenceMapParent = referenceMap.ReferenceMapName;
                        }
                        else
                        {
                            Debug.LogError($"Component 'Image' not found on {map.gameObject.name}");
                        }
                    }
                    else
                    {
                        if (map.TryGetComponent(typeof(Image), out var comp))
                        {
                            var newData = new Theme.MapData(map.MapType)
                            {
                                Sprite = map.GetComponent<Image>()?.sprite,
                                SpriteColor = map.GetComponent<Image>()?.color ?? Color.white
                            };

                            theme.ThemeMap.Add(newData);
                            newData.ReferenceMapParent = referenceMap.ReferenceMapName;
                        }
                        else
                        {
                            Debug.LogError($"Component 'Image' not found on {map.gameObject.name}");
                        }
                    }
                }
            }
        }
        /*

        private void CopyFromThemePack()
        {
            if (theme.CopyFromThemeMap == null)
            {
                EditorUtility.DisplayDialog("Error", "No theme map selected to copy from.", "OK");
                return;
            }

            foreach (var sourceMap in theme.CopyFromThemeMap.ThemeMap)
            {
                var targetMap = theme.ThemeMap.Find(m => m.MapType == sourceMap.MapType);
                if (targetMap != null)
                {
                    targetMap.Sprite = sourceMap.Sprite;
                    targetMap.SpriteColor = sourceMap.SpriteColor;
                }
                else
                {
                    theme.ThemeMap.Add(new Theme.MapData(sourceMap.MapType)
                    {
                        Sprite = sourceMap.Sprite,
                        SpriteColor = sourceMap.SpriteColor
                    });
                }
            }
        }
*/
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

    }
}
