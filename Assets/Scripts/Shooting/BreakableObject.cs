

using UnityEngine;
using TopDown.Systems;
using TopDown.Core;

namespace TopDown.World
{
    public class BreakableObject : MonoBehaviour, IDamageable
    {
        [Header("Settings")]
        [SerializeField] private float maxHealth = 20f;
        [SerializeField] private GameObject breakEffect;

        private float currentHealth;
        private bool isBroken = false;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (isBroken) return;

            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Break();
            }
        }

        private void Break()
        {
            isBroken = true;

            // Updated to the new Unified Method
            if (ObjectiveManager.Instance != null)
            {
                ObjectiveManager.Instance.OnEntityResourceCheck(Health.EntityType.Box);
            }

            if (breakEffect != null)
            {
                Instantiate(breakEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}