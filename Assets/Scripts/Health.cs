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
            if (currentHealth.Value <= 0) return;

            currentHealth.Value -= damage;

            // Grunt-äänet vain jos jää henkiin
            if (currentHealth.Value > 0)
            {
                if (entityType == EntityType.Player)
                    SoundFXManager.Instance?.PlayPlayerGrunt();
                else if (entityType == EntityType.Enemy || entityType == EntityType.Boss)
                    SoundFXManager.Instance?.PlayEnemyGrunt(transform.position);

                // Vihollinen reagoi vain itseensä
                if (entityType != EntityType.Player)
                {
                    var villain = GetComponent<VillainMovement>();
                    var player = GameObject.FindGameObjectWithTag("Player");
                    if (villain != null && player != null)
                        villain.SetTarget(player.transform);
                }
            }

            if (currentHealth.Value <= 0) Die();
        }

        private void Die()
        {
            // Death-äänet
            if (entityType == EntityType.Player)
                SoundFXManager.Instance?.PlayPlayerDeath();
            else if (entityType == EntityType.Enemy || entityType == EntityType.Boss)
                SoundFXManager.Instance?.PlayEnemyDeath(transform.position);

            // Notify UI ja level controllerit
            ObjectiveManager.Instance?.OnEntityResourceCheck(entityType);

            var level1 = Object.FindFirstObjectByType<LevelOneController>();
            if (level1 != null) level1.NotifyEnemyDeath(entityType);

            var level2 = Object.FindFirstObjectByType<LevelTwoController>();
            if (level2 != null) level2.NotifyEnemyDeath(entityType);

            // Pelaajan kuolema
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