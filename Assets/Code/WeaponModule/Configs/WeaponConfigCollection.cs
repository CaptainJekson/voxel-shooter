using System;
using Code.WeaponModule.Views;
using Sirenix.OdinInspector;
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
    }

    [Serializable]
    public class WeaponConfig
    {
        //TODO оставил на потом
        //public bool IsThrowing;
        //public bool IsMelee;
        
        public WeaponView WeaponView;
        public WeaponItemView WeaponItemView;
        public WeaponItemAmmoView WeaponItemAmmoView;
        
        [Header("Stats")]
        public bool IsAutomatic;
        public int MagazineAmmo;
        public float PrewarmTime;
        public float ShootingRate;
        public float RechargeTime;
        public float Damage;

        public bool IsSleeve;

        [ShowIf("IsSleeve")] [Header("Sleeve settings")]
        public WeaponSleeveView WeaponSleeveView;

        public bool IsProjectile;
        
        [ShowIf("IsProjectile")] [Header("Projectile settings")]   
        public WeaponProjectileView WeaponProjectileView;
        [ShowIf("IsProjectile")]
        public float ProjectileSpeed;
        [ShowIf("IsProjectile")]
        public float ProjectileTimeLife;
    }
}