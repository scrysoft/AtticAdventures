using UnityEditor;
using UnityEngine;
using System.Linq;
using System;

namespace RealSoftGames.AdvancedAchievementSystem
{

    [CustomEditor(typeof(Map))]
    public class MapEditor : Editor
    {
        private SerializedProperty mapTypeProperty;
        private SerializedProperty componentProperty;
        private SerializedProperty boolProperty;
        private SerializedProperty referenceMapParent;
        private string searchTerm = "";
        private string[] enumNames;
        private int selectedIndex;

        private void OnEnable()
        {
            mapTypeProperty = serializedObject.FindProperty("mapType");
            componentProperty = serializedObject.FindProperty("component");
            boolProperty = serializedObject.FindProperty("hideFromThemeEditor");
            referenceMapParent = serializedObject.FindProperty("referenceMapParent");
            enumNames = System.Enum.GetNames(typeof(MapType));
            selectedIndex = mapTypeProperty.enumValueIndex;
        }

        public override void OnInspectorGUI()
        {
            // Draw Component with red text if null
            var componentLabelStyle = new GUIStyle(EditorStyles.label);
            componentLabelStyle.normal.textColor = Color.yellow;

            serializedObject.Update();

            EditorGUILayout.LabelField("Map Settings", EditorStyles.boldLabel);

            // Searchable Enum Dropdown
            SearchableEnumPopup(mapTypeProperty, "Map Type");

            // Component field
            EditorGUILayout.PropertyField(componentProperty);

            //Hide In Inspector Bool
            EditorGUILayout.PropertyField(boolProperty);

            //Reference Map Parent          
            EditorGUILayout.LabelField($"Reference Map Parent: {referenceMapParent.stringValue}", componentLabelStyle);

            serializedObject.ApplyModifiedProperties();
        }

        private void SearchableEnumPopup(SerializedProperty enumProperty, string label)
        {
            if (enumProperty.propertyType != SerializedPropertyType.Enum)
            {
                Debug.LogError("Provided serialized property is not an enum type.");
                return;
            }

            if (enumProperty.enumValueIndex < 0 || enumProperty.enumValueIndex >= enumProperty.enumNames.Length)
                enumProperty.enumValueIndex = 0;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(label);
            if (EditorGUILayout.DropdownButton(new GUIContent(enumProperty.enumDisplayNames[enumProperty.enumValueIndex]), FocusType.Keyboard))
            {
                SearchableDropdown searchableDropdown = new SearchableDropdown(enumProperty);
                PopupWindow.Show(GUILayoutUtility.GetLastRect(), searchableDropdown);
            }
            EditorGUILayout.EndHorizontal();
        }

        private class SearchableDropdown : PopupWindowContent
        {
            private SerializedProperty enumProperty;
            private string searchText = "";
            private Vector2 scrollPosition;

            private GUIStyle hoverStyle;
            private Color hoverColor = new Color(0.5f, 0.5f, 0.5f, 1.0f); // Dark grey color for hover effect

            // Maximum height for the dropdown window
            private const float MaxWindowHeight = 200f;

            // Height of each item in the dropdown
            private const float ItemHeight = 20f;

            // Padding for the window
            private const float WindowPadding = 25f;

            private float maxLabelWidth;

            public SearchableDropdown(SerializedProperty enumProperty)
            {
                this.enumProperty = enumProperty;

                // Set up the GUIStyle for hover display
                hoverStyle = new GUIStyle(GUI.skin.label);
                hoverStyle.normal.textColor = Color.white;
                hoverStyle.active.textColor = Color.white;
                hoverStyle.hover.textColor = Color.white;

                // Use a 2x2 white texture for hover background
                hoverStyle.hover.background = EditorGUIUtility.whiteTexture;
            }

            public override Vector2 GetWindowSize()
            {
                // Filtered list of enum names based on search text
                var filteredEnumNames = enumProperty.enumNames.Where(name => string.IsNullOrEmpty(searchText) || IsMatchingSearchTerms(name, searchText));

                // Calculate the maximum width based on the longest text length
                float maxWidth = 150f;
                foreach (var name in filteredEnumNames)
                {
                    float width = GUI.skin.label.CalcSize(new GUIContent(name)).x;
                    maxWidth = Mathf.Max(maxWidth, width);
                }

                // Update maxLabelWidth if necessary
                maxLabelWidth = Mathf.Max(maxLabelWidth, maxWidth);

                // Calculate the height based on the number of visible items, capped by the maximum window height
                float contentHeight = Mathf.Min(filteredEnumNames.Count() * ItemHeight, MaxWindowHeight);

                // Minimum width based on the largest label width and additional space for the search field
                float minWidth = maxLabelWidth + 45; // Adding space for the label

                return new Vector2(minWidth, contentHeight + WindowPadding); // Adding extra height for padding
            }

            private bool IsMatchingSearchTerms(string name, string search)
            {
                // Split search string into individual terms
                string[] searchTerms = search.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Check if all search terms are found in the name
                return searchTerms.All(term => name.ToLower().Contains(term));
            }

            public override void OnGUI(Rect rect)
            {
                // Search field
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Search:", GUILayout.Width(45));
                searchText = EditorGUILayout.TextField(searchText, GUILayout.ExpandWidth(true));
                EditorGUILayout.EndHorizontal();

                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                // Filtered list of enum names based on search text
                var filteredEnumNames = enumProperty.enumNames.Where(name => string.IsNullOrEmpty(searchText) || IsMatchingSearchTerms(name, searchText));

                // Display items
                foreach (var name in filteredEnumNames)
                {
                    Rect labelRect = GUILayoutUtility.GetRect(new GUIContent(name), GUI.skin.label);
                    HandleMouseHover(labelRect, Array.IndexOf(enumProperty.enumNames, name));
                }

                EditorGUILayout.EndScrollView();
            }

            // This method is called when the mouse hovers over the dropdown.
            private void HandleMouseHover(Rect labelRect, int i)
            {
                if (labelRect.Contains(Event.current.mousePosition))
                {
                    if (Event.current.type == EventType.Repaint)
                    {
                        EditorGUI.DrawRect(labelRect, hoverColor); // Draw hover background
                        GUI.Label(labelRect, enumProperty.enumDisplayNames[i], hoverStyle); // Draw label with hover style
                    }
                    else if (Event.current.type == EventType.MouseMove)
                    {
                        // Force repaint of the dropdown to update the hover effect
                        editorWindow.Repaint();
                    }

                    if (Event.current.type == EventType.MouseDown)
                    {
                        enumProperty.enumValueIndex = i;
                        enumProperty.serializedObject.ApplyModifiedProperties();
                        editorWindow.Close();
                        Event.current.Use();
                    }
                }
                else
                {
                    EditorGUI.LabelField(labelRect, enumProperty.enumDisplayNames[i]); // Draw label with normal style
                }
            }
        }
    }
}