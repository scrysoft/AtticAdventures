using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace RealSoftGames.AdvancedAchievementSystem
{
    [CustomEditor(typeof(ReferenceMap))]
    public class ReferenceMapEditor : Editor
    {
        private ReferenceMap referenceMap;
        private ReorderableList reorderableList;
        private string searchString = "";

        private void OnEnable()
        {
            referenceMap = (ReferenceMap)target;
            InitializeReorderableList();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // ReferenceMapName field
            referenceMap.ReferenceMapName = EditorGUILayout.TextField("Reference Map Name", referenceMap.ReferenceMapName);

            // Scan and Add References Button
            EditorGUILayout.Space();
            if (GUILayout.Button("Scan and Add References"))
            {
                ScanAndAddReferences(referenceMap);
                InitializeReorderableList();
            }

            // Search field
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Search", EditorStyles.boldLabel);
            searchString = EditorGUILayout.TextField(searchString);
            EditorGUILayout.Space();

            // Display ReorderableList
            EditorGUILayout.Space();
            FilterReorderableList();
            reorderableList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }

        private void InitializeReorderableList()
        {
            reorderableList = new ReorderableList(referenceMap.Maps, typeof(Map), true, true, true, true);

            reorderableList.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "Map References");
            };

            reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                if (index < 0 || index >= reorderableList.count) return;

                var element = reorderableList.list[index] as Map;
                if (element == null) return;

                rect.y += 2;
                float lineHeight = EditorGUIUtility.singleLineHeight + 2;

                // Draw MapType as label with Ping button
                EditorGUI.LabelField(new Rect(rect.x, rect.y, 80, EditorGUIUtility.singleLineHeight), "Map Type:");
                EditorGUI.LabelField(new Rect(rect.x + 80, rect.y, rect.width - 140, EditorGUIUtility.singleLineHeight), Utilities.ToCamelCase(element.MapType.ToString()));

                if (GUI.Button(new Rect(rect.x + rect.width - 55, rect.y, 50, EditorGUIUtility.singleLineHeight), "Ping"))
                {
                    EditorGUIUtility.PingObject(element);
                }

                rect.y += lineHeight;

                // Draw Component with red text if null
                var componentLabelStyle = new GUIStyle(EditorStyles.label);
                if (element.Component == null)
                {
                    componentLabelStyle.normal.textColor = Color.red;
                }

                EditorGUI.LabelField(new Rect(rect.x, rect.y, 80, EditorGUIUtility.singleLineHeight), "Component:", componentLabelStyle);
                element.Component = (Component)EditorGUI.ObjectField(new Rect(rect.x + 80, rect.y, rect.width - 80, EditorGUIUtility.singleLineHeight), element.Component, typeof(Component), true);

                if (element.Component != null)
                {
                    rect.y += lineHeight;
                    EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), "Component Type: " + element.Component.GetType().Name);
                }
                componentLabelStyle.normal.textColor = Color.yellow;
                rect.y += lineHeight;
                EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), "Reference Map Parent: " + element.ReferenceMapParent, componentLabelStyle);

                rect.y += lineHeight;
                // Draw HideFromThemeEditor label with color
                var hideLabelStyle = new GUIStyle(EditorStyles.label)
                {
                    normal = { textColor = element.HideFromThemeEditor ? Color.red : Color.green }
                };

                EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), "Hide From Theme Editor", hideLabelStyle);
                element.HideFromThemeEditor = EditorGUI.Toggle(new Rect(rect.x + rect.width - 20, rect.y, 20, EditorGUIUtility.singleLineHeight), element.HideFromThemeEditor);
            };

            reorderableList.elementHeightCallback = (index) =>
            {
                var element = referenceMap.Maps[index];
                float lineHeight = EditorGUIUtility.singleLineHeight;
                int lines = element.Component != null ? 6 : 5;
                return lines * lineHeight; // Extra padding for spacing
            };

            reorderableList.onAddCallback = (ReorderableList list) =>
            {
                referenceMap.Maps.Add(new Map());
            };

            reorderableList.onRemoveCallback = (ReorderableList list) =>
            {
                if (EditorUtility.DisplayDialog("Warning", "Are you sure you want to delete the element?", "Yes", "No"))
                {
                    referenceMap.Maps.RemoveAt(list.index);
                }
            };
        }

        private void FilterReorderableList()
        {
            List<Map> filteredMaps = new List<Map>();

            foreach (var map in referenceMap.Maps)
            {
                if (map == null) continue;

                string searchStr = searchString.ToLower().Replace(" ", "");
                bool matchesSearch = map.MapType.ToString().ToLower().Contains(searchStr)
                                     || (map.Component != null && map.Component.GetType().Name.ToLower().Contains(searchStr))
                                     || (map.Component != null && map.Component.name.ToLower().Contains(searchStr));

                if (matchesSearch)
                {
                    filteredMaps.Add(map);
                }
            }

            reorderableList.list = filteredMaps;
        }

        private void ScanAndAddReferences(ReferenceMap referenceMap)
        {
            referenceMap.Maps.Clear();
            Transform rootTransform = referenceMap.transform;
            List<Map> foundMaps = new List<Map>();

            foreach (Transform child in rootTransform.DeepFind())
            {
                Map[] mapComponents = child.GetComponents<Map>();
                if (mapComponents != null)
                {
                    foreach (var map in mapComponents)
                        map.ReferenceMapParent = referenceMap.ReferenceMapName;
                    foundMaps.AddRange(mapComponents);
                }
            }

            referenceMap.Maps = foundMaps;

            Debug.Log($"Found and added {foundMaps.Count} map references.");
            EditorUtility.SetDirty(referenceMap);
        }
    }

    public static class TransformExtensions
    {
        public static IEnumerable<Transform> DeepFind(this Transform parent)
        {
            Stack<Transform> stack = new Stack<Transform>();
            stack.Push(parent);

            while (stack.Count > 0)
            {
                Transform current = stack.Pop();
                yield return current;

                foreach (Transform child in current)
                {
                    stack.Push(child);
                }
            }
        }
    }
}
