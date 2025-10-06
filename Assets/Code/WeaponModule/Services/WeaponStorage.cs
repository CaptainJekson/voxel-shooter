using System;
using System.Collections.Generic;
using Code.WeaponModule.Configs;
using Code.WeaponModule.Enums;
using UnityEngine;

namespace Code.WeaponModule.Services
{
    public class WeaponStorage
    {
        private readonly WeaponConfigCollection _weaponConfigCollection;
        
        private Dictionary<WeaponModelType, WeaponData> _weaponDataByType;
        private WeaponModelType _selectedWeapon;

        public Dictionary<WeaponModelType, WeaponData> WeaponDataByType => _weaponDataByType;

        public event Action<WeaponModelType, int, int> WeaponAmmoChanged;
        public event Action<WeaponModelType> WeaponChanged;
        
        public WeaponStorage(
            WeaponConfigCollection weaponConfigCollection)
        {
            _weaponConfigCollection = weaponConfigCollection;
            
            _weaponDataByType = new Dictionary<WeaponModelType, WeaponData>();
        }
        
        public void AddAmmo(WeaponModelType modelType, int ammo, bool isHasWeapon)
        {
            if (_weaponDataByType.TryGetValue(modelType, out var weaponData))
            {
                weaponData.Ammo += ammo;

                if (!weaponData.IsHasWeapon && isHasWeapon)
                {
                    weaponData.IsHasWeapon = true;
                }
                
                WeaponAmmoChanged?.Invoke(modelType, weaponData.MagazineAmmo, weaponData.Ammo);
            }
            else
            {
                _weaponDataByType.Add(modelType, new WeaponData
                {
                    Ammo = ammo,
                    IsHasWeapon = isHasWeapon,
                });
                
                WeaponAmmoChanged?.Invoke(modelType, 0, ammo);
            }
        }

        public void ReloadSelectedWeapon()
        {
            if (!_weaponDataByType.TryGetValue(_selectedWeapon, out var weaponData))
            {
                Debug.LogError($"[WeaponStorage.RechargeSelectedWeapon] the selected weapon is not correct");
                return;
            }

            if (weaponData.Ammo <= 0)
            {
                return;
            }
            
            var magazineCapacity = _weaponConfigCollection.WeaponConfigsByType[_selectedWeapon].MagazineCapacity;

            if (weaponData.Ammo < magazineCapacity)
            {
                weaponData.MagazineAmmo = weaponData.Ammo;
                weaponData.Ammo = 0;
            }
            else
            {
                weaponData.Ammo =- magazineCapacity;
                weaponData.MagazineAmmo = magazineCapacity;
            }
            
            WeaponAmmoChanged?.Invoke(_selectedWeapon, weaponData.MagazineAmmo, weaponData.Ammo);
        }
        
        public bool TryDecreaseMagazineAmmoSelectedWeapon()
        {
            if (!_weaponDataByType.TryGetValue(_selectedWeapon, out var weaponData))
            {
                Debug.LogError($"[WeaponStorage.DecreaseMagazineAmmoSelectedWeapon] the selected weapon is not correct");
                return false;
            }

            if (weaponData.MagazineAmmo <= 0)
            {
                return false;
            }
            
            weaponData.MagazineAmmo--;
            WeaponAmmoChanged?.Invoke(_selectedWeapon, weaponData.MagazineAmmo, weaponData.Ammo);
            return true;
        }

        public bool TryGetSelectedWeaponData(out WeaponData weaponData)
        {
            return _weaponDataByType.TryGetValue(_selectedWeapon, out weaponData);
        }

        public bool TrySelectWeapon(WeaponModelType weaponModelType)
        {
            if (!_weaponDataByType.TryGetValue(weaponModelType, out var weaponData))
            {
                return false;
            }
            
            _selectedWeapon = weaponModelType;
            WeaponChanged?.Invoke(_selectedWeapon);
            WeaponAmmoChanged?.Invoke(_selectedWeapon, weaponData.MagazineAmmo, weaponData.Ammo);
            return true;
        }
    }

    public class WeaponData
    {
        public int Ammo;
        public int MagazineAmmo;
        public bool IsHasWeapon;
    }
}