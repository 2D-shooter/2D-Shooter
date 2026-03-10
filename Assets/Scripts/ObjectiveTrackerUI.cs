using UnityEngine;
using TMPro;
using TopDown.Systems;

namespace TopDown.UI
{
    public class ObjectiveTrackerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI objectiveText;

        private void Start()
        {
            if (ObjectiveManager.Instance != null)
            {
                // Subscribe to the progress event
                ObjectiveManager.Instance.OnObjectiveProgressChanged += UpdateVisuals;
                UpdateVisuals(); // Initial update
            }
        }

        private void OnDestroy()
        {
            // Unsubscribe to prevent memory leaks
            if (ObjectiveManager.Instance != null)
            {
                ObjectiveManager.Instance.OnObjectiveProgressChanged -= UpdateVisuals;
            }
        }

        private void UpdateVisuals()
        {
            if (ObjectiveManager.Instance == null) return;

            // This pulls the string we already formatted in the Manager
            objectiveText.text = ObjectiveManager.Instance.GetObjectiveSummary();
        }
    }
}