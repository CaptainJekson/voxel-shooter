using System.Collections.Generic;
using Code.WeaponModule.Enums;

namespace Code.WeaponModule.Services
{
    public class WeaponStorage
    {
        private Dictionary<WeaponModelType, WeaponData> _weaponDataByType;

        public WeaponStorage()
        {
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
            }
            else
            {
                _weaponDataByType.Add(modelType, new WeaponData
                {
                    Ammo = ammo,
                    IsHasWeapon = isHasWeapon,
                });
            }
        }
    }

    public class WeaponData
    {
        public int Ammo;
        public bool IsHasWeapon;
    }
}