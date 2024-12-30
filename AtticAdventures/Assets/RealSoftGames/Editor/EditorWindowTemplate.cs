using UnityEditor;
using UnityEngine;
using static RealSoftGames.Utilities;

namespace RealSoftGames
{
    public class EditorWindowTemplate : EditorWindow
    {
        protected TaskManager taskManager;
        protected Vector2 NavigationScrollPosition, MainBodyScrollPosition;
        protected float padding = 5f;
        protected float NavigationPanelWidth = 200f;
        private static Rect navRect;
        private static Rect mainBodyRect;

        public static Rect NavRect => navRect;
        public static Rect MainBodyRect => mainBodyRect;

        protected virtual GUILayoutOption[] NavBarWidthOptions
        {
            get => new GUILayoutOption[]
            {
                GUILayout.Width(NavigationPanelWidth - (padding * 8)),
                GUILayout.Height(30f)
            };
        }

        //[MenuItem("Tools/RealSoftGames/EditorWindowTemplate")]
        public static void ShowWindow()
        {
            GetWindow<EditorWindowTemplate>("RealSoft Games");
        }

        protected virtual void OnEnable()
        {
            if (taskManager == null) taskManager = new TaskManager();
            EditorApplication.update = null;
            EditorApplication.update += Update;
        }

        protected virtual void Update()
        {
            Repaint();
            taskManager.ProcessTaskQueue();
        }

        protected virtual void OnGUI()
        {
            // Start the horizontal layout that will contain the navigation and the main content.
            GUILayout.BeginHorizontal();
            DrawNavigationPanel();

            DrawMainBody();
            // End the horizontal layout that includes both the navigation and main content.
            GUILayout.EndHorizontal();
        }

        protected virtual void OnDisable()
        {
            EditorApplication.update -= Update;
        }

        protected virtual void OnDestroy()
        {
        }

        private void DrawNavigationPanel()
        {
            navRect = Utilities.EditorGUILayoutBeginContentZone(padding, padding,
                NavigationPanelWidth, position.height,
                padding,
                ref NavigationScrollPosition);

            EditorGUILAyoutLeftPanel();

            Utilities.EditorGUILayoutEndContentZone();
        }

        private void DrawMainBody()
        {
            mainBodyRect = Utilities.EditorGUILayoutBeginContentZone(
               NavigationPanelWidth + padding,
               padding,
               position.width - (NavigationPanelWidth + padding),
               position.height,
               padding,
               ref MainBodyScrollPosition);

            EditorGUILAyoutMainBody();

            Utilities.EditorGUILayoutEndContentZone();
        }

        /// <summary>
        /// Navigation Bar Content Drawer
        /// </summary>
        protected virtual void EditorGUILAyoutLeftPanel()
        {
        }

        /// <summary>
        /// Main Body Content Drawer
        /// </summary>
        protected virtual void EditorGUILAyoutMainBody()
        {
        }
    }
}