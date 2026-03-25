using UnityEngine;
using UniRx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TopDown.Core;


namespace TopDown.Shooting
{
    public enum WeaponType { Fists, Pistol, AssaultRifle }

    public class WeaponController : MonoBehaviour
    {
        private Animator anim;

        public WeaponType currentWeapon = WeaponType.Fists;
        public WeaponData activeWeaponData;

        [Header("Melee Settings")]
        [SerializeField] private PunchEffect punchVisualEffect;
        [SerializeField] private Transform fistPoint;
        [SerializeField] private float punchDamage = 25f;

        private float nextFireTime = 0f;
        private bool hasReleasedSinceLastShot = true;
        private Transform currentFirePoint;

        private bool isReloading = false;
        private WeaponType reloadingWeapon;

        private Dictionary<WeaponType, IntReactiveProperty> clipAmmoPool = new Dictionary<WeaponType, IntReactiveProperty>();
        private Dictionary<WeaponType, IntReactiveProperty> totalAmmoPool = new Dictionary<WeaponType, IntReactiveProperty>();

        public IntReactiveProperty CurrentAmmoInClip { get; private set; } = new IntReactiveProperty(0);
        public IntReactiveProperty TotalAmmo { get; private set; } = new IntReactiveProperty(0);

        private void Awake()
        {
            
            anim = GetComponentInChildren<Animator>();

            InitializeAmmo(WeaponType.Pistol, 10, 30);
            InitializeAmmo(WeaponType.AssaultRifle, 30, 90);
            SwitchToWeapon(WeaponType.Fists);
        }

        private void InitializeAmmo(WeaponType type, int clip, int total)
        {
            if (!clipAmmoPool.ContainsKey(type))
            {
                clipAmmoPool[type] = new IntReactiveProperty(clip);
                totalAmmoPool[type] = new IntReactiveProperty(total);
            }
        }

        public void SwitchToWeapon(WeaponType newType, WeaponData data = null, Transform weaponFirePoint = null)
        {
            currentWeapon = newType;
            activeWeaponData = data;
            currentFirePoint = weaponFirePoint;
            hasReleasedSinceLastShot = true;

            if (anim != null)
            {
                anim.SetInteger("weapon_int", (int)newType);
                Debug.Log("Weapon: " + (int)newType);
            }

            if (newType != WeaponType.Fists && clipAmmoPool.ContainsKey(newType))
            {
                CurrentAmmoInClip.Value = clipAmmoPool[newType].Value;
                TotalAmmo.Value = totalAmmoPool[newType].Value;
            }
            else
            {
                CurrentAmmoInClip.Value = 0;
                TotalAmmo.Value = 0;
            }
        }

        private void Update()
        {
            bool isPressing = false;
            if (Mouse.current != null) isPressing = Mouse.current.leftButton.isPressed;

            if (!isPressing) hasReleasedSinceLastShot = true;

            // estä ampuminen reloadin aikana
            if (isReloading) return;

            if (!isPressing || Time.time < nextFireTime) return;

            if (currentWeapon == WeaponType.Fists)
            {
                if (hasReleasedSinceLastShot) DoPunch();
                return;
            }

            if (activeWeaponData == null) return;
            if (!activeWeaponData.isAutomatic && !hasReleasedSinceLastShot) return;

            if (clipAmmoPool.ContainsKey(currentWeapon) && clipAmmoPool[currentWeapon].Value > 0)
            {
                DoShoot();
                SoundFXManager.Instance.PlayShoot();
            }
            else
            {
                // Empty klik (vain kerran per painallus)
                if (hasReleasedSinceLastShot)
                {
                    SoundFXManager.Instance.PlayEmptyGun();
                    hasReleasedSinceLastShot = false;
                }
            }
        }

        private void DoShoot()
        {
            float delay = Mathf.Max(activeWeaponData.fireRate, 0.1f);
            nextFireTime = Time.time + delay;
            hasReleasedSinceLastShot = false;

            GameObject bullet = Instantiate(activeWeaponData.projectilePrefab, currentFirePoint.position, currentFirePoint.rotation);
            if (bullet.TryGetComponent<Projectile>(out Projectile projectile))
            {
                projectile.SetDamage(activeWeaponData.damage);
                projectile.ShootBullet(currentFirePoint);
            }

            //  vähennetään ammo
            clipAmmoPool[currentWeapon].Value--;
            CurrentAmmoInClip.Value = clipAmmoPool[currentWeapon].Value;

            //  VAIN Assault Rifle → viimeinen panos klik viiveellä
            if (currentWeapon == WeaponType.AssaultRifle &&
                clipAmmoPool[currentWeapon].Value == 0)
            {
                StartCoroutine(PlayEmptyWithDelay(0.05f));
            }
        }

        private IEnumerator PlayEmptyWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            SoundFXManager.Instance.PlayEmptyGun();
        }

        private void DoPunch()
        {
            nextFireTime = Time.time + 0.4f;
            hasReleasedSinceLastShot = false;

            SoundFXManager.Instance.PlayFistSwing();

            if (anim != null)
        {
            anim.SetTrigger("Punch_animation");
            Debug.Log("Punch triggered");
        }

            if (punchVisualEffect != null) punchVisualEffect.Show();

            bool hitEnemy = false;

            Collider2D[] hitObjects = Physics2D.OverlapCircleAll(fistPoint.position, 0.5f);
            foreach (var obj in hitObjects)
            {
                if (obj.TryGetComponent<Health>(out var health))
                {
                    if (health.entityType != Health.EntityType.Player)
                    {
                        health.TakeDamage(punchDamage);
                        hitEnemy = true;
                    }
                }
            }

            if (hitEnemy)
            {
                SoundFXManager.Instance.PlayFistHit();
            }
        }

        public void AddAmmo(WeaponType type, int amount)
        {
            if (totalAmmoPool.ContainsKey(type))
            {
                totalAmmoPool[type].Value += amount;
                if (currentWeapon == type)
                {
                    TotalAmmo.Value = totalAmmoPool[type].Value;
                }
            }
        }

        private void OnReload()
        {
            if (currentWeapon == WeaponType.Fists || activeWeaponData == null) return;
            if (!clipAmmoPool.ContainsKey(currentWeapon)) return;

            if (!isReloading)
            {
                StartCoroutine(ReloadRoutine());
            }
        }

private IEnumerator ReloadRoutine()
{
    isReloading = true;

    // tallenna ase joka aloitti reloadin
    reloadingWeapon = currentWeapon;

    SoundFXManager.Instance.PlayReload();

    yield return new WaitForSeconds(1.5f);

    // jos ase vaihdettu kesken reloadin → älä tee mitään
    if (currentWeapon != reloadingWeapon)
    {
        isReloading = false;
        yield break;
    }

    // tarkista että ase on edelleen validi
    if (activeWeaponData == null || !clipAmmoPool.ContainsKey(currentWeapon))
    {
        isReloading = false;
        yield break;
    }

    int missing = activeWeaponData.clipSize - clipAmmoPool[currentWeapon].Value;
    int available = totalAmmoPool[currentWeapon].Value;
    int amount = Mathf.Min(missing, available);

    if (amount > 0)
    {
        clipAmmoPool[currentWeapon].Value += amount;
        totalAmmoPool[currentWeapon].Value -= amount;

        CurrentAmmoInClip.Value = clipAmmoPool[currentWeapon].Value;
        TotalAmmo.Value = totalAmmoPool[currentWeapon].Value;
    }

    isReloading = false;
}
    }
}