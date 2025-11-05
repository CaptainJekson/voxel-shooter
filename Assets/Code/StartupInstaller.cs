using Code.PlayerControllerModule;
using Code.WeaponModule;
using UnityEngine;
using VContainer;
using Zenject;

namespace Code
{
    public class StartupInstaller : MonoInstaller
    {
        [SerializeField] private GameLifeTimeScope _vContainerScope;
        
        public override void InstallBindings()
        {
            //force VContainer build
            _vContainerScope.SetZenjectContainer(Container);
            _vContainerScope.Build();
            
            var resolver = _vContainerScope.Container;
            Container.Bind<IObjectResolver>().FromInstance(resolver).AsSingle();
            //-----
            
            PlayerControllerInstaller.Install(Container);
        }
    }
}
