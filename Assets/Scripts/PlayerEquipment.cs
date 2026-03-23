

using UnityEngine;
using UnityEngine.InputSystem;
using TopDown.Shooting;
using System.Collections.Generic;

namespace TopDown.Equipment
{
    public class PlayerEquipment : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private WeaponController weaponController;

        [Header("Visual Objects")]
        [SerializeField] private GameObject fistVisual;
        [SerializeField] private GameObject pistolVisual;
        [SerializeField] private GameObject arVisual;

        [Header("Specific FirePoints")]
        [SerializeField] private Transform pistolFirePoint;
        [SerializeField] private Transform arFirePoint;

        [Header("Weapon Data Assets")]
        [SerializeField] private WeaponData pistolData;
        [SerializeField] private WeaponData arData;

        // Tracks which weapons the player has picked up
        private HashSet<WeaponType> unlockedWeapons = new HashSet<WeaponType>();

        private void Awake()
        {
            unlockedWeapons.Add(WeaponType.Fists);
            Equip(WeaponType.Fists);
        }

        // Action Map Callbacks (Requires Alpha1, Alpha2, Alpha3 in Input Actions)
        private void OnAlpha1() => Equip(WeaponType.Fists);
        private void OnAlpha2() => Equip(WeaponType.Pistol);
        private void OnAlpha3() => Equip(WeaponType.AssaultRifle);

        private void Equip(WeaponType type)
        {
            if (!unlockedWeapons.Contains(type)) return;

            // 1. Toggle Visuals
            fistVisual.SetActive(type == WeaponType.Fists);
            pistolVisual.SetActive(type == WeaponType.Pistol);
            arVisual.SetActive(type == WeaponType.AssaultRifle);

            // 2. Prepare Data to send to Controller
            WeaponData selectedData = null;
            Transform selectedFirePoint = null;

            if (type == WeaponType.Pistol)
            {
                selectedData = pistolData;
                selectedFirePoint = pistolFirePoint;
            }
            else if (type == WeaponType.AssaultRifle)
            {
                selectedData = arData;
                selectedFirePoint = arFirePoint;
            }

            // 3. Update Controller
            weaponController.SwitchToWeapon(type, selectedData, selectedFirePoint);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PistolPickup"))
            {
                unlockedWeapons.Add(WeaponType.Pistol);
                Equip(WeaponType.Pistol);
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("ARPickup"))
            {
                unlockedWeapons.Add(WeaponType.AssaultRifle);
                Equip(WeaponType.AssaultRifle);
                Destroy(other.gameObject);
            }
        }
    }
}