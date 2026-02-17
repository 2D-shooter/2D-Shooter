
using UnityEngine;

namespace TopDown.Shooting
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [Header("Movement Stats")]
        [SerializeField] private float speed = 20f;
        [SerializeField] private float lifetime = 2f;
        [SerializeField] private float damage = 1f;

        private Rigidbody2D body;
        private float lifeTimer;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        public void ShootBullet(Transform shootPoint)
        {
            // Reset the internal timer every time the bullet is fired
            lifeTimer = 0;

            body.linearVelocity = Vector2.zero;
            transform.position = shootPoint.position;
            transform.rotation = shootPoint.rotation;
            gameObject.SetActive(true);

            body.AddForce(transform.up * speed, ForceMode2D.Impulse);
        }

        private void Update()
        {
            // Increment lifeTimer until it hits the lifetime limit
            lifeTimer += Time.deltaTime;
            if (lifeTimer >= lifetime)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Try to find the IDamageable interface on the object we hit
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }

            // In a top-down shooter, bullets usually disappear on impact
            gameObject.SetActive(false);
        }
    }
}