using AtticAdventures.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Bootstrapper : PersistentSingleton<Bootstrapper>
{
    static readonly int sceneIndex = 0;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static async void Init()
    {
        Debug.Log("Bootstrapper...");
        /*
#if UNITY_EDITOR
        EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[sceneIndex].path);
#endif
        */
        await SceneManager.LoadSceneAsync("Bootstrapper").AsTask();
    }
}