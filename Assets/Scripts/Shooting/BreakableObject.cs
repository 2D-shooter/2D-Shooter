
//using UnityEngine;

//public class BreakableObject : MonoBehaviour, IDamageable
//{
//    [Header("Health Settings")]
//    [SerializeField] private float maxHealth = 3f;
//    private float currentHealth;

//    private void Awake()
//    {
//        currentHealth = maxHealth;
//    }

//    public void TakeDamage(float damage)
//    {
//        currentHealth -= damage;

//        // Optional: Add a hit effect here
//        if (currentHealth <= 0)
//        {
//            Break();
//        }
//    }

//    private void Break()
//    {

//        if (TopDown.Systems.ObjectiveManager.Instance != null)
//        {
//            TopDown.Systems.ObjectiveManager.Instance.RegisterObjectDestroyed();
//        }
//        // For now, it just disappears. 
//        // Later, you can Instantiate "debris" particles here.
//        Destroy(gameObject);
//    }
//}

using UnityEngine;
using TopDown.Systems; // Added this to make referencing ObjectiveManager cleaner

public class BreakableObject : MonoBehaviour, IDamageable
{
    // This enum allows you to pick the type in the Inspector
    public enum ObjectType { Box, Enemy }

    [Header("Objective Settings")]
    [SerializeField] private ObjectType objectType = ObjectType.Box;

    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 3f;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Break();
        }
    }

    private void Break()
    {
        // Check if the ObjectiveManager exists in the scene
        if (ObjectiveManager.Instance != null)
        {
            // Direct the signal to the correct counter based on the dropdown choice
            if (objectType == ObjectType.Box)
            {
                ObjectiveManager.Instance.RegisterBoxDestroyed();
            }
            else if (objectType == ObjectType.Enemy)
            {
                ObjectiveManager.Instance.RegisterEnemyKilled();
            }
        }

        // Destroy the object
        Destroy(gameObject);
    }
}