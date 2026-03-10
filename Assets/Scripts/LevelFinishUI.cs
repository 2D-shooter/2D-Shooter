using UnityEngine;
using TMPro;
using TopDown.Systems;

namespace TopDown.UI
{
    public class LevelFinishUI : MonoBehaviour
    {
        [Header("UI Panels")]
        [SerializeField] private GameObject finishPanel; // The Canvas group/Panel

        [Header("Text Displays")]
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI summaryText;

        private void Awake()
        {
            if (finishPanel != null) finishPanel.SetActive(false);
        }

        public void ShowFinishScreen()
        {
            finishPanel.SetActive(true);
            titleText.text = "LEVEL DONE";

            if (ObjectiveManager.Instance != null)
            {
                summaryText.text = ObjectiveManager.Instance.GetObjectiveSummary();
            }

            // Freeze the game time
            Time.timeScale = 0f;
        }

        public void RestartLevel()
        {
            Time.timeScale = 1f;
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}