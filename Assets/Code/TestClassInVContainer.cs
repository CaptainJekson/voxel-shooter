using System.Threading.Tasks;
using Code.WeaponModule.Services;
using UnityEngine;
using Zenject;

namespace Code
{
    public class TestClassInVContainer
    {
        [VContainer.Inject] private readonly DiContainer _zenjectContainer;
        
        public void Make()
        {
            Debug.LogError("Zenject вызвал класс VContainer!");

            var weaponStorage = _zenjectContainer.Resolve<WeaponStorage>();
            weaponStorage.Make();
        }
    }
}