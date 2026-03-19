using UnityEngine;
using UnityEngine.InputSystem; // 🔥 tärkeä
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject optionsPanel;

    private bool isPaused = false;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (optionsPanel.activeSelf)
            {
                ShowPause();
            }
            else if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ShowOptions()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void ShowPause()
    {
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void BackToMenu()
{
    Time.timeScale = 1f;
    SceneManager.LoadScene("MainMenu");
}
}