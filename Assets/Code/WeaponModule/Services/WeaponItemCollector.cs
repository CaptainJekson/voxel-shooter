using System;
using Code.GlobalUtils.MonoProviders;
using Code.PlayerControllerModule.Views;
using Code.WeaponModule.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.WeaponModule.Services
{
    public class WeaponItemCollector : IDisposable
    {
        private readonly OnControllerColliderHitProvider _onControllerColliderHitProvider;
        private readonly WeaponStorage _weaponStorage;

        public WeaponItemCollector(
            PlayerView playerView,
            WeaponStorage weaponStorage)
        {
            _onControllerColliderHitProvider = playerView.colliderHitProvider;
            _weaponStorage = weaponStorage;
            
            _onControllerColliderHitProvider.Subscribe(Handler);
        }
        
        private void Handler(ControllerColliderHit colliderHit)
        {
            if (colliderHit.collider.TryGetComponent<WeaponItemView>(out var weaponItemView))
            {
                _weaponStorage.AddAmmo(weaponItemView.WeaponModelType, weaponItemView.Ammo, true);
                Object.Destroy(weaponItemView.gameObject);
            }
            else if (colliderHit.collider.TryGetComponent<WeaponItemAmmoView>(out var weaponItemAmmoView))
            {
                _weaponStorage.AddAmmo(weaponItemAmmoView.WeaponModelType, weaponItemAmmoView.Ammo, false);
                Object.Destroy(weaponItemAmmoView.gameObject);
            }
        }

        public void Dispose()
        {
            _onControllerColliderHitProvider.Unsubscribe(Handler);
        }
    }
}