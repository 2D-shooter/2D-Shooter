

using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TopDown.Core;

namespace TopDown.UI
{
    public class HealthBarUI : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] private Health playerHealth; // Drag your Player here
        [SerializeField] private Image fillImage;

        private void Start()
        {
            if (playerHealth == null || fillImage == null) return;

            // Automatically update the bar whenever currentHealth changes
            playerHealth.currentHealth
                .Subscribe(hp =>
                {
                    fillImage.fillAmount = hp / playerHealth.MaxHealth;
                })
                .AddTo(this);
        }
    }
}