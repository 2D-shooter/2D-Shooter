using UniRx;
using UnityEngine;
using TMPro;
using TopDown.Shooting;
using System.Runtime.CompilerServices;
using static UnityEngine.Rendering.DebugUI;

namespace TopDown.UI
{

    public class NewMonoBehaviourScript : MonoBehaviour
    {
        private CompositeDisposable subscriptions = new CompositeDisposable();

        [SerializeField] private TextMeshProUGUI ammoCounterText;
        [SerializeField] private WeaponController weaponController;

        private int ammoInClip;
        private int totalAmmo;
        

        private void OnEnable()
        {
            weaponController.CurrentAmmoInClip.ObserveEveryValueChanged(property => property.Value).Subscribe(value =>
            {
                ammoInClip = value;
                UpdateAmmoCounter(ammoInClip, totalAmmo);
            }).AddTo(subscriptions);


            weaponController.TotalAmmo.ObserveEveryValueChanged(property => property.Value).Subscribe(value =>
            {
                totalAmmo = value;
                UpdateAmmoCounter(ammoInClip, totalAmmo);
            }).AddTo(subscriptions);
        }

        private void OnDisable()
        {
            subscriptions.Clear();
        }

        private void UpdateAmmoCounter(int currentAmmo, int totalAmmo)
        {
            ammoCounterText.text = $"{currentAmmo}/{totalAmmo}";
        }
    }
}