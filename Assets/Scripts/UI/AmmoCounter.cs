

using UniRx;
using UnityEngine;
using TMPro;
using TopDown.Shooting;

namespace TopDown.UI
{
    public class AmmoCounter : MonoBehaviour
    {
        private CompositeDisposable subscriptions = new CompositeDisposable();

        [Header("UI Components")]
        [SerializeField] private TextMeshProUGUI ammoCounterText;

        [Header("Weapon Source")]
        [SerializeField] private WeaponController weaponController;

        private int _ammoInClip;
        private int _totalAmmo;

        private void OnEnable()
        {
            if (weaponController == null)
            {
                Debug.LogWarning($"AmmoCounter on {gameObject.name} is missing a WeaponController reference!");
                return;
            }

            // Subscribe directly to the ReactiveProperties
            weaponController.CurrentAmmoInClip
                .Subscribe(value =>
                {
                    _ammoInClip = value;
                    UpdateUI();
                })
                .AddTo(subscriptions);

            weaponController.TotalAmmo
                .Subscribe(value =>
                {
                    _totalAmmo = value;
                    UpdateUI();
                })
                .AddTo(subscriptions);
        }

        private void OnDisable()
        {
            // This prevents memory leaks by disposing of all subscriptions at once
            subscriptions.Clear();
        }

        private void UpdateUI()
        {
            if (ammoCounterText != null)
            {
                ammoCounterText.text = $"{_ammoInClip}/{_totalAmmo}";
            }
        }
    }
}