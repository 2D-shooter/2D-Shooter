//////using UnityEngine;
//////using UniRx;
//////using UnityEditor.Experimental.GraphView;

//////namespace TopDown.Shooting
//////{

//////    public class WeaponController : MonoBehaviour
//////    {
//////        [Header("Cooldown")]
//////        [SerializeField] private float cooldown = 0.25f;
//////        private float cooldownTimer;

//////        [Header("References")]
//////        [SerializeField] private GameObject bulletprefab;
//////        [SerializeField] private Transform firepoint;
//////        //[SerializeField] private Animator muzzleFlashAnimator;

//////        [Header("Ammo")]
//////        [SerializeField] private int initialAmmo;
//////        [SerializeField] private int clipSize;

//////        public IntReactiveProperty TotalAmmo { get; private set; } = new IntReactiveProperty(0);

//////        public IntReactiveProperty CurrentAmmoInClip { get; private set; } = new IntReactiveProperty(0);

//////        private void Awake()
//////        {
//////            TotalAmmo.Value = initialAmmo;

//////            if (initialAmmo <= clipSize)
//////                CurrentAmmoInClip.Value = initialAmmo;
//////            else
//////                CurrentAmmoInClip.Value = clipSize;

//////        }

//////        private void Update()
//////        {
//////            cooldownTimer += Time.deltaTime;
//////        }

//////        private void Shoot()
//////        {
//////            if (cooldownTimer < cooldown) return;
//////            if (CurrentAmmoInClip.Value <= 0) return;


//////            GameObject bullet = Instantiate(bulletprefab,firepoint.position,firepoint.rotation, null);
//////            bullet.GetComponent<Projectile>().ShootBullet(firepoint);
//////            //muzzleFlashAnimator.SetTrigger("shoot");
//////            cooldownTimer = 0;
//////            CurrentAmmoInClip.Value--;
//////        }

//////        private void Reload()
//////        {
//////            if(TotalAmmo.Value <= 0) return;


//////            int missingAmmo;
//////            missingAmmo = clipSize - CurrentAmmoInClip.Value;

//////            if (missingAmmo == 0) return;


//////            int reloadAmmo;

//////            if (TotalAmmo.Value >= missingAmmo)
//////                reloadAmmo = missingAmmo;
//////            else
//////                reloadAmmo = TotalAmmo.Value;

//////            CurrentAmmoInClip.Value += reloadAmmo;
//////            TotalAmmo.Value -= reloadAmmo;
//////        }

//////        #region Input
//////        private void OnShoot()
//////        {
//////            Shoot();   
//////        }

//////        private void OnReload()
//////        {
//////            Reload();
//////        }
//////        #endregion
//////    }
//////}

////using UnityEngine;
////using UniRx;

////namespace TopDown.Shooting
////{
////    public class WeaponController : MonoBehaviour
////    {
////        [Header("Cooldown")]
////        [SerializeField] private float cooldown = 0.25f;
////        private float cooldownTimer;

////        [Header("References")]
////        [SerializeField] private GameObject bulletprefab;
////        [SerializeField] private Transform firepoint;

////        [Header("Ammo")]
////        [SerializeField] private int initialAmmo = 30;
////        [SerializeField] private int clipSize = 10;

////        // UniRx Properties for the UI to observe
////        public IntReactiveProperty TotalAmmo { get; private set; } = new IntReactiveProperty(0);
////        public IntReactiveProperty CurrentAmmoInClip { get; private set; } = new IntReactiveProperty(0);

////        private void Awake()
////        {
////            // Initialize Ammo values
////            TotalAmmo.Value = initialAmmo;

////            // Set clip to full, or to total ammo if total is less than a full clip
////            CurrentAmmoInClip.Value = Mathf.Min(initialAmmo, clipSize);
////        }

////        private void Update()
////        {
////            cooldownTimer += Time.deltaTime;
////        }

////        private void Shoot()
////        {
////            // IMPORTANT: Only shoot if the script is enabled (picked up)
////            if (!this.enabled) return;

////            if (cooldownTimer < cooldown) return;
////            if (CurrentAmmoInClip.Value <= 0) return;

////            GameObject bullet = Instantiate(bulletprefab, firepoint.position, firepoint.rotation, null);

////            // Verify Projectile component exists before calling ShootBullet
////            if (bullet.TryGetComponent<Projectile>(out Projectile projectile))
////            {
////                projectile.ShootBullet(firepoint);
////            }

////            cooldownTimer = 0;
////            CurrentAmmoInClip.Value--;
////        }

////        private void Reload()
////        {
////            if (!this.enabled) return;
////            if (TotalAmmo.Value <= 0) return;

////            int missingAmmo = clipSize - CurrentAmmoInClip.Value;
////            if (missingAmmo <= 0) return;

////            int reloadAmount = Mathf.Min(TotalAmmo.Value, missingAmmo);

////            CurrentAmmoInClip.Value += reloadAmount;
////            TotalAmmo.Value -= reloadAmount;
////        }

////        #region Input System Messages
////        // These are called by the PlayerInput component on the Player object
////        private void OnShoot()
////        {
////            Shoot();
////        }

////        private void OnReload()
////        {
////            Reload();
////        }
////        #endregion
////    }
////}

//using UnityEngine;
//using UniRx;

//namespace TopDown.Shooting
//{
//    public class WeaponController : MonoBehaviour
//    {
//        [Header("Weapon Status")]
//        // This is toggled by the PlayerEquipment script
//        public bool isEquipped = false;

//        [Header("Cooldown")]
//        [SerializeField] private float cooldown = 0.25f;
//        private float cooldownTimer;

//        [Header("References")]
//        [SerializeField] private GameObject bulletprefab;
//        [SerializeField] private Transform firepoint;

//        [Header("Ammo")]
//        [SerializeField] private int initialAmmo = 30;
//        [SerializeField] private int clipSize = 10;

//        // Reactive properties for the UI
//        public IntReactiveProperty TotalAmmo { get; private set; } = new IntReactiveProperty(0);
//        public IntReactiveProperty CurrentAmmoInClip { get; private set; } = new IntReactiveProperty(0);

//        private void Awake()
//        {
//            TotalAmmo.Value = initialAmmo;
//            CurrentAmmoInClip.Value = Mathf.Min(initialAmmo, clipSize);
//        }

//        private void Update()
//        {
//            // Timer always runs, but Shoot() is blocked if not equipped
//            cooldownTimer += Time.deltaTime;
//        }

//        private void Shoot()
//        {
//            // Safety checks
//            if (!isEquipped) return;
//            if (cooldownTimer < cooldown) return;
//            if (CurrentAmmoInClip.Value <= 0) return;
//            if (bulletprefab == null || firepoint == null) return;

//            // Spawn the bullet
//            GameObject bullet = Instantiate(bulletprefab, firepoint.position, firepoint.rotation, null);

//            if (bullet.TryGetComponent<Projectile>(out Projectile projectile))
//            {
//                projectile.ShootBullet(firepoint);
//            }

//            cooldownTimer = 0;
//            CurrentAmmoInClip.Value--;
//        }

//        private void Reload()
//        {
//            if (!isEquipped) return;
//            if (TotalAmmo.Value <= 0) return;

//            int missingAmmo = clipSize - CurrentAmmoInClip.Value;
//            if (missingAmmo <= 0) return;

//            int reloadAmount = Mathf.Min(TotalAmmo.Value, missingAmmo);

//            CurrentAmmoInClip.Value += reloadAmount;
//            TotalAmmo.Value -= reloadAmount;
//        }

//        #region Input System Messages
//        private void OnShoot()
//        {
//            Shoot();
//        }

//        private void OnReload()
//        {
//            Reload();
//        }
//        #endregion
//    }
//}

using UnityEngine;
using UniRx;

namespace TopDown.Shooting
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Weapon Status")]
        public bool isEquipped = false;

        [Header("Fist / Melee References")]
        [SerializeField] private PunchEffect punchVisualEffect; // Drag PunchCircle here
        [SerializeField] private Transform fistPoint;           // Drag FistPoint here
        [SerializeField] private float punchDamage = 1f;
        [SerializeField] private float punchRange = 0.5f;
        [SerializeField] private float punchCooldown = 0.4f;

        [Header("Pistol Settings")]
        [SerializeField] private float shootCooldown = 0.25f;
        [SerializeField] private GameObject bulletprefab;
        [SerializeField] private Transform firepoint;

        [Header("Ammo")]
        [SerializeField] private int initialAmmo = 30;
        [SerializeField] private int clipSize = 10;

        private float cooldownTimer;

        public IntReactiveProperty TotalAmmo { get; private set; } = new IntReactiveProperty(0);
        public IntReactiveProperty CurrentAmmoInClip { get; private set; } = new IntReactiveProperty(0);

        private void Awake()
        {
            TotalAmmo.Value = initialAmmo;
            CurrentAmmoInClip.Value = Mathf.Min(initialAmmo, clipSize);
        }

        private void Update() => cooldownTimer += Time.deltaTime;

        private void HandleAction()
        {
            if (isEquipped) Shoot();
            else Punch();
        }

        private void Punch()
        {
            if (cooldownTimer < punchCooldown) return;

            // Trigger the visual circle pop
            if (punchVisualEffect != null) punchVisualEffect.Show();

            // Melee detection logic
            Collider2D[] hitObjects = Physics2D.OverlapCircleAll(fistPoint.position, punchRange);

            foreach (var obj in hitObjects)
            {
                if (obj.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.TakeDamage(punchDamage);
                }
            }

            cooldownTimer = 0;
        }

        private void Shoot()
        {
            if (cooldownTimer < shootCooldown) return;
            if (CurrentAmmoInClip.Value <= 0) return;

            GameObject bullet = Instantiate(bulletprefab, firepoint.position, firepoint.rotation, null);
            if (bullet.TryGetComponent<Projectile>(out Projectile projectile))
            {
                projectile.ShootBullet(firepoint);
            }

            cooldownTimer = 0;
            CurrentAmmoInClip.Value--;
        }

        private void Reload()
        {
            if (!isEquipped || TotalAmmo.Value <= 0) return;
            int missingAmmo = clipSize - CurrentAmmoInClip.Value;
            if (missingAmmo <= 0) return;

            int reloadAmount = Mathf.Min(TotalAmmo.Value, missingAmmo);
            CurrentAmmoInClip.Value += reloadAmount;
            TotalAmmo.Value -= reloadAmount;
        }

        private void OnShoot() => HandleAction();
        private void OnReload() => Reload();

        private void OnDrawGizmosSelected()
        {
            if (fistPoint != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(fistPoint.position, punchRange);
            }
        }
    }
}