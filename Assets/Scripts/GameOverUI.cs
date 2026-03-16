//using UnityEngine;
//using UnityEngine.SceneManagement;

//namespace TopDown.UI
//{
//    public class GameOverUI : MonoBehaviour
//    {
//        [SerializeField] private GameObject deathPanel;

//        private void Awake()
//        {
//            if (deathPanel != null) deathPanel.SetActive(false);
//        }

//        public void ShowDeathScreen()
//        {
//            deathPanel.SetActive(true);
//            Time.timeScale = 0f; // Pause the game
//        }

//        public void RestartLevel()
//        {
//            Time.timeScale = 1f;
//            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//        }
//    }
//}

using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopDown.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [Header("UI Panels")]
        [SerializeField] private GameObject deathPanel;

        private void Awake()
        {
            if (deathPanel != null) deathPanel.SetActive(false);
        }

        public void ShowDeathScreen()
        {
            if (deathPanel != null)
            {
                deathPanel.SetActive(true);
                Time.timeScale = 0f; // Freeze game
                Cursor.visible = true; // Show cursor
            }
        }

        public void RestartLevel()
        {
            Time.timeScale = 1f; // Resume time
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}