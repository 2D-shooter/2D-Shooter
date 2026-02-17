//using UnityEngine;
//using TopDown.Shooting;

//namespace TopDown.Equipment
//{
//    public class AmmoPickup : MonoBehaviour
//    {
//        [SerializeField] private WeaponType weaponType;
//        [SerializeField] private int ammoAmount = 20;

//        private void OnTriggerEnter2D(Collider2D other)
//        {
//            // Check if the player touched it
//            if (other.CompareTag("Player"))
//            {
//                WeaponController controller = other.GetComponentInChildren<WeaponController>();
//                if (controller != null)
//                {
//                    controller.AddAmmo(weaponType, ammoAmount);
//                    Destroy(gameObject);
//                }
//            }
//        }
//    }
//}
using UnityEngine;
using TopDown.Shooting;

namespace TopDown.Equipment
{
    public class AmmoPickup : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private WeaponType ammoType; // Set this to Pistol or AssaultRifle in Inspector
        [SerializeField] private int amountToGive = 20;

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the object has the WeaponController (The Player)
            if (other.TryGetComponent<WeaponController>(out WeaponController controller))
            {
                controller.AddAmmo(ammoType, amountToGive);

                Debug.Log($"Picked up {amountToGive} ammo for {ammoType}");

                Destroy(gameObject);
            }
        }
    }
}