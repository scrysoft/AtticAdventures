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

    private void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        onPause?.Invoke();
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

    public async void RestartLevel(int index)
    {
        SceneLoader sceneLoader = FindAnyObjectByType<SceneLoader>();
        ResumeGame();
        await sceneLoader.LoadSceneGroup(index);
    }
}
