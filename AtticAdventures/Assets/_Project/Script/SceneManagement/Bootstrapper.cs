using AtticAdventures.Utilities;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;


public class Bootstrapper : PersistentSingleton<Bootstrapper>
{
    static readonly int sceneIndex = 0;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static async void Init()
    {
        Debug.Log("Bootstrapper...");
#if UNITY_EDITOR
        EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[sceneIndex].path);
#endif
        await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Bootstrapper").AsTask();
    }
}