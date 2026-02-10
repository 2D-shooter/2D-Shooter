////using UnityEngine;

////namespace TopDown.Equipment
////{
////    public class PlayerEquipment : MonoBehaviour
////    {
////        [Header("Weapon Settings")]
////        [SerializeField] private GameObject pistolChild; // Drag the Pistol child here

////        private void Awake()
////        {
////            // Start the game with the pistol hidden
////            if (pistolChild != null)
////                pistolChild.SetActive(false);
////        }

////        private void OnTriggerEnter2D(Collider2D other)
////        {
////            // Ensure your ground item has the Tag "PistolPickup"
////            if (other.CompareTag("PistolPickup"))
////            {
////                EquipPistol();
////                Destroy(other.gameObject); // Remove the item from the ground
////            }
////        }

////        private void EquipPistol()
////        {
////            if (pistolChild != null)
////            {
////                pistolChild.SetActive(true);
////                Debug.Log("Pistol Equipped! Input and UI should now react.");
////            }
////        }
////    }
////}
//using UnityEngine;
//using TopDown.Shooting;

//namespace TopDown.Equipment
//{
//    public class PlayerEquipment : MonoBehaviour
//    {
//        [Header("References")]
//        [SerializeField] private WeaponController weaponController; // Drag Player here
//        [SerializeField] private GameObject pistolVisual;        // Drag the child Square here

//        private void Awake()
//        {
//            // Ensure the weapon is 'locked' and hidden at the start
//            if (weaponController != null)
//            {
//                weaponController.isEquipped = false;
//            }

//            if (pistolVisual != null)
//            {
//                pistolVisual.SetActive(false);
//            }
//        }

//        private void OnTriggerEnter2D(Collider2D other)
//        {
//            // Check for the pickup tag
//            if (other.CompareTag("PistolPickup"))
//            {
//                EquipPistol();
//                Destroy(other.gameObject); // Remove the item from the ground
//            }
//        }

//        private void EquipPistol()
//        {
//            if (weaponController != null)
//            {
//                weaponController.isEquipped = true;
//                Debug.Log("Weapon Controller Unlocked.");
//            }

//            if (pistolVisual != null)
//            {
//                pistolVisual.SetActive(true);
//                Debug.Log("Pistol Visuals Shown.");
//            }
//        }
//    }
//}

using UnityEngine;
using TopDown.Shooting;

namespace TopDown.Equipment
{
    public class PlayerEquipment : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private WeaponController weaponController; // The script on the Player
        [SerializeField] private GameObject pistolVisual;        // The Pistol Child Object
        [SerializeField] private GameObject fistVisual;          // The Fists Child Object

        private void Awake()
        {
            // At the start of the game:
            // 1. Logic is set to 'Unarmed' (isEquipped = false)
            if (weaponController != null)
            {
                weaponController.isEquipped = false;
            }

            // 2. Hide the gun
            if (pistolVisual != null)
            {
                pistolVisual.SetActive(false);
            }

            // 3. Show the fists
            if (fistVisual != null)
            {
                fistVisual.SetActive(true);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Look for the ground item tag
            if (other.CompareTag("PistolPickup"))
            {
                EquipPistol();
                Destroy(other.gameObject); // Remove the ground item
            }
        }

        private void EquipPistol()
        {
            if (weaponController != null)
            {
                // Unlocks shooting logic in WeaponController
                weaponController.isEquipped = true;
            }

            // Swap the visuals
            if (pistolVisual != null) pistolVisual.SetActive(true);
            if (fistVisual != null) fistVisual.SetActive(false);

            Debug.Log("Pistol equipped. Fists hidden.");
        }
    }
}