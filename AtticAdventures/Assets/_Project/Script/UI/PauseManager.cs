using AtticAdventures.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public UnityEvent onPause;
    public UnityEvent onResume;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
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
