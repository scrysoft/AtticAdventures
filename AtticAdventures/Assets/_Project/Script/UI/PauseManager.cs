using AtticAdventures.Core;
using AtticAdventures.SceneManagement;
using Rewired;
using UnityEngine;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{
    public UnityEvent onPause;
    public UnityEvent onResume;
    private bool isPaused = false;

    private Player rewiredPlayer;
    private int playerId = 0;

    void Awake()
    {
        rewiredPlayer = ReInput.players.GetPlayer(playerId);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") || rewiredPlayer.GetButtonDown("Cancel"))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        onPause?.Invoke();
    }

    public void PauseGameWithoutEvent()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        onResume?.Invoke();
    }

    public void UnpauseGame()
    {
        ResumeGame();
    }

    public void ResumeGameWithoutEvent()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    public async void RestartLevel(int index)
    {
        SceneLoader sceneLoader = FindAnyObjectByType<SceneLoader>();
        int spawnIndex = GameManager.Instance.GetSpawnPointIndex();
        ResumeGame();
        await sceneLoader.LoadSceneGroup(index);
    }
}
