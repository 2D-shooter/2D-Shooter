

using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopDown.Systems
{
    public class SceneHandler : MonoBehaviour
    {
        // Must be PUBLIC to show up in the Button menu!
        public void LoadNextLevel()
        {
            Time.timeScale = 1f;
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }

        // Must be PUBLIC!
        public void RestartLevel()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Must be PUBLIC!
        public void LoadMainMenu()
        {
            Time.timeScale = 1f;
            // Ensure your scene is actually named "MainMenu" in the Project folder
            SceneManager.LoadScene(0);
        }

        // Must be PUBLIC!
        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("Game Exited");
        }
    }
}