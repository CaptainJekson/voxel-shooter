using System;
using Code.WeaponModule.Enums;
using Code.WeaponModule.Views;
using UnityEngine;

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
        
        public WeaponModelType WeaponModelType;
        public WeaponView WeaponView;
        public Sprite WeaponIcon;
        
        [Header("Stats")]
        public bool IsAutomatic;
        public int MagazineAmmo;
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