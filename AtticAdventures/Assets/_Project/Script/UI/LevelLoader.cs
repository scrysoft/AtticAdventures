using AtticAdventures.Core;
using AtticAdventures.SceneManagement;
using UnityEngine;

namespace AtticAdventures.UI
{
    public class LevelLoader : MonoBehaviour
    {
        private SceneLoader sceneLoader = null;
        private int sceneIndex = 1;

        private void Start()
        {
            sceneLoader = FindObjectOfType<SceneLoader>();
        }

        public async void LoadLevelAtSpawnPoint(int spawnPointIndex)
        {
            GameManager.Instance.SetSpawnPointIndex(spawnPointIndex);
            Debug.Log("Spawnpoint Index set to " + spawnPointIndex);

            await sceneLoader.LoadSceneGroup(sceneIndex);
        }
    }
}
