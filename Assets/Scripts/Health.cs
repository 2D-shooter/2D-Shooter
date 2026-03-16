//////using UnityEngine;
//////using UniRx;
//////using TopDown.Systems;

//////namespace TopDown.Core
//////{
//////    public class Health : MonoBehaviour, IDamageable
//////    {
//////        public enum EntityType { Player, Enemy, Box }

//////        [Header("Entity Settings")]
//////        public EntityType entityType = EntityType.Enemy;
//////        [SerializeField] private float maxHealth = 100f;

//////        // ReactiveProperty lets the UI "watch" this value automatically
//////        public FloatReactiveProperty currentHealth = new FloatReactiveProperty();
//////        public float MaxHealth => maxHealth;

//////        private bool isDead = false;

//////        private void Awake()
//////        {
//////            currentHealth.Value = maxHealth;
//////        }

//////        public void TakeDamage(float damage)
//////        {
//////            if (isDead) return;

//////            currentHealth.Value -= damage;
//////            Debug.Log($"{gameObject.name} HP: {currentHealth.Value}");

//////            if (currentHealth.Value <= 0)
//////            {
//////                Die();
//////            }
//////        }

//////        private void Die()
//////        {
//////            if (isDead) return;
//////            isDead = true;

//////            // Handle Objective Progress
//////            if (ObjectiveManager.Instance != null)
//////            {
//////                if (entityType == EntityType.Enemy) ObjectiveManager.Instance.RegisterEnemyKilled();
//////                if (entityType == EntityType.Box) ObjectiveManager.Instance.RegisterBoxDestroyed();
//////            }

//////            // Handle Death Events
//////            if (entityType == EntityType.Player)
//////            {
//////                // Find the GameOver UI and show it
//////                Object.FindFirstObjectByType<TopDown.UI.GameOverUI>()?.ShowDeathScreen();
//////                gameObject.SetActive(false);
//////            }
//////            else
//////            {
//////                Destroy(gameObject);
//////            }
//////        }
//////    }
//////}

////using UnityEngine;
////using UniRx;
////using TopDown.Systems;

////namespace TopDown.Core
////{
////    public class Health : MonoBehaviour, IDamageable
////    {
////        public enum EntityType { Player, Enemy, Box }

////        [Header("Entity Settings")]
////        public EntityType entityType = EntityType.Enemy;
////        [SerializeField] private float maxHealth = 100f;

////        public FloatReactiveProperty currentHealth = new FloatReactiveProperty();
////        public float MaxHealth => maxHealth;

////        private bool isDead = false;

////        private void Awake()
////        {
////            currentHealth.Value = maxHealth;
////        }

////        public void TakeDamage(float damage)
////        {
////            if (isDead) return;

////            currentHealth.Value -= damage;
////            Debug.Log($"{gameObject.name} took {damage} damage. Remaining: {currentHealth.Value}");

////            if (currentHealth.Value <= 0)
////            {
////                Die();
////            }
////        }

////        private void Die()
////        {
////            if (isDead) return;
////            isDead = true;

////            if (ObjectiveManager.Instance != null)
////            {
////                if (entityType == EntityType.Enemy) ObjectiveManager.Instance.RegisterEnemyKilled();
////                if (entityType == EntityType.Box) ObjectiveManager.Instance.RegisterBoxDestroyed();
////            }

////            if (entityType == EntityType.Player)
////            {
////                // Trigger the GameOverUI
////                Object.FindFirstObjectByType<TopDown.UI.GameOverUI>()?.ShowDeathScreen();
////                gameObject.SetActive(false);
////            }
////            else
////            {
////                Destroy(gameObject);
////            }
////        }
////    }
////}

//using UnityEngine;
//using UniRx;
//using TopDown.Systems;

//namespace TopDown.Core
//{
//    public class Health : MonoBehaviour, IDamageable
//    {
//        public enum EntityType { Player, Enemy, Box }

//        [Header("Entity Settings")]
//        public EntityType entityType = EntityType.Enemy;
//        [SerializeField] private float maxHealth = 100f;

//        public FloatReactiveProperty currentHealth = new FloatReactiveProperty();
//        public float MaxHealth => maxHealth;

//        private bool isDead = false;

//        private void Awake()
//        {
//            currentHealth.Value = maxHealth;
//        }

//        public void TakeDamage(float damage)
//        {
//            if (isDead) return;

//            currentHealth.Value -= damage;
//            Debug.Log($"{gameObject.name} took {damage} damage. Remaining: {currentHealth.Value}");

//            if (currentHealth.Value <= 0)
//            {
//                Die();
//            }
//        }

//        private void Die()
//        {
//            if (isDead) return;
//            isDead = true;

//            if (ObjectiveManager.Instance != null)
//            {
//                if (entityType == EntityType.Enemy) ObjectiveManager.Instance.RegisterEnemyKilled();
//                if (entityType == EntityType.Box) ObjectiveManager.Instance.RegisterBoxDestroyed();
//            }

//            if (entityType == EntityType.Player)
//            {
//                Object.FindFirstObjectByType<TopDown.UI.GameOverUI>()?.ShowDeathScreen();
//                gameObject.SetActive(false);
//            }
//            else
//            {
//                Destroy(gameObject);
//            }
//        }
//    }
//}

using UnityEngine;
using UniRx;
using TopDown.Systems;

namespace TopDown.Core
{
    public class Health : MonoBehaviour, IDamageable
    {
        public enum EntityType { Player, Enemy, Box }

        [Header("Entity Settings")]
        public EntityType entityType = EntityType.Enemy;
        [SerializeField] private float maxHealth = 100f;

        public FloatReactiveProperty currentHealth = new FloatReactiveProperty();
        public float MaxHealth => maxHealth;

        private bool isDead = false;

        private void Awake()
        {
            currentHealth.Value = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (isDead) return;

            currentHealth.Value -= damage;
            Debug.Log($"{gameObject.name} took {damage} damage. Remaining: {currentHealth.Value}");

            if (currentHealth.Value <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;

            // Notify the ObjectiveManager if applicable
            if (ObjectiveManager.Instance != null)
            {
                if (entityType == EntityType.Enemy) ObjectiveManager.Instance.RegisterEnemyKilled();
                if (entityType == EntityType.Box) ObjectiveManager.Instance.RegisterBoxDestroyed();
            }

            // Handle unique behavior for Player death
            if (entityType == EntityType.Player)
            {
                // This triggers the GameOverUI found in the scene
                Object.FindFirstObjectByType<TopDown.UI.GameOverUI>()?.ShowDeathScreen();
                gameObject.SetActive(false);
            }
            else
            {
                // Normal objects or enemies just get destroyed
                Destroy(gameObject);
            }
        }
    }
}