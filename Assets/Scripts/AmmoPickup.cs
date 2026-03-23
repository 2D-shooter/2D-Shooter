

using UnityEngine;
using TopDown.Shooting;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private WeaponType ammoType;
    [SerializeField] private int ammoAmount = 30;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<WeaponController>(out WeaponController controller))
            {
                controller.AddAmmo(ammoType, ammoAmount);
                Destroy(gameObject);
            }
        }
    }
}