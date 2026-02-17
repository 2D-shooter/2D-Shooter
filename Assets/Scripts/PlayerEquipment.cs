////////using UnityEngine;

////////namespace TopDown.Equipment
////////{
////////    public class PlayerEquipment : MonoBehaviour
////////    {
////////        [Header("Weapon Settings")]
////////        [SerializeField] private GameObject pistolChild; // Drag the Pistol child here

////////        private void Awake()
////////        {
////////            // Start the game with the pistol hidden
////////            if (pistolChild != null)
////////                pistolChild.SetActive(false);
////////        }

////////        private void OnTriggerEnter2D(Collider2D other)
////////        {
////////            // Ensure your ground item has the Tag "PistolPickup"
////////            if (other.CompareTag("PistolPickup"))
////////            {
////////                EquipPistol();
////////                Destroy(other.gameObject); // Remove the item from the ground
////////            }
////////        }

////////        private void EquipPistol()
////////        {
////////            if (pistolChild != null)
////////            {
////////                pistolChild.SetActive(true);
////////                Debug.Log("Pistol Equipped! Input and UI should now react.");
////////            }
////////        }
////////    }
////////}
//////using UnityEngine;
//////using TopDown.Shooting;

//////namespace TopDown.Equipment
//////{
//////    public class PlayerEquipment : MonoBehaviour
//////    {
//////        [Header("References")]
//////        [SerializeField] private WeaponController weaponController; // Drag Player here
//////        [SerializeField] private GameObject pistolVisual;        // Drag the child Square here

//////        private void Awake()
//////        {
//////            // Ensure the weapon is 'locked' and hidden at the start
//////            if (weaponController != null)
//////            {
//////                weaponController.isEquipped = false;
//////            }

//////            if (pistolVisual != null)
//////            {
//////                pistolVisual.SetActive(false);
//////            }
//////        }

//////        private void OnTriggerEnter2D(Collider2D other)
//////        {
//////            // Check for the pickup tag
//////            if (other.CompareTag("PistolPickup"))
//////            {
//////                EquipPistol();
//////                Destroy(other.gameObject); // Remove the item from the ground
//////            }
//////        }

//////        private void EquipPistol()
//////        {
//////            if (weaponController != null)
//////            {
//////                weaponController.isEquipped = true;
//////                Debug.Log("Weapon Controller Unlocked.");
//////            }

//////            if (pistolVisual != null)
//////            {
//////                pistolVisual.SetActive(true);
//////                Debug.Log("Pistol Visuals Shown.");
//////            }
//////        }
//////    }
//////}

////using UnityEngine;
////using TopDown.Shooting;

////namespace TopDown.Equipment
////{
////    public class PlayerEquipment : MonoBehaviour
////    {
////        [Header("References")]
////        [SerializeField] private WeaponController weaponController; // The script on the Player
////        [SerializeField] private GameObject pistolVisual;        // The Pistol Child Object
////        [SerializeField] private GameObject fistVisual;          // The Fists Child Object

////        private void Awake()
////        {
////            // At the start of the game:
////            // 1. Logic is set to 'Unarmed' (isEquipped = false)
////            if (weaponController != null)
////            {
////                weaponController.isEquipped = false;
////            }

////            // 2. Hide the gun
////            if (pistolVisual != null)
////            {
////                pistolVisual.SetActive(false);
////            }

////            // 3. Show the fists
////            if (fistVisual != null)
////            {
////                fistVisual.SetActive(true);
////            }
////        }

////        private void OnTriggerEnter2D(Collider2D other)
////        {
////            // Look for the ground item tag
////            if (other.CompareTag("PistolPickup"))
////            {
////                EquipPistol();
////                Destroy(other.gameObject); // Remove the ground item
////            }
////        }

////        private void EquipPistol()
////        {
////            if (weaponController != null)
////            {
////                // Unlocks shooting logic in WeaponController
////                weaponController.isEquipped = true;
////            }

////            // Swap the visuals
////            if (pistolVisual != null) pistolVisual.SetActive(true);
////            if (fistVisual != null) fistVisual.SetActive(false);

////            Debug.Log("Pistol equipped. Fists hidden.");
////        }
////    }
////}

//using UnityEngine;
//using UnityEngine.InputSystem;
//using TopDown.Shooting;
//using System.Collections.Generic;

//namespace TopDown.Equipment
//{
//    public class PlayerEquipment : MonoBehaviour
//    {
//        [SerializeField] private WeaponController weaponController;

//        [Header("Visuals")]
//        [SerializeField] private GameObject fistVisual;
//        [SerializeField] private GameObject pistolVisual;
//        [SerializeField] private GameObject arVisual;

//        [Header("Weapon Data Assets")]
//        [SerializeField] private WeaponData pistolData;
//        [SerializeField] private WeaponData arData;

//        private HashSet<WeaponType> unlockedWeapons = new HashSet<WeaponType>();

//        private void Awake()
//        {
//            unlockedWeapons.Add(WeaponType.Fists);
//            Equip(WeaponType.Fists);
//        }

//        // Number Key Inputs via Input System
//        private void OnAlpha1() => Equip(WeaponType.Fists);
//        private void OnAlpha2() => Equip(WeaponType.Pistol);
//        private void OnAlpha3() => Equip(WeaponType.AssaultRifle);

//        private void Equip(WeaponType type)
//        {
//            if (!unlockedWeapons.Contains(type)) return;

//            // Toggle Visuals
//            fistVisual.SetActive(type == WeaponType.Fists);
//            pistolVisual.SetActive(type == WeaponType.Pistol);
//            arVisual.SetActive(type == WeaponType.AssaultRifle);

//            // Update Controller Logic
//            WeaponData data = null;
//            if (type == WeaponType.Pistol) data = pistolData;
//            else if (type == WeaponType.AssaultRifle) data = arData;

//            weaponController.SwitchToWeapon(type, data);
//        }

//        private void OnTriggerEnter2D(Collider2D other)
//        {
//            if (other.CompareTag("PistolPickup"))
//            {
//                unlockedWeapons.Add(WeaponType.Pistol);
//                Equip(WeaponType.Pistol);
//                Destroy(other.gameObject);
//            }
//            else if (other.CompareTag("ARPickup"))
//            {
//                unlockedWeapons.Add(WeaponType.AssaultRifle);
//                Equip(WeaponType.AssaultRifle);
//                Destroy(other.gameObject);
//            }
//            else if (other.CompareTag("AmmoPickup"))
//            {
//                // Example: Giving 20 ammo to both (or you could differentiate by tag)
//                weaponController.AddAmmo(WeaponType.Pistol, 10);
//                weaponController.AddAmmo(WeaponType.AssaultRifle, 30);
//                Destroy(other.gameObject);
//            }
//        }
//    }
//}

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