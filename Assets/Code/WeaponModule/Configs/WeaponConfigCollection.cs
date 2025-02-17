using System;
using System.Collections.Generic;
using Code.WeaponModule.Enums;
using Code.WeaponModule.Views;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.WeaponModule.Configs
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/WeaponModule/WeaponConfig")]
    public class WeaponConfigCollection : ScriptableObject
    {
        [Header("General settings")] 
        public float DestroySleeveDelay;

        [Header("Weapons")] 
        public WeaponConfig[] WeaponConfigs;

        private Dictionary<WeaponModelType, WeaponConfig> _weaponConfigsByType;
        
        public Dictionary<WeaponModelType, WeaponConfig> WeaponConfigsByType
        {
            get
            {
                if (_weaponConfigsByType != null)
                {
                    return _weaponConfigsByType;
                }
        
                _weaponConfigsByType = new Dictionary<WeaponModelType, WeaponConfig>();
                
                foreach (var item in WeaponConfigs)
                {
                    _weaponConfigsByType.TryAdd(item.WeaponModelType, item);
                }
        
                return _weaponConfigsByType;
            }
        }
    }

    [Serializable]
    public class WeaponConfig
    {
        //TODO оставил на потом
        //public bool IsThrowing;
        //public bool IsMelee;
        
        public WeaponModelType WeaponModelType;
        public WeaponView WeaponView;
        public Sprite WeaponIcon;
        
        [Header("Stats")]
        public bool IsAutomatic;
        public int MagazineCapacity;
        public float PrewarmTime;
        public float ShootingRate;
        public float RechargeTime;
        public float Damage;

        public bool IsSleeve;

        //[ShowIf("IsSleeve")] [Header("Sleeve settings")]
        public WeaponSleeveView WeaponSleeveView;

        public bool IsProjectile;
        
        //[ShowIf("IsProjectile")] [Header("Projectile settings")]   
        public WeaponProjectileView WeaponProjectileView;
        //[ShowIf("IsProjectile")]
        public float ProjectileSpeed;
        //[ShowIf("IsProjectile")]
        public float ProjectileTimeLife;
    }
}