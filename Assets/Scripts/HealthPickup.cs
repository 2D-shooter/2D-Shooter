using UnityEngine;
using TopDown.Core;

namespace TopDown.Items
{
    public class HealthPickup : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float healAmount = 30f;
        [SerializeField] private GameObject pickupEffect; // Optional: Particle prefab

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Check if the object touching the box is the Player
            if (collision.CompareTag("Player"))
            {
                Health playerHealth = collision.GetComponent<Health>();

                if (playerHealth != null)
                {
                    // Check if player actually needs healing
                    if (playerHealth.currentHealth.Value < playerHealth.MaxHealth)
                    {
                        ApplyHealing(playerHealth);
                    }
                }
            }
        }

        private void ApplyHealing(Health hp)
        {
            // Calculate new health but Clamp it so it never exceeds MaxHealth
            float newHealth = hp.currentHealth.Value + healAmount;
            hp.currentHealth.Value = Mathf.Clamp(newHealth, 0, hp.MaxHealth);

            Debug.Log($"Healed! New Health: {hp.currentHealth.Value}");

            // Play sound if your friend's manager has a heal sound
            // SoundFXManager.Instance?.PlayHeal(); 

            // Spawn effect if you assigned one
            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }

            // Destroy the health box
            Destroy(gameObject);
        }
    }
}