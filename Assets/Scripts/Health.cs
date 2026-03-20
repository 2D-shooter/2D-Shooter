//////////////////////////using UnityEngine;
//////////////////////////using UniRx;
//////////////////////////using TopDown.Systems;

//////////////////////////namespace TopDown.Core
//////////////////////////{
//////////////////////////    public class Health : MonoBehaviour, IDamageable
//////////////////////////    {
//////////////////////////        public enum EntityType { Player, Enemy, Box }

//////////////////////////        [Header("Entity Settings")]
//////////////////////////        public EntityType entityType = EntityType.Enemy;
//////////////////////////        [SerializeField] private float maxHealth = 100f;

//////////////////////////        // ReactiveProperty lets the UI "watch" this value automatically
//////////////////////////        public FloatReactiveProperty currentHealth = new FloatReactiveProperty();
//////////////////////////        public float MaxHealth => maxHealth;

//////////////////////////        private bool isDead = false;

//////////////////////////        private void Awake()
//////////////////////////        {
//////////////////////////            currentHealth.Value = maxHealth;
//////////////////////////        }

//////////////////////////        public void TakeDamage(float damage)
//////////////////////////        {
//////////////////////////            if (isDead) return;

//////////////////////////            currentHealth.Value -= damage;
//////////////////////////            Debug.Log($"{gameObject.name} HP: {currentHealth.Value}");

//////////////////////////            if (currentHealth.Value <= 0)
//////////////////////////            {
//////////////////////////                Die();
//////////////////////////            }
//////////////////////////        }

//////////////////////////        private void Die()
//////////////////////////        {
//////////////////////////            if (isDead) return;
//////////////////////////            isDead = true;

//////////////////////////            // Handle Objective Progress
//////////////////////////            if (ObjectiveManager.Instance != null)
//////////////////////////            {
//////////////////////////                if (entityType == EntityType.Enemy) ObjectiveManager.Instance.RegisterEnemyKilled();
//////////////////////////                if (entityType == EntityType.Box) ObjectiveManager.Instance.RegisterBoxDestroyed();
//////////////////////////            }

//////////////////////////            // Handle Death Events
//////////////////////////            if (entityType == EntityType.Player)
//////////////////////////            {
//////////////////////////                // Find the GameOver UI and show it
//////////////////////////                Object.FindFirstObjectByType<TopDown.UI.GameOverUI>()?.ShowDeathScreen();
//////////////////////////                gameObject.SetActive(false);
//////////////////////////            }
//////////////////////////            else
//////////////////////////            {
//////////////////////////                Destroy(gameObject);
//////////////////////////            }
//////////////////////////        }
//////////////////////////    }
//////////////////////////}

////////////////////////using UnityEngine;
////////////////////////using UniRx;
////////////////////////using TopDown.Systems;

////////////////////////namespace TopDown.Core
////////////////////////{
////////////////////////    public class Health : MonoBehaviour, IDamageable
////////////////////////    {
////////////////////////        public enum EntityType { Player, Enemy, Box }

////////////////////////        [Header("Entity Settings")]
////////////////////////        public EntityType entityType = EntityType.Enemy;
////////////////////////        [SerializeField] private float maxHealth = 100f;

////////////////////////        public FloatReactiveProperty currentHealth = new FloatReactiveProperty();
////////////////////////        public float MaxHealth => maxHealth;

////////////////////////        private bool isDead = false;

////////////////////////        private void Awake()
////////////////////////        {
////////////////////////            currentHealth.Value = maxHealth;
////////////////////////        }

////////////////////////        public void TakeDamage(float damage)
////////////////////////        {
////////////////////////            if (isDead) return;

////////////////////////            currentHealth.Value -= damage;
////////////////////////            Debug.Log($"{gameObject.name} took {damage} damage. Remaining: {currentHealth.Value}");

////////////////////////            if (currentHealth.Value <= 0)
////////////////////////            {
////////////////////////                Die();
////////////////////////            }
////////////////////////        }

////////////////////////        private void Die()
////////////////////////        {
////////////////////////            if (isDead) return;
////////////////////////            isDead = true;

////////////////////////            if (ObjectiveManager.Instance != null)
////////////////////////            {
////////////////////////                if (entityType == EntityType.Enemy) ObjectiveManager.Instance.RegisterEnemyKilled();
////////////////////////                if (entityType == EntityType.Box) ObjectiveManager.Instance.RegisterBoxDestroyed();
////////////////////////            }

////////////////////////            if (entityType == EntityType.Player)
////////////////////////            {
////////////////////////                // Trigger the GameOverUI
////////////////////////                Object.FindFirstObjectByType<TopDown.UI.GameOverUI>()?.ShowDeathScreen();
////////////////////////                gameObject.SetActive(false);
////////////////////////            }
////////////////////////            else
////////////////////////            {
////////////////////////                Destroy(gameObject);
////////////////////////            }
////////////////////////        }
////////////////////////    }
////////////////////////}

//////////////////////using UnityEngine;
//////////////////////using UniRx;
//////////////////////using TopDown.Systems;

//////////////////////namespace TopDown.Core
//////////////////////{
//////////////////////    public class Health : MonoBehaviour, IDamageable
//////////////////////    {
//////////////////////        public enum EntityType { Player, Enemy, Box }

//////////////////////        [Header("Entity Settings")]
//////////////////////        public EntityType entityType = EntityType.Enemy;
//////////////////////        [SerializeField] private float maxHealth = 100f;

//////////////////////        public FloatReactiveProperty currentHealth = new FloatReactiveProperty();
//////////////////////        public float MaxHealth => maxHealth;

//////////////////////        private bool isDead = false;

//////////////////////        private void Awake()
//////////////////////        {
//////////////////////            currentHealth.Value = maxHealth;
//////////////////////        }

//////////////////////        public void TakeDamage(float damage)
//////////////////////        {
//////////////////////            if (isDead) return;

//////////////////////            currentHealth.Value -= damage;
//////////////////////            Debug.Log($"{gameObject.name} took {damage} damage. Remaining: {currentHealth.Value}");

//////////////////////            if (currentHealth.Value <= 0)
//////////////////////            {
//////////////////////                Die();
//////////////////////            }
//////////////////////        }

//////////////////////        private void Die()
//////////////////////        {
//////////////////////            if (isDead) return;
//////////////////////            isDead = true;

//////////////////////            if (ObjectiveManager.Instance != null)
//////////////////////            {
//////////////////////                if (entityType == EntityType.Enemy) ObjectiveManager.Instance.RegisterEnemyKilled();
//////////////////////                if (entityType == EntityType.Box) ObjectiveManager.Instance.RegisterBoxDestroyed();
//////////////////////            }

//////////////////////            if (entityType == EntityType.Player)
//////////////////////            {
//////////////////////                Object.FindFirstObjectByType<TopDown.UI.GameOverUI>()?.ShowDeathScreen();
//////////////////////                gameObject.SetActive(false);
//////////////////////            }
//////////////////////            else
//////////////////////            {
//////////////////////                Destroy(gameObject);
//////////////////////            }
//////////////////////        }
//////////////////////    }
//////////////////////}

////////////////////using UnityEngine;
////////////////////using UniRx;
////////////////////using TopDown.Systems;

////////////////////namespace TopDown.Core
////////////////////{
////////////////////    public class Health : MonoBehaviour, IDamageable
////////////////////    {
////////////////////        public enum EntityType { Player, Enemy, Box }

////////////////////        [Header("Entity Settings")]
////////////////////        public EntityType entityType = EntityType.Enemy;
////////////////////        [SerializeField] private float maxHealth = 100f;

////////////////////        public FloatReactiveProperty currentHealth = new FloatReactiveProperty();
////////////////////        public float MaxHealth => maxHealth;

////////////////////        private bool isDead = false;

////////////////////        private void Awake()
////////////////////        {
////////////////////            currentHealth.Value = maxHealth;
////////////////////        }

////////////////////        public void TakeDamage(float damage)
////////////////////        {
////////////////////            if (isDead) return;

////////////////////            currentHealth.Value -= damage;
////////////////////            Debug.Log($"{gameObject.name} took {damage} damage. Remaining: {currentHealth.Value}");

////////////////////            if (currentHealth.Value <= 0)
////////////////////            {
////////////////////                Die();
////////////////////            }
////////////////////        }

////////////////////        private void Die()
////////////////////        {
////////////////////            if (isDead) return;
////////////////////            isDead = true;

////////////////////            // Notify the ObjectiveManager if applicable
////////////////////            if (ObjectiveManager.Instance != null)
////////////////////            {
////////////////////                if (entityType == EntityType.Enemy) ObjectiveManager.Instance.RegisterEnemyKilled();
////////////////////                if (entityType == EntityType.Box) ObjectiveManager.Instance.RegisterBoxDestroyed();
////////////////////            }

////////////////////            // Handle unique behavior for Player death
////////////////////            if (entityType == EntityType.Player)
////////////////////            {
////////////////////                // This triggers the GameOverUI found in the scene
////////////////////                Object.FindFirstObjectByType<TopDown.UI.GameOverUI>()?.ShowDeathScreen();
////////////////////                gameObject.SetActive(false);
////////////////////            }
////////////////////            else
////////////////////            {
////////////////////                // Normal objects or enemies just get destroyed
////////////////////                Destroy(gameObject);
////////////////////            }
////////////////////        }
////////////////////    }
////////////////////}

//////////////////using UnityEngine;
//////////////////using UniRx;
//////////////////using TopDown.Systems;

//////////////////namespace TopDown.Core
//////////////////{
//////////////////    public class Health : MonoBehaviour, IDamageable
//////////////////    {
//////////////////        public enum EntityType { Player, Enemy, Box, Boss } // Added Boss
//////////////////        [SerializeField] private EntityType entityType;
//////////////////        public EntityType Type => entityType;

//////////////////        [SerializeField] private float maxHealth = 100f;
//////////////////        public float MaxHealth => maxHealth;

//////////////////        public FloatReactiveProperty currentHealth = new FloatReactiveProperty();

//////////////////        private void Awake()
//////////////////        {
//////////////////            currentHealth.Value = maxHealth;
//////////////////        }

//////////////////        public void TakeDamage(float damage)
//////////////////        {
//////////////////            currentHealth.Value -= damage;
//////////////////            if (currentHealth.Value <= 0)
//////////////////            {
//////////////////                Die();
//////////////////            }
//////////////////        }

//////////////////        private void Die()
//////////////////        {
//////////////////            // Notify ObjectiveManager
//////////////////            ObjectiveManager.Instance?.OnEntityResourceCheck(entityType);

//////////////////            // UPDATED: Look for LevelOneController instead of LevelController
//////////////////            FindObjectOfType<LevelOneController>()?.NotifyEnemyDeath(entityType);

//////////////////            Destroy(gameObject);
//////////////////        }
//////////////////    }
//////////////////}

////////////////using UnityEngine;
////////////////using UniRx;
////////////////using TopDown.Systems;

////////////////namespace TopDown.Core
////////////////{
////////////////    public class Health : MonoBehaviour, IDamageable
////////////////    {
////////////////        public enum EntityType { Player, Enemy, Box, Boss }

////////////////        [Header("Entity Settings")]
////////////////        [SerializeField] public EntityType entityType; // CHANGED TO PUBLIC to fix WeaponController error

////////////////        [SerializeField] private float maxHealth = 100f;
////////////////        public float MaxHealth => maxHealth;

////////////////        public FloatReactiveProperty currentHealth = new FloatReactiveProperty();

////////////////        private void Awake()
////////////////        {
////////////////            currentHealth.Value = maxHealth;
////////////////        }

////////////////        public void TakeDamage(float damage)
////////////////        {
////////////////            currentHealth.Value -= damage;
////////////////            if (currentHealth.Value <= 0)
////////////////            {
////////////////                Die();
////////////////            }
////////////////        }

////////////////        private void Die()
////////////////        {
////////////////            // 1. Notify ObjectiveManager for the UI and the final Exit spawn
////////////////            ObjectiveManager.Instance?.OnEntityResourceCheck(entityType);

////////////////            // 2. Notify LevelOneController to track the 8 kills and spawn the Boss
////////////////            FindObjectOfType<LevelOneController>()?.NotifyEnemyDeath(entityType);

////////////////            Destroy(gameObject);
////////////////        }
////////////////    }
////////////////}

//////////////using UnityEngine;
//////////////using UniRx;
//////////////using TopDown.Systems;

//////////////namespace TopDown.Core
//////////////{
//////////////    public class Health : MonoBehaviour, IDamageable
//////////////    {
//////////////        public enum EntityType { Player, Enemy, Box, Boss }

//////////////        [Header("Entity Settings")]
//////////////        [SerializeField] public EntityType entityType;

//////////////        [SerializeField] private float maxHealth = 100f;
//////////////        public float MaxHealth => maxHealth;

//////////////        public FloatReactiveProperty currentHealth = new FloatReactiveProperty();

//////////////        private void Awake()
//////////////        {
//////////////            currentHealth.Value = maxHealth;
//////////////        }

//////////////        public void TakeDamage(float damage)
//////////////        {
//////////////            currentHealth.Value -= damage;
//////////////            if (currentHealth.Value <= 0)
//////////////            {
//////////////                Die();
//////////////            }
//////////////        }

//////////////        private void Die()
//////////////        {
//////////////            // 1. Notify ObjectiveManager
//////////////            ObjectiveManager.Instance?.OnEntityResourceCheck(entityType);

//////////////            // 2. FIXED: Using the modern FindFirstObjectByType to remove the warning
//////////////            Object.FindFirstObjectByType<LevelOneController>()?.NotifyEnemyDeath(entityType);

//////////////            Destroy(gameObject);
//////////////        }
//////////////    }
//////////////}

////////////using UnityEngine;
////////////using UniRx;
////////////using TopDown.Systems;

////////////namespace TopDown.Core
////////////{
////////////    public class Health : MonoBehaviour, IDamageable
////////////    {
////////////        public enum EntityType { Player, Enemy, Box, Boss }

////////////        [Header("Entity Settings")]
////////////        [SerializeField] public EntityType entityType;

////////////        [SerializeField] private float maxHealth = 100f;
////////////        public float MaxHealth => maxHealth;

////////////        public FloatReactiveProperty currentHealth = new FloatReactiveProperty();

////////////        private void Awake()
////////////        {
////////////            currentHealth.Value = maxHealth;
////////////        }

////////////        public void TakeDamage(float damage)
////////////        {
////////////            currentHealth.Value -= damage;
////////////            if (currentHealth.Value <= 0)
////////////            {
////////////                Die();
////////////            }
////////////        }

////////////        private void Die()
////////////        {
////////////            // 1. Update the Objectives (UI and Win Condition)
////////////            ObjectiveManager.Instance?.OnEntityResourceCheck(entityType);

////////////            // 2. Notify the Level Controller (To open the Boss Door)
////////////            Object.FindFirstObjectByType<LevelOneController>()?.NotifyEnemyDeath(entityType);

////////////            // 3. Handle the actual destruction/disabling
////////////            if (entityType == EntityType.Player)
////////////            {
////////////                // If we DESTROY the player, the camera crashes.
////////////                // Instead, we deactivate them.
////////////                gameObject.SetActive(false);

////////////                // Trigger your Death Screen here!
////////////                // DeathScreenManager.Instance?.ShowDeathScreen();
////////////                Debug.Log("PLAYER DIED: Showing Death Screen...");
////////////            }
////////////            else
////////////            {
////////////                // Enemies, Bosses, and Boxes can be destroyed safely.
////////////                Destroy(gameObject);
////////////            }
////////////        }
////////////    }
////////////}

//////////using UnityEngine;
//////////using UniRx;
//////////using TopDown.Systems;

//////////namespace TopDown.Core
//////////{
//////////    public class Health : MonoBehaviour, IDamageable
//////////    {
//////////        public enum EntityType { Player, Enemy, Box, Boss }
//////////        [SerializeField] public EntityType entityType;
//////////        [SerializeField] private float maxHealth = 100f;

//////////        public FloatReactiveProperty currentHealth = new FloatReactiveProperty();

//////////        private void Awake() => currentHealth.Value = maxHealth;

//////////        public void TakeDamage(float damage)
//////////        {
//////////            currentHealth.Value -= damage;
//////////            if (currentHealth.Value <= 0) Die();
//////////        }

//////////        private void Die()
//////////        {
//////////            ObjectiveManager.Instance?.OnEntityResourceCheck(entityType);
//////////            Object.FindFirstObjectByType<LevelOneController>()?.NotifyEnemyDeath(entityType);

//////////            if (entityType == EntityType.Player)
//////////            {
//////////                // Instead of destroying or deactivating, we just "Mute" the player.
//////////                // This keeps the Camera target valid!

//////////                // 1. Hide the visuals
//////////                if (TryGetComponent<SpriteRenderer>(out var renderer)) renderer.enabled = false;

//////////                // 2. Disable movement/shooting scripts (Adjust names to match your scripts)
//////////                // GetComponent<PlayerMovement>().enabled = false; 

//////////                Debug.Log("Player is 'Dead' but object remains for Camera/UI.");
//////////            }
//////////            else
//////////            {
//////////                Destroy(gameObject);
//////////            }
//////////        }
//////////    }
//////////}

////////using UnityEngine;
////////using UniRx;
////////using TopDown.Systems;

////////namespace TopDown.Core
////////{
////////    public class Health : MonoBehaviour, IDamageable
////////    {
////////        public enum EntityType { Player, Enemy, Box, Boss }

////////        [Header("Entity Settings")]
////////        [SerializeField] public EntityType entityType;

////////        [SerializeField] private float maxHealth = 100f;
////////        // This is the property your HealthBarUI.cs is looking for!
////////        public float MaxHealth => maxHealth;

////////        public FloatReactiveProperty currentHealth = new FloatReactiveProperty();

////////        private void Awake()
////////        {
////////            currentHealth.Value = maxHealth;
////////        }

////////        public void TakeDamage(float damage)
////////        {
////////            currentHealth.Value -= damage;
////////            if (currentHealth.Value <= 0)
////////            {
////////                Die();
////////            }
////////        }

////////        private void Die()
////////        {
////////            // 1. Update Objectives
////////            ObjectiveManager.Instance?.OnEntityResourceCheck(entityType);

////////            // 2. Notify Boss Door Logic
////////            Object.FindFirstObjectByType<LevelOneController>()?.NotifyEnemyDeath(entityType);

////////            if (entityType == EntityType.Player)
////////            {
////////                // To keep the camera from breaking, we don't Destroy(gameObject).
////////                // We just hide the player and disable their controls.
////////                if (TryGetComponent<SpriteRenderer>(out var renderer)) renderer.enabled = false;
////////                if (TryGetComponent<Collider2D>(out var col)) col.enabled = false;

////////                // Trigger your Death Screen here
////////                Debug.Log("Player 'Dead'. Camera target preserved.");
////////            }
////////            else
////////            {
////////                // Non-player objects can be safely destroyed
////////                Destroy(gameObject);
////////            }
////////        }
////////    }
////////}

//////using UnityEngine;
//////using UniRx;
//////using TopDown.Systems;

//////namespace TopDown.Core
//////{
//////    public class Health : MonoBehaviour, IDamageable
//////    {
//////        public enum EntityType { Player, Enemy, Box, Boss }
//////        [SerializeField] public EntityType entityType;
//////        [SerializeField] private float maxHealth = 100f;

//////        public float MaxHealth => maxHealth; // Restored for your HealthBarUI
//////        public FloatReactiveProperty currentHealth = new FloatReactiveProperty();

//////        private void Awake() => currentHealth.Value = maxHealth;

//////        public void TakeDamage(float damage)
//////        {
//////            currentHealth.Value -= damage;
//////            if (currentHealth.Value <= 0) Die();
//////        }

//////        private void Die()
//////        {
//////            ObjectiveManager.Instance?.OnEntityResourceCheck(entityType);
//////            Object.FindFirstObjectByType<LevelOneController>()?.NotifyEnemyDeath(entityType);

//////            if (entityType == EntityType.Player)
//////            {
//////                // STOP: Do NOT Destroy(gameObject). 
//////                // Disabling the object keeps the Camera target valid but "kills" the player.
//////                gameObject.SetActive(false);
//////                Debug.Log("Player Deactivated. Camera stays on corpse.");
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
////        public enum EntityType { Player, Enemy, Box, Boss }
////        [SerializeField] public EntityType entityType;

////        [SerializeField] private float maxHealth = 100f;
////        public float MaxHealth => maxHealth; // Restored for HealthBarUI

////        public FloatReactiveProperty currentHealth = new FloatReactiveProperty();

////        private void Awake() => currentHealth.Value = maxHealth;

////        public void TakeDamage(float damage)
////        {
////            currentHealth.Value -= damage;
////            if (currentHealth.Value <= 0) Die();
////        }

////        private void Die()
////        {
////            ObjectiveManager.Instance?.OnEntityResourceCheck(entityType);
////            Object.FindFirstObjectByType<LevelOneController>()?.NotifyEnemyDeath(entityType);

////            if (entityType == EntityType.Player)
////            {
////                // Deactivate the player so the Camera still has a valid (but dead) target
////                gameObject.SetActive(false);
////                Debug.Log("Player Dead. Camera remains active.");
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
//        public enum EntityType { Player, Enemy, Box, Boss }

//        [Header("Entity Settings")]
//        [SerializeField] public EntityType entityType;
//        [SerializeField] private float maxHealth = 100f;
//        public float MaxHealth => maxHealth;

//        [Header("Death Settings")]
//        [SerializeField] private GameObject deathScreen; // NEW: Drag your Death Screen UI here

//        public FloatReactiveProperty currentHealth = new FloatReactiveProperty();

//        private void Awake()
//        {
//            currentHealth.Value = maxHealth;
//        }

//        public void TakeDamage(float damage)
//        {
//            currentHealth.Value -= damage;
//            if (currentHealth.Value <= 0)
//            {
//                Die();
//            }
//        }

//        private void Die()
//        {
//            // 1. Update Objectives (The UI text)
//            ObjectiveManager.Instance?.OnEntityResourceCheck(entityType);

//            // 2. THE SIGNAL: This finds the Controller and tells it an enemy died
//            LevelOneController controller = Object.FindFirstObjectByType<LevelOneController>();
//            if (controller != null)
//            {
//                controller.NotifyEnemyDeath(entityType);
//            }
//            else
//            {
//                Debug.LogError("HEALTH: Could not find a LevelOneController in the scene!");
//            }

//            // 3. Handle Death
//            if (entityType == EntityType.Player)
//            {
//                if (deathScreen != null) deathScreen.SetActive(true);
//                if (TryGetComponent<SpriteRenderer>(out var r)) r.enabled = false;
//                if (TryGetComponent<Collider2D>(out var c)) c.enabled = false;
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
            ObjectiveManager.Instance?.OnEntityResourceCheck(entityType);

            // THIS SEARCHES FOR THE CONTROLLER
            LevelOneController controller = Object.FindFirstObjectByType<LevelOneController>();

            if (controller != null)
            {
                Debug.Log($"<color=yellow>Health: Enemy died. Sending signal to {controller.name}</color>");
                controller.NotifyEnemyDeath(entityType);
            }
            else
            {
                Debug.LogError("HEALTH ERROR: Could not find LevelOneController in your scene!");
            }

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