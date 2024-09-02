using Code.WeaponModule.Enums;
using UnityEngine;

namespace Code.WeaponModule.Views
{
    public class WeaponItemView : MonoBehaviour
    {
        [SerializeField] public WeaponModelType WeaponModelType;
        [SerializeField] public int Ammo;
    }
}