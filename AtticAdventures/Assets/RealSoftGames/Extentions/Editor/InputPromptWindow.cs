using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealSoftGames.AdvancedAchievementSystem
{
    public class InputPromptWindow : EditorWindow
    {
        private static List<InputPromptWindow> openWindows = new List<InputPromptWindow>();
        private TaskCompletionSource<List<object>> tcs;
        private TaskCompletionSource<object> customTcs;

        // Input configuration class
        [Serializable]
        public class InputConfig
        {
            public string label;
            public InputType type;
            public object defaultValue;
            public float minValue;
            public float maxValue;
            public Type enumType;
            public Type ObjectType;
        }

        public enum InputType
        {
            TextField,
            PasswordField,
            Label,
            Slider,
            Toggle,
            Dropdown,
            ObjectField
        }

        private List<InputField> inputFields = new List<InputField>();
        private Action<List<object>> onSubmit;
        private Func<List<object>, bool> shouldClose;
        private Action onClose;
        private string titleMessage;
        private List<ButtonConfig> buttons;

        private class InputField
        {
            public string Label { get; }
            public object Value { get; set; }
            public InputType Type { get; }
            public float MinValue { get; } // For Slider
            public float MaxValue { get; } // For Slider
            public bool BoolValue { get; set; } // For Toggle
            public Type EnumType { get; } // For Dropdown
            public Enum EnumValue { get; set; }
            public Type ObjectType;

            // Constructor for ObjectField
            public InputField(string label, UnityEngine.Object defaultValue, Type objectType)
            {
                Label = label;
                Type = InputType.ObjectField;
                Value = defaultValue;
                ObjectType = objectType;
            }

            // Constructor for text fields and labels
            public InputField(string label, InputType type, object defaultValue = null)
            {
                Label = label;
                Type = type;
                Value = defaultValue;
                MinValue = 0f;
                MaxValue = 0f;
                BoolValue = false;
                EnumType = null;
                EnumValue = null;
            }

            // Constructor for sliders
            public InputField(string label, InputType type, float defaultValue, float minValue, float maxValue)
            {
                Label = label;
                Type = type;
                MinValue = minValue;
                MaxValue = maxValue;
                BoolValue = false;
                Value = defaultValue;
                EnumType = null;
                EnumValue = null;
            }

            // Constructor for toggles
            public InputField(string label, InputType type, bool defaultValue)
            {
                Label = label;
                Type = type;
                BoolValue = defaultValue;
                MinValue = 0f;
                MaxValue = 0f;
                Value = defaultValue; // Store as the correct type
                EnumType = null;
                EnumValue = null;
            }

            // Constructor for Dropdowns
            public InputField(string label, Type enumType, Enum defaultValue)
            {
                Label = label;
                Type = InputType.Dropdown;
                EnumType = enumType;
                EnumValue = defaultValue;
                MinValue = 0f;
                MaxValue = 0f;
                BoolValue = false;
                Value = defaultValue; // Store as the correct type
            }

            // Retrieve the value with the appropriate type
            public T GetValue<T>()
            {
                return (T)Value;
            }
        }

        [Serializable]
        public class ButtonConfig
        {
            public string label;
            public Action<List<object>> action;
            public Func<List<object>, bool> shouldCloseCondition;
        }

        public static void ShowWindow(string title, string message, List<InputConfig> inputConfigs, List<ButtonConfig> buttonConfigs, Action onClose = null)
        {
            InputPromptWindow window = CreateInstance<InputPromptWindow>();
            window.titleContent = new GUIContent(title);
            window.titleMessage = message;
            window.buttons = buttonConfigs;
            window.onClose = onClose;

            window.inputFields.Clear();

            if (inputConfigs != null)
            {
                foreach (var inputConfig in inputConfigs)
                {
                    switch (inputConfig.type)
                    {
                        case InputType.ObjectField:
                            // Cast defaultValue to the appropriate UnityEngine.Object type and pass the type to the constructor
                            window.inputFields.Add(new InputField(inputConfig.label, inputConfig.defaultValue as UnityEngine.Object, inputConfig.ObjectType));
                            break;

                        case InputType.TextField:
                        case InputType.PasswordField:
                        case InputType.Label:
                        case InputType.Slider:
                        case InputType.Toggle:
                        case InputType.Dropdown:
                            // For other input types that are not ObjectFields, use the existing constructor
                            window.inputFields.Add(new InputField(inputConfig.label, inputConfig.type, inputConfig.defaultValue));
                            break;

                        default:
                            // Handle other types or throw an exception if an unsupported type is encountered
                            throw new ArgumentException($"Unsupported input type: {inputConfig.type}");
                    }
                }
            }

            openWindows.Add(window);
            window.Show();
        }

        public static Task<List<object>> ShowWindowAsync(string title, string message, List<InputConfig> inputConfigs, List<ButtonConfig> buttonConfigs, Action onClose = null)
        {
            InputPromptWindow window = CreateInstance<InputPromptWindow>();
            window.titleContent = new GUIContent(title);
            window.titleMessage = message;
            window.buttons = buttonConfigs;
            window.onClose = onClose;

            window.inputFields.Clear();

            if (inputConfigs != null)
            {
                foreach (var inputConfig in inputConfigs)
                {
                    switch (inputConfig.type)
                    {
                        case InputType.ObjectField:
                            // Cast defaultValue to the appropriate UnityEngine.Object type and pass the type to the constructor
                            window.inputFields.Add(new InputField(inputConfig.label, inputConfig.defaultValue as UnityEngine.Object, inputConfig.ObjectType));
                            break;

                        case InputType.TextField:
                        case InputType.PasswordField:
                        case InputType.Label:
                        case InputType.Slider:
                        case InputType.Toggle:
                        case InputType.Dropdown:
                            // For other input types that are not ObjectFields, use the existing constructor
                            window.inputFields.Add(new InputField(inputConfig.label, inputConfig.type, inputConfig.defaultValue));
                            break;

                        default:
                            // Handle other types or throw an exception if an unsupported type is encountered
                            throw new ArgumentException($"Unsupported input type: {inputConfig.type}");
                    }
                }
            }

            openWindows.Add(window);
            window.Show();

            window.tcs = new TaskCompletionSource<List<object>>();
            return window.tcs.Task;
        }

        public static Task<TResult> ShowWindowAsync<TResult>(string title, string message, List<InputConfig> inputConfigs, List<ButtonConfig> buttonConfigs, Func<List<object>, TResult> processResult, Action onClose = null)
        {
            InputPromptWindow window = CreateInstance<InputPromptWindow>();
            window.titleContent = new GUIContent(title);
            window.titleMessage = message;
            window.buttons = buttonConfigs;
            window.onClose = onClose;

            window.inputFields.Clear();

            if (inputConfigs != null)
            {
                foreach (var inputConfig in inputConfigs)
                {
                    switch (inputConfig.type)
                    {
                        case InputType.ObjectField:
                            // Cast defaultValue to the appropriate UnityEngine.Object type and pass the type to the constructor
                            window.inputFields.Add(new InputField(inputConfig.label, inputConfig.defaultValue as UnityEngine.Object, inputConfig.ObjectType));
                            break;

                        case InputType.TextField:
                        case InputType.PasswordField:
                        case InputType.Label:
                        case InputType.Slider:
                        case InputType.Toggle:
                        case InputType.Dropdown:
                            // For other input types that are not ObjectFields, use the existing constructor
                            window.inputFields.Add(new InputField(inputConfig.label, inputConfig.type, inputConfig.defaultValue));
                            break;

                        default:
                            // Handle other types or throw an exception if an unsupported type is encountered
                            throw new ArgumentException($"Unsupported input type: {inputConfig.type}");
                    }
                }
            }

            openWindows.Add(window);
            window.Show();

            window.customTcs = new TaskCompletionSource<object>();
            window.tcs = new TaskCompletionSource<List<object>>();
            return window.customTcs.Task.ContinueWith(t => processResult((List<object>)t.Result));
        }

        private void OnDestroy()
        {
            openWindows.Remove(this);
            onClose?.Invoke();
            tcs?.TrySetResult(null);
            customTcs?.TrySetResult(null);
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField(titleMessage, EditorStyles.boldLabel);

            foreach (var inputField in inputFields)
            {
                switch (inputField.Type)
                {
                    case InputType.TextField:
                        inputField.Value = EditorGUILayout.TextField(inputField.Label, inputField.Value.ToString());
                        break;

                    case InputType.PasswordField:
                        inputField.Value = EditorGUILayout.PasswordField(inputField.Label, inputField.Value.ToString());
                        break;

                    case InputType.Label:
                        EditorGUILayout.LabelField(inputField.Label, inputField.Value.ToString());
                        break;

                    case InputType.Slider:
                        inputField.Value = EditorGUILayout.Slider(inputField.Label, (float)inputField.Value, 0f, 1f);
                        break;

                    case InputType.Toggle:
                        inputField.Value = EditorGUILayout.Toggle(inputField.Label, (bool)inputField.Value);
                        break;

                    case InputType.Dropdown:
                        if (inputField.Value != null && inputField.Value.GetType().IsEnum)
                        {
                            inputField.Value = EditorGUILayout.EnumPopup(inputField.Label, (Enum)inputField.Value);
                        }
                        break;

                    case InputType.ObjectField:
                        inputField.Value = EditorGUILayout.ObjectField(inputField.Label, (UnityEngine.Object)inputField.Value, inputField.ObjectType, true);
                        break;
                }
            }

            GUILayout.FlexibleSpace();

            EditorGUILayout.BeginHorizontal();
            foreach (var buttonConfig in buttons)
            {
                if (GUILayout.Button(buttonConfig.label, new GUIStyle(GUI.skin.button)
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 12,
                    fixedHeight = 25
                }))
                {
                    List<object> inputValues = new List<object>();
                    foreach (var field in inputFields)
                    {
                        inputValues.Add(field.Value);
                    }
                    buttonConfig.action?.Invoke(inputValues);

                    // Check if the window should be closed
                    if (buttonConfig.shouldCloseCondition == null || buttonConfig.shouldCloseCondition(inputValues))
                    {
                        tcs?.TrySetResult(inputValues);
                        customTcs?.TrySetResult(inputValues);
                        Close();
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}