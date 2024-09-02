using Code.WeaponModule.Enums;
using UnityEngine;

namespace Code.WeaponModule.Views
{
    public class WeaponItemAmmoView : MonoBehaviour
    {
        [SerializeField] public WeaponModelType WeaponModelType;
        [SerializeField] public int Ammo;
    }
}