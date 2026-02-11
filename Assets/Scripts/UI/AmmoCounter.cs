//using UniRx;
//using UnityEngine;
//using TMPro;
//using TopDown.Shooting;
//using System.Runtime.CompilerServices;
//using static UnityEngine.Rendering.DebugUI;

//namespace TopDown.UI
//{

//    public class NewMonoBehaviourScript : MonoBehaviour
//    {
//        private CompositeDisposable subscriptions = new CompositeDisposable();

//        [SerializeField] private TextMeshProUGUI ammoCounterText;
//        [SerializeField] private WeaponController weaponController;

//        private int ammoInClip;
//        private int totalAmmo;


//        private void OnEnable()
//        {
//            weaponController.CurrentAmmoInClip.ObserveEveryValueChanged(property => property.Value).Subscribe(value =>
//            {
//                ammoInClip = value;
//                UpdateAmmoCounter(ammoInClip, totalAmmo);
//            }).AddTo(subscriptions);


//            weaponController.TotalAmmo.ObserveEveryValueChanged(property => property.Value).Subscribe(value =>
//            {
//                totalAmmo = value;
//                UpdateAmmoCounter(ammoInClip, totalAmmo);
//            }).AddTo(subscriptions);
//        }

//        private void OnDisable()
//        {
//            subscriptions.Clear();
//        }

//        private void UpdateAmmoCounter(int currentAmmo, int totalAmmo)
//        {
//            ammoCounterText.text = $"{currentAmmo}/{totalAmmo}";
//        }
//    }
//}

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