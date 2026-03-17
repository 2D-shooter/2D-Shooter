//using UnityEngine;
//using TopDown.Shooting;

//public class EnemyShooting : MonoBehaviour
//{
//    [Header("Shooting")]
//    [SerializeField] private Projectile projectilePrefab;
//    [SerializeField] private Transform shootPoint;
//    [SerializeField] private float fireRate = 1f; // seconds between shots
//    [SerializeField] private float shootDistance = 8f;

//    private Transform target;
//    private float fireTimer;

//    void Update()
//    {
//        if (target == null) return;

//        fireTimer += Time.deltaTime;

//        float distance = Vector2.Distance(transform.position, target.position);

//        if (distance <= shootDistance && fireTimer >= fireRate)
//        {
//            Shoot();
//            fireTimer = 0f;
//        }
//    }

//    void Shoot()
//    {
//        // rotate shoot point towards target
//        Vector2 dir = (target.position - shootPoint.position).normalized;
//        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

//        shootPoint.rotation = Quaternion.Euler(0, 0, angle);

//        // spawn projectile
//        Projectile bullet = Instantiate(projectilePrefab);
//        bullet.ShootBullet(shootPoint);
//    }

//    public void SetTarget(Transform t)
//    {
//        target = t;
//    }

//    public void ClearTarget()
//    {
//        target = null;
//    }
//}

using UnityEngine;
using TopDown.Shooting;

public class EnemyShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float fireRate = 1f; // Seconds between shots
    [SerializeField] private float shootDistance = 8f;

    // NEW: You can now change this in the Unity Inspector!
    [SerializeField] private float bulletDamage = 10f;

    private Transform target;
    private float fireTimer;

    void Update()
    {
        if (target == null) return;

        fireTimer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= shootDistance && fireTimer >= fireRate)
        {
            Shoot();
            SoundFXManager.Instance.PlayShoot();
            fireTimer = 0f;
        }
    }

    void Shoot()
    {
        if (shootPoint == null || projectilePrefab == null) return;

        // Rotate shoot point towards target
        Vector2 dir = (target.position - shootPoint.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        shootPoint.rotation = Quaternion.Euler(0, 0, angle);

        // Spawn projectile
        Projectile bullet = Instantiate(projectilePrefab);

        // Pass the damage from the Inspector to the bullet
        bullet.SetDamage(bulletDamage);

        bullet.ShootBullet(shootPoint);
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    public void ClearTarget()
    {
        target = null;
    }
}