//using UnityEngine;

//public class BreakableObject : MonoBehaviour, IDamageable
//{
//    [SerializeField] private float maxHealth = 3f;
//    private float currentHealth;

//    private void Awake()
//    {
//        currentHealth = maxHealth;
//    }

//    public void TakeDamage(float damage)
//    {
//        currentHealth -= damage;
//        Debug.Log($"{gameObject.name} took damage! Health: {currentHealth}");

//        if (currentHealth <= 0)
//        {
//            Break();
//        }
//    }

//    private void Break()
//    {
//        // You can add particle effects or sound here later!
//        Destroy(gameObject);
//    }
//}

using UnityEngine;

public class BreakableObject : MonoBehaviour, IDamageable
{
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

        // Optional: Add a hit effect here
        if (currentHealth <= 0)
        {
            Break();
        }
    }

    private void Break()
    {
        // For now, it just disappears. 
        // Later, you can Instantiate "debris" particles here.
        Destroy(gameObject);
    }
}