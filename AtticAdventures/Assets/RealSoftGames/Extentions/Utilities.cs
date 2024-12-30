using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System;
using System.Linq;
using RealSoftGames.AdvancedAchievementSystem;
using System.Text.RegularExpressions;



#if UNITY_EDITOR

using UnityEditor;

#endif

namespace RealSoftGames
{
    public static class Utilities
    {
        public static void GenerateEnumScript(
            string enumName,
            List<string> additionalValues, // Additional enum names provided by the user.
            string directoryPath,
            string fileName,
            string[] subNamespaces = null,
            string[] defaultEnumValues = null)// Guaranteed enum names that should be included first.) // Renamed for clarity
        {
            // Combine guaranteed values with additional values, ensuring guaranteed values are first.
            // We also remove duplicates while preserving the order of the list.
            List<string> uniqueValues = new List<string>();

            if (defaultEnumValues != null)
                uniqueValues = defaultEnumValues.Concat(additionalValues.Except(defaultEnumValues)).ToList();
            else
                uniqueValues = additionalValues.Distinct().ToList();

            // Correctly constructing the full namespace based on whether sub-namespaces are provided.
            string fullNamespace = "RealSoftGames";
            if (subNamespaces != null && subNamespaces.Length > 0)
            {
                fullNamespace += "." + string.Join(".", subNamespaces);
            }

            // Continue with file path and script content generation.
            string filePath = Path.Combine(directoryPath, fileName + ".cs");
            string scriptContent = GenerateEnumScriptContent(fullNamespace, enumName, uniqueValues);
            SaveScriptToFile(filePath, scriptContent);
        }

        private static string GenerateEnumScriptContent(string fullNamespace, string enumName, List<string> values)
        {
            StringBuilder sb = new StringBuilder();

            // Use the full namespace
            sb.AppendLine($"namespace {fullNamespace}");
            sb.AppendLine("{");
            sb.AppendLine($"    public enum {enumName}");
            sb.AppendLine("    {");

            // Add the enum values
            foreach (string value in values)
            {
                sb.AppendLine($"        {value},");
            }

            // End the enum definition
            sb.AppendLine("    }");

            sb.AppendLine("}"); // Close the namespace

            return sb.ToString();
        }

        private static void SaveScriptToFile(string filePath, string content)
        {
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllText(filePath, content);
        }

        private static string searchText = "";

        public static string FindReplace(BaseAchievement achievement, string text)
        {
            text = text.Replace(Constants.REPLACE_CURRENT_VALUE, achievement.CurrentValue.ToString());
            text = text.Replace(Constants.REPLACE_NEEDED_VALUE, achievement.NeededValue.ToString());
            text = text.Replace(Constants.REPLACE_ACHIEVEMENT_POINTS, achievement.AchievementPoints.ToString());

            return text;
        }

        public static string ToCamelCase(string str)
        {
            return Regex.Replace(str, "(\\B[A-Z])", " $1");
        }

#if UNITY_EDITOR

        public static void ApplySelectedPrefabChanges(GameObject sceneGameObject)
        {
            if (EditorApplication.isPlaying || EditorApplication.isPlayingOrWillChangePlaymode)
                return;

            // Get the root of the prefab instance
            GameObject prefabInstanceRoot = PrefabUtility.GetOutermostPrefabInstanceRoot(sceneGameObject);
            if (prefabInstanceRoot != null)
            {
                // Get the corresponding prefab asset
                UnityEngine.Object prefabAsset = PrefabUtility.GetCorrespondingObjectFromSource(prefabInstanceRoot);

                // Record changes on the prefab
                Undo.RecordObject(prefabAsset, "Apply Prefab Changes");

                // Apply changes from the instance to the prefab
                PrefabUtility.ApplyPrefabInstance(prefabInstanceRoot, InteractionMode.UserAction);
            }
        }

        public static float singleLineHeight
        {
            get => EditorGUIUtility.singleLineHeight;
        }

        public static float StandardVerticalSpacing
        {
            get => EditorGUIUtility.standardVerticalSpacing;
        }

        #region Material

        public static Material LoadMaterial(string materialName, string directoryPath)
        {
            // Construct the full path to the material asset
            string materialPath = $"Assets/{directoryPath}/{materialName}.mat";

            // Load the material from the specified path
            Material loadedMaterial = AssetDatabase.LoadAssetAtPath<Material>(materialPath);

            // Check if the material was loaded successfully
            if (loadedMaterial != null)
            {
                Debug.Log($"Material {materialName} was loaded successfully from {materialPath}.");
            }
            else
            {
                Debug.LogError($"Failed to load material {materialName} from {materialPath}. Make sure the path is correct and the asset exists.");
            }

            return loadedMaterial;
        }

        public static T Load<T>(string itemName, string directoryPath) where T : UnityEngine.Object
        {
            // Determine the file extension based on the type of asset being loaded
            string extension = GetExtensionForType<T>();
            string path = $"Assets/{directoryPath}/{itemName}{extension}";

            // Load the asset from the specified path
            T loadedItem = AssetDatabase.LoadAssetAtPath<T>(path);

            // Check if the asset was loaded successfully
            if (loadedItem != null)
            {
                Debug.Log($"{typeof(T).Name} {itemName} was loaded successfully from {path}.");
            }
            else
            {
                Debug.LogError($"Failed to load {typeof(T).Name} {itemName} from {path}. Make sure the path is correct and the asset exists.");
            }

            return loadedItem;
        }

        private static string GetExtensionForType<T>() where T : UnityEngine.Object
        {
            // Map asset types to their extensions
            if (typeof(T) == typeof(Texture2D))
            {
                return ".png"; // Assume .png for textures, but could be ".jpg", ".tga", etc.
            }
            else if (typeof(T) == typeof(Material))
            {
                return ".mat";
            }
            else if (typeof(T) == typeof(Shader))
            {
                return ".shader";
            }
            else if (typeof(T) == typeof(AudioClip))
            {
                return ".mp3"; // or ".wav" etc.
            }
            // ... Add more types as needed

            // Default to no extension if type is not handled
            return "";
        }

        #endregion Material

        #region Editor GUI Layout

        public static T EditorGUILayoutField<T>(string label, T value, bool readOnly = false)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = Mathf.Max(GUI.skin.label.CalcSize(new GUIContent(label)).x + 5, 180); // Add 5 pixels buffer

            // Directly use EditorGUILayout.PrefixLabel to automatically handle label widths
            EditorGUILayout.PrefixLabel(label);

            // Temporarily set GUI to read-only if required
            bool previousGUIState = GUI.enabled;
            GUI.enabled = !readOnly;

            // Draw the appropriate field based on the type of T
            value = DrawLayoutFieldByType(value);

            // Reset GUI enabled back to its previous state
            GUI.enabled = previousGUIState;

            EditorGUILayout.EndHorizontal();
            EditorGUIUtility.labelWidth = 0; // Reset the label width for subsequent controls
            return value;
        }

        public static void EditorGUILayoutField<T>(SerializedProperty property, string label = null, bool readOnly = false, bool allowSceneObjects = true)
        {
            // Begin a horizontal group
            EditorGUILayout.BeginHorizontal();

            GUIContent labelContent = string.IsNullOrEmpty(label) ? new GUIContent(property.displayName) : new GUIContent(label);

            // Calculate and set the label width
            EditorGUIUtility.labelWidth = Mathf.Max(GUI.skin.label.CalcSize(labelContent).x + 5, 180); // Add 5 pixels buffer

            // Temporarily set GUI to read-only if required
            bool previousGUIState = GUI.enabled;
            GUI.enabled = !readOnly;

            // If the property is an object reference, then use the specific handling
            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                // Assuming the label is integrated into the ObjectField
                property.objectReferenceValue = EditorGUILayout.ObjectField(labelContent, property.objectReferenceValue, typeof(T), allowSceneObjects);
            }
            else
            {
                // Use PrefixLabel to handle labels uniformly
                EditorGUILayout.PrefixLabel(labelContent);
                EditorGUILayout.PropertyField(property, GUIContent.none, true);
            }

            // Reset GUI enabled back to its previous state
            GUI.enabled = previousGUIState;

            // End the horizontal group and reset the label width
            EditorGUILayout.EndHorizontal();
            EditorGUIUtility.labelWidth = 0;
        }

        private static T DrawLayoutFieldByType<T>(T value)
        {
            Type type = typeof(T);

            // Handling integer type
            if (type == typeof(int))
            {
                return (T)(object)EditorGUILayout.IntField((int)(object)value);
            }
            // Handling float type
            else if (type == typeof(float))
            {
                return (T)(object)EditorGUILayout.FloatField((float)(object)value);
            }
            // Handling string type
            else if (type == typeof(string))
            {
                return (T)(object)EditorGUILayout.TextField((string)(object)value);
            }
            // Handling boolean type
            else if (type == typeof(bool))
            {
                return (T)(object)EditorGUILayout.Toggle((bool)(object)value);
            }
            // Handling Vector2 type
            else if (type == typeof(Vector2))
            {
                return (T)(object)EditorGUILayout.Vector2Field("", (Vector2)(object)value);
            }
            // Handling Vector3 type
            else if (type == typeof(Vector3))
            {
                return (T)(object)EditorGUILayout.Vector3Field("", (Vector3)(object)value);
            }
            // Handling Vector4 type
            else if (type == typeof(Vector4))
            {
                return (T)(object)EditorGUILayout.Vector4Field("", (Vector4)(object)value);
            }
            // Handling Color type
            else if (type == typeof(Color))
            {
                return (T)(object)EditorGUILayout.ColorField((Color)(object)value);
            }
            // Handling Rect type
            else if (type == typeof(Rect))
            {
                return (T)(object)EditorGUILayout.RectField((Rect)(object)value);
            }
            // Handling AnimationCurve type
            else if (type == typeof(AnimationCurve))
            {
                return (T)(object)EditorGUILayout.CurveField((AnimationCurve)(object)value);
            }
            // Handling Bounds type
            else if (type == typeof(Bounds))
            {
                return (T)(object)EditorGUILayout.BoundsField((Bounds)(object)value);
            }
            // Handling enum type
            else if (type.IsEnum)
            {
                return (T)(object)EditorGUILayout.EnumPopup((Enum)(object)value);
            }
            // Handling LayerMask type
            else if (type == typeof(LayerMask))
            {
                return (T)(object)EditorGUILayout.LayerField((LayerMask)(object)value);
            }
            // Handling Unity Object types (e.g., GameObject, Material)
            else if (typeof(UnityEngine.Object).IsAssignableFrom(type))
            {
                return (T)(object)EditorGUILayout.ObjectField((UnityEngine.Object)(object)value, type, true);
            }
            // Default case for unsupported types
            else
            {
                EditorGUILayout.LabelField($"Unsupported type: {type.Name}");
                return value;
            }
        }

        #endregion Editor GUI Layout

        #region Editor GUI

        public static T EditorGUIField<T>(ref Rect rect, string label, T value)
        {
            // Adjust the rect for the current indent level
            float indentOffset = EditorGUI.indentLevel * 15; // Default indent width in Unity is 15 pixels per level
            rect.x += indentOffset;
            rect.width -= indentOffset;

            // Set up the style for the label to ensure consistency
            GUIStyle style = new GUIStyle(GUI.skin.label)
            {
                wordWrap = false,
                alignment = TextAnchor.MiddleLeft,
                fontSize = 12, // Adjust as necessary
                padding = new RectOffset(2, 2, 2, 2),
            };

            // Create GUIContent for the label
            GUIContent labelContent = new GUIContent(label);

            // Measure the label size with the custom style.
            Vector2 labelSize = style.CalcSize(labelContent);
            // Calculate the minimum width for the label based on its content size and available space.
            float totalAvailableWidth = rect.width;

            // Set a minimum label width
            float minLabelWidth = 120f;

            // Calculate the label width based on the content, but not less than the minimum width
            float labelWidth = Mathf.Max(labelSize.x, minLabelWidth);

            // Ensure the label width does not exceed the total available width
            labelWidth = Mathf.Min(labelWidth, rect.width);
            labelWidth = Mathf.Min(labelWidth, totalAvailableWidth);

            // Adjust the position for the field based on the label width
            float fieldWidth = Mathf.Max(80, totalAvailableWidth - labelWidth);
            float fieldOffset = (labelWidth + style.padding.left) - indentOffset;

            // Define rects for label and field
            Rect labelRect = new Rect(rect.x, rect.y, labelWidth, EditorGUIUtility.singleLineHeight);
            Rect fieldRect = new Rect(rect.x + fieldOffset, rect.y, fieldWidth, EditorGUIUtility.singleLineHeight);
            //Rect fieldPosition = new Rect(rect.x + labelWidth, rect.y, fieldWidth, EditorGUIUtility.singleLineHeight);

            // Draw the label
            //EditorGUI.LabelField(new Rect(rect.x, rect.y, labelWidth, EditorGUIUtility.singleLineHeight), labelContent, labelStyle);

            // Draw field by type

            GUI.Label(labelRect, labelContent, style);
            T newValue = DrawFieldByType(ref fieldRect, value);

            // Increment rect.y after drawing the field for the next control
            rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            rect.x -= indentOffset;
            rect.width += indentOffset;

            return newValue;
        }

        private static T DrawFieldByType<T>(ref Rect position, T value)
        {
            Type type = typeof(T);

            T returnValue;

            if (type == typeof(int))
            {
                returnValue = (T)(object)EditorGUI.IntField(position, (int)(object)value);
            }
            else if (type == typeof(float))
            {
                returnValue = (T)(object)EditorGUI.FloatField(position, (float)(object)value);
            }
            else if (type == typeof(string))
            {
                returnValue = (T)(object)EditorGUI.TextField(position, (string)(object)value);
            }
            else if (type == typeof(bool))
            {
                returnValue = (T)(object)EditorGUI.Toggle(position, (bool)(object)value);
            }
            else if (type == typeof(Vector2))
            {
                returnValue = (T)(object)EditorGUI.Vector2Field(position, "", (Vector2)(object)value);
            }
            else if (type == typeof(Vector3))
            {
                returnValue = (T)(object)EditorGUI.Vector3Field(position, "", (Vector3)(object)value);
            }
            else if (type == typeof(Vector4))
            {
                returnValue = (T)(object)EditorGUI.Vector4Field(position, "", (Vector4)(object)value);
            }
            else if (type == typeof(Color))
            {
                returnValue = (T)(object)EditorGUI.ColorField(position, (Color)(object)value);
            }
            else if (type == typeof(Rect))
            {
                returnValue = (T)(object)EditorGUI.RectField(position, (Rect)(object)value);
            }
            else if (type == typeof(AnimationCurve))
            {
                returnValue = (T)(object)EditorGUI.CurveField(position, (AnimationCurve)(object)value);
            }
            else if (type == typeof(Bounds))
            {
                returnValue = (T)(object)EditorGUI.BoundsField(position, (Bounds)(object)value);
            }
            else if (type.IsEnum)
            {
                returnValue = (T)(object)EditorGUI.EnumPopup(position, (Enum)(object)value);
            }
            else if (type == typeof(LayerMask))
            {
                returnValue = (T)(object)EditorGUI.LayerField(position, (LayerMask)(object)value);
            }
            else if (typeof(UnityEngine.Object).IsAssignableFrom(type))
            {
                returnValue = (T)(object)EditorGUI.ObjectField(position, (UnityEngine.Object)(object)value, type, true);
            }
            else
            {
                EditorGUI.LabelField(position, $"Unsupported type: {type.Name}");
                returnValue = value;
            }

            // Increment the y position for the next field.
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            return returnValue;
        }

        public static void EditorGUIField(ref Rect rect, SerializedProperty property, string label = null)
        {
            // Adjust the rect for the current indent level
            float indentOffset = EditorGUI.indentLevel * 15; // Default indent width in Unity is 15 pixels per level
            rect.x += indentOffset;
            rect.width -= indentOffset;

            GUIStyle style = new GUIStyle(GUI.skin.label)
            {
                wordWrap = false,
                alignment = TextAnchor.MiddleLeft,
                fontSize = 12, // Adjust as necessary
                padding = new RectOffset(2, 2, 2, 2),
            };

            GUIContent labelContent = new GUIContent(label);

            // Measure the label size with the custom style.
            Vector2 labelSize = style.CalcSize(labelContent);

            // Calculate the minimum width for the label based on its content size and available space.
            float totalAvailableWidth = rect.width;
            // Set a minimum label width
            float minLabelWidth = 120f;
            // Calculate the label width based on the content, but not less than the minimum width
            float labelWidth = Mathf.Max(labelSize.x, minLabelWidth);
            // Ensure the label width does not exceed the total available width
            labelWidth = Mathf.Min(labelWidth, totalAvailableWidth);
            float fieldWidth = Mathf.Max(80, totalAvailableWidth - labelWidth); // Ensure field is not too small
            float fieldOffset = (labelWidth + style.padding.left) - indentOffset;

            // Define rects for label and field
            Rect labelRect = new Rect(rect.x, rect.y, labelWidth, EditorGUIUtility.singleLineHeight);
            Rect fieldRect = new Rect(rect.x + fieldOffset, rect.y, fieldWidth, EditorGUIUtility.singleLineHeight);

            // Draw colored boxes for visualization
            //EditorGUI.DrawRect(labelRect, new Color(255, 0, 0, 0.2f));
            //EditorGUI.DrawRect(fieldRect, new Color(0, 0, 255, 0.2f));

            GUI.Label(labelRect, labelContent, style);

            EditorGUI.PropertyField(fieldRect, property, GUIContent.none, true);
            rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            rect.x -= indentOffset;
            rect.width += indentOffset;
        }

        public static void EditorGUITextArea(ref Rect rect, SerializedProperty property, string label, int textAreaHeight = 4)
        {
            // Adjust the rect for the current indent level
            float indentOffset = EditorGUI.indentLevel * 15; // Default indent width in Unity is 15 pixels per level


            GUIStyle style = new GUIStyle(GUI.skin.label)
            {
                wordWrap = false,
                alignment = TextAnchor.MiddleLeft,
                fontSize = 12, // Adjust as necessary
                padding = new RectOffset(2, 2, 2, 2),
            };

            GUIContent labelContent = new GUIContent(label ?? property.displayName);

            // Measure the label size with the custom style.
            Vector2 labelSize = style.CalcSize(labelContent);

            // Set a minimum label width
            float minLabelWidth = 120f;
            // Calculate the label width based on the content, but not less than the minimum width
            float labelWidth = Mathf.Max(labelSize.x, minLabelWidth);
            // Ensure the label width does not exceed the total available width
            labelWidth = Mathf.Min(labelWidth, rect.width);
            float fieldWidth = rect.width - labelWidth; // Use the rest of the space for the field

            // Define rect for label
            Rect labelRect = new Rect(rect.x, rect.y, labelWidth, EditorGUIUtility.singleLineHeight);
            // Draw the label
            EditorGUI.LabelField(labelRect, labelContent, style);

            // Define rect for the text area field
            Rect fieldRect = new Rect(rect.x + labelWidth, rect.y, fieldWidth, EditorGUIUtility.singleLineHeight * textAreaHeight);
            // Draw the text area
            EditorGUI.PropertyField(fieldRect, property, GUIContent.none, true);

            // Increment rect.y after drawing the field for the next control
            rect.y += (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * textAreaHeight;

        }

        public static void EditorGUILabel(ref Rect rect, string label, GUIStyle style = null)
        {
            // Adjust the rect for the current indent level
            float indentOffset = EditorGUI.indentLevel * 15; // Default indent width in Unity is 15 pixels per level
            rect.x += indentOffset;
            rect.width -= indentOffset;

            // If no style is provided, use the default label style
            if (style == null)
            {
                style = new GUIStyle(GUI.skin.label)
                {
                    wordWrap = false,
                    alignment = TextAnchor.MiddleLeft,
                    fontSize = 12, // Adjust as necessary
                    padding = new RectOffset(2, 2, 2, 2),
                };
            }

            GUIContent labelContent = new GUIContent(label);

            // Measure the label size with the provided style.
            Vector2 labelSize = style.CalcSize(labelContent);

            // Set a minimum label width
            float minLabelWidth = 120f;
            // Calculate the label width based on the content, but not less than the minimum width
            float labelWidth = Mathf.Max(labelSize.x, minLabelWidth);
            // Ensure the label width does not exceed the total available width
            labelWidth = Mathf.Min(labelWidth, rect.width);

            // Define rect for label
            Rect labelRect = new Rect(rect.x, rect.y, labelWidth, EditorGUIUtility.singleLineHeight);

            // Draw the label
            GUI.Label(labelRect, labelContent, style);

            // Increment rect.y after drawing the label for the next control
            rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // Reset the rect x and width to account for the next control
            rect.x -= indentOffset;
            rect.width += indentOffset;
        }

        public static void EditorGUIIntSlider(ref Rect rect, SerializedProperty property, int min, int max, string label = null)
        {
            // Adjust the rect for the current indent level
            float indentOffset = EditorGUI.indentLevel * 15; // Default indent width in Unity is 15 pixels per level

            // Set up the style for the label
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label)
            {
                wordWrap = false,
                alignment = TextAnchor.MiddleLeft,
                fontSize = 12,
                padding = new RectOffset(2, 2, 2, 2),
            };

            // Create GUIContent for the label
            GUIContent labelContent = new GUIContent(label ?? property.displayName);

            // Calculate the label width based on the content, with a minimum width
            float minLabelWidth = 120f; // Set a minimum label width
            float labelWidth = Mathf.Max(labelStyle.CalcSize(labelContent).x, minLabelWidth);

            // Adjust the rects for the label and the slider
            Rect labelRect = new Rect(rect.x, rect.y, labelWidth, EditorGUIUtility.singleLineHeight);
            Rect sliderRect = new Rect(labelRect.xMax, rect.y, rect.width - labelWidth - indentOffset, EditorGUIUtility.singleLineHeight);

            // Draw the label
            EditorGUI.LabelField(labelRect, labelContent, labelStyle);

            // Draw the IntSlider
            EditorGUI.BeginProperty(sliderRect, labelContent, property);
            property.intValue = EditorGUI.IntSlider(sliderRect, GUIContent.none, property.intValue, min, max);
            EditorGUI.EndProperty();

            // Increment rect.y after drawing the field for the next control
            rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        public static void EditorGUIFloatSlider(ref Rect rect, SerializedProperty property, float min, float max, string label = null)
        {
            // Adjust the rect for the current indent level
            float indentOffset = EditorGUI.indentLevel * 15; // Default indent width in Unity is 15 pixels per level

            // Set up the style for the label
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label)
            {
                wordWrap = false,
                alignment = TextAnchor.MiddleLeft,
                fontSize = 12,
                padding = new RectOffset(2, 2, 2, 2),
            };

            // Create GUIContent for the label
            GUIContent labelContent = new GUIContent(label ?? property.displayName);

            // Calculate the label width based on the content, with a minimum width
            float minLabelWidth = 120f; // Set a minimum label width
            float labelWidth = Mathf.Max(labelStyle.CalcSize(labelContent).x, minLabelWidth);

            // Adjust the rects for the label and the slider
            Rect labelRect = new Rect(rect.x + indentOffset, rect.y, labelWidth, EditorGUIUtility.singleLineHeight);
            Rect sliderRect = new Rect(labelRect.xMax, rect.y, rect.width - labelWidth - indentOffset, EditorGUIUtility.singleLineHeight);

            // Draw the label
            EditorGUI.LabelField(labelRect, labelContent, labelStyle);

            // Draw the FloatSlider
            EditorGUI.BeginProperty(sliderRect, labelContent, property);
            property.floatValue = EditorGUI.Slider(sliderRect, labelContent, property.floatValue, min, max);
            EditorGUI.EndProperty();

            // Increment rect.y after drawing the field for the next control
            rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // Reset the rect x and width to account for the next control
            rect.x -= indentOffset;
            rect.width += indentOffset;
        }

        public static bool EditorGUIFoldout(ref Rect rect, SerializedProperty element, string foldoutLabel, bool foldoutState)
        {
            // Adjust the rect for the current indent level
            float indentOffset = EditorGUI.indentLevel * 15; // Default indent width in Unity is 15 pixels per level
            rect.x += indentOffset;
            rect.width -= indentOffset;

            // Apply the foldout with the provided label and state
            foldoutState = EditorGUI.Foldout(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                foldoutState, new GUIContent(foldoutLabel), true);

            // Increment rect.y after drawing the foldout for the next control
            rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // Reset the rect x and width to account for the next control
            rect.x -= indentOffset;
            rect.width += indentOffset;

            // Return the new foldout state
            return foldoutState;
        }

        public static void EditorGUIColorField(ref Rect rect, SerializedProperty property, string label = null, bool showAlpha = true)
        {
            if (property.propertyType != SerializedPropertyType.Color)
            {
                Debug.LogError("Provided serialized property is not a color type.");
                return;
            }

            // Adjust the rect for the current indent level
            float indentOffset = EditorGUI.indentLevel * 15; // Default indent width in Unity is 15 pixels per level
            rect.x += indentOffset;
            rect.width -= indentOffset;

            GUIStyle style = new GUIStyle(GUI.skin.label)
            {
                wordWrap = false,
                alignment = TextAnchor.MiddleLeft,
                fontSize = 12, // Adjust as necessary
                padding = new RectOffset(2, 2, 2, 2),
            };

            GUIContent labelContent = label != null ? new GUIContent(label) : new GUIContent(property.displayName);

            // Measure the label size with the custom style.
            Vector2 labelSize = style.CalcSize(labelContent);

            // Calculate the minimum width for the label based on its content size and available space.
            float totalAvailableWidth = rect.width;
            float minLabelWidth = label != null ? 120f : 0f; // If no label is provided, we don't reserve space for it.
            float labelWidth = Mathf.Max(labelSize.x, minLabelWidth);
            labelWidth = Mathf.Min(labelWidth, totalAvailableWidth);

            // Define rects for label and field
            Rect labelRect = new Rect(rect.x, rect.y, labelWidth, EditorGUIUtility.singleLineHeight);
            Rect fieldRect = new Rect(rect.x + labelWidth, rect.y, rect.width - labelWidth, EditorGUIUtility.singleLineHeight);

            // Draw the label if provided
            if (!string.IsNullOrEmpty(label))
            {
                EditorGUI.LabelField(labelRect, labelContent, style);
            }

            // Draw the ColorField
            Color colorValue = property.colorValue;
            Color newColor = EditorGUI.ColorField(fieldRect, GUIContent.none, colorValue, showAlpha, false, false);
            if (newColor != colorValue) // Only assign the value back if it was actually changed.
            {
                property.colorValue = newColor;
            }

            // Increment rect.y after drawing the field for the next control
            rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // Reset rect.x and rect.width to account for the next control
            rect.x -= indentOffset;
            rect.width += indentOffset;
        }

        #endregion Editor GUI

        #region Search Enum

        public static void SearchableEnumPopup(ref Rect rect, SerializedProperty enumProperty, string label = null, bool dissabledGroup = false)
        {
            if (enumProperty.propertyType != SerializedPropertyType.Enum)
            {
                Debug.LogError("Provided serialized property is not an enum type.");
                return;
            }

            if (enumProperty.enumValueIndex < 0 || enumProperty.enumValueIndex >= enumProperty.enumNames.Length)
                enumProperty.enumValueIndex = 0;

            // Calculate the indentation offset for the label
            float indentOffset = EditorGUI.indentLevel * 15; // Default indent width in Unity is 15 pixels per level

            // If a label is provided, calculate the space it needs
            float labelWidth = 0f;
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label)
            {
                wordWrap = false,
                alignment = TextAnchor.MiddleLeft,
                fontSize = 12,
                //padding = new RectOffset(2, 2, 2, 2),
            };

            if (!string.IsNullOrEmpty(label))
            {
                GUIContent labelContent = new GUIContent(label);
                labelWidth = Mathf.Max(labelStyle.CalcSize(labelContent).x, 120f); // Ensure a minimum label width
                labelWidth = Mathf.Min(labelWidth + indentOffset, rect.width); // Adjust for available width
                EditorGUI.LabelField(new Rect(rect.x, rect.y, labelWidth, EditorGUIUtility.singleLineHeight), labelContent, labelStyle);
            }

            // Calculate rect for the dropdown button
            float dropdownWidth = rect.width - labelWidth - (string.IsNullOrEmpty(label) ? 0 : indentOffset);
            Rect dropdownRect = new Rect(rect.x + labelWidth, rect.y, dropdownWidth, EditorGUIUtility.singleLineHeight);

            string selectedName = enumProperty.enumDisplayNames[enumProperty.enumValueIndex];
            EditorGUI.BeginDisabledGroup(dissabledGroup);
            if (EditorGUI.DropdownButton(dropdownRect, new GUIContent(selectedName), FocusType.Keyboard))
            {
                SearchableDropdown searchableDropdown = new SearchableDropdown(enumProperty);
                PopupWindow.Show(dropdownRect, searchableDropdown);
            }
            EditorGUI.EndDisabledGroup();
            // Increment rect.y after drawing the dropdown for the next control
            rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            // Reset the rect x and width to account for the next control
        }

        public static void SearchableEnumPopup(SerializedProperty enumProperty, string label)
        {
            if (enumProperty.propertyType != SerializedPropertyType.Enum)
            {
                Debug.LogError("Provided serialized property is not an enum type.");
                return;
            }

            if (enumProperty.enumValueIndex < 0 || enumProperty.enumValueIndex >= enumProperty.enumNames.Length)
                enumProperty.enumValueIndex = 0;

            string selectedName = enumProperty.enumNames[enumProperty.enumValueIndex];
            GUIContent dropdownContent = new GUIContent(selectedName);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(label);
            if (EditorGUILayout.DropdownButton(dropdownContent, FocusType.Keyboard))
            {
                UnityEditor.PopupWindow.Show(GUILayoutUtility.GetLastRect(), new SearchableDropdown(enumProperty));
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

        #endregion Search Enum

        #region GuiLayoutContentZone

        public static Rect EditorGUILayoutBeginContentZone(float x, float y, float width, float height, float padding, ref Vector2 scrollPos)
        {
            // Draw the navigation panel on the left using a fixed Rect.
            Rect rect = new Rect(x, y,
                width - padding,
                 height - (padding * 2));

            GUILayout.BeginArea(rect);
            GUILayout.BeginVertical("box", GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));

            // Start the scroll view
            scrollPos = GUILayout.BeginScrollView(scrollPos, false, false);
            return rect;
        }

        public static void EditorGUILayoutEndContentZone()
        {
            // End the scroll view
            GUILayout.EndScrollView();
            // End the vertical layout for the main content.
            GUILayout.EndVertical();
            // End the padded area.
            GUILayout.EndArea();
        }

        #endregion GuiLayoutContentZone

        #region Drag & Drop Field

        public static T[] EditorGUIDragAndDropZone<T>(Rect dropArea, string label) where T : UnityEngine.Object
        {
            // Draw a box in the provided drop area
            GUI.Box(dropArea, "", EditorStyles.helpBox); // Using an empty string for the box and a HelpBox style to give it a distinct look
                                                         // Calculate label size
            var labelSize = GUI.skin.label.CalcSize(new GUIContent(label));
            // Calculate the position of the label to center it within the drop area
            var labelPosition = new Rect(
                dropArea.x + (dropArea.width - labelSize.x) / 2,
                dropArea.y + (dropArea.height - labelSize.y) / 2,
                labelSize.x,
                labelSize.y);
            // Draw the label at the calculated position
            GUI.Label(labelPosition, label);

            Event evt = Event.current;
            T[] draggedObjects = new T[0];

            switch (evt.type)
            {
                case EventType.DragUpdated:
                    if (!dropArea.Contains(evt.mousePosition))
                        break;

                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    break;

                case EventType.DragPerform:
                    if (!dropArea.Contains(evt.mousePosition))
                        break;

                    DragAndDrop.AcceptDrag();
                    draggedObjects = DragAndDrop.objectReferences.OfType<T>().ToArray();
                    break;
            }

            return draggedObjects;
        }

        public static T[] EditorGUILayoutDragAndDropZone<T>(string label, Action<T[]> onDrop = null, GUIStyle style = null, GUILayoutOption[] options = null) where T : UnityEngine.Object
        {
            // Start with the default style based on EditorStyles.helpBox
            GUIStyle defaultStyle = new GUIStyle(EditorStyles.helpBox)
            {
                alignment = TextAnchor.MiddleCenter, // Center the text inside the box
                                                     // You can set default styling options here
            };

            // If a custom style is provided, modify the default style based on the custom style's properties
            if (style != null)
            {
                defaultStyle = new GUIStyle(defaultStyle) // Copy the default style to keep its properties
                {
                    // Override specific properties from the custom style. For example:
                    font = style.font ?? defaultStyle.font,
                    fontSize = style.fontSize > 0 ? style.fontSize : defaultStyle.fontSize,
                    fontStyle = style.fontStyle != FontStyle.Normal ? style.fontStyle : defaultStyle.fontStyle,
                    alignment = style.alignment != TextAnchor.UpperLeft ? style.alignment : defaultStyle.alignment,
                    // Continue for other properties as needed
                };
            }

            // Create the box with the (potentially modified) default style
            GUILayout.Box(new GUIContent(label), defaultStyle, options ?? new GUILayoutOption[] { GUILayout.Height(50), GUILayout.ExpandWidth(true) });

            // Get the last drawn control's rectangle as the drop area
            Rect dropArea = GUILayoutUtility.GetLastRect();

            // Prepare an array to hold the objects that were dragged in
            T[] draggedObjects = new T[0];

            // Handle drag and drop logic
            var evt = Event.current;
            switch (evt.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (!dropArea.Contains(evt.mousePosition))
                        break;

                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                    if (evt.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();

                        draggedObjects = DragAndDrop.objectReferences.OfType<T>().ToArray();

                        //foreach (var obj in DragAndDrop.objectReferences)
                        //    Debug.Log(obj.GetType());
                        // Execute the onDrop action with the dragged objects if provided
                        onDrop?.Invoke(draggedObjects);
                        Event.current.Use();
                    }
                    break;
            }

            return draggedObjects;
        }

        #endregion Drag & Drop Field      

#endif
    }
}