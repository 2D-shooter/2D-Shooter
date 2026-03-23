
using UnityEngine;
using UniRx;
using TopDown.Systems;

namespace TopDown.Core
{
    public class Health : MonoBehaviour, IDamageable
    {
        public enum EntityType { Player, Enemy, Box, Boss }
        [SerializeField] public EntityType entityType;
        [SerializeField] private float maxHealth = 100f;
        public float MaxHealth => maxHealth;

        [SerializeField] private GameObject deathScreen;
        public FloatReactiveProperty currentHealth = new FloatReactiveProperty();

        private void Awake() => currentHealth.Value = maxHealth;

        public void TakeDamage(float damage)
        {
            currentHealth.Value -= damage;
            if (currentHealth.Value <= 0) Die();
        }

        private void Die()
        {
            // Notify the UI
            ObjectiveManager.Instance?.OnEntityResourceCheck(entityType);

            // Look for Level 1
            var level1 = Object.FindFirstObjectByType<LevelOneController>();
            if (level1 != null) level1.NotifyEnemyDeath(entityType);

            // Look for Level 2
            var level2 = Object.FindFirstObjectByType<LevelTwoController>();
            if (level2 != null) level2.NotifyEnemyDeath(entityType);

            // Handle the actual death/removal of the object
            if (entityType == EntityType.Player)
            {
                if (deathScreen != null) deathScreen.SetActive(true);
                if (TryGetComponent<SpriteRenderer>(out var r)) r.enabled = false;
                if (TryGetComponent<Collider2D>(out var c)) c.enabled = false;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}