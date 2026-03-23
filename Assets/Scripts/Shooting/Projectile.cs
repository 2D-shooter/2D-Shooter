
using UnityEngine;
using TopDown.Core;

namespace TopDown.Shooting
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float speed = 20f;
        [SerializeField] private float lifetime = 2f;
        [SerializeField] private float damage = 10f; // Default damage

        private Rigidbody2D body;
        private float lifeTimer;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        // Overload to allow weapons to set specific damage (11 or 16)
        public void SetDamage(float amount) => damage = amount;

        public void ShootBullet(Transform shootPoint)
        {
            lifeTimer = 0;
            transform.position = shootPoint.position;
            transform.rotation = shootPoint.rotation;
            body.linearVelocity = Vector2.zero;
            body.AddForce(transform.up * speed, ForceMode2D.Impulse);
        }

        private void Update()
        {
            lifeTimer += Time.deltaTime;
            if (lifeTimer >= lifetime) Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}