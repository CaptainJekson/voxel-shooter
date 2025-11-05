using Code.CameraModule;
using Code.CameraModule.Views;
using Code.LevelModule;
using Code.PlayerControllerModule;
using Code.SoundModule;
using Code.UiModule;
using Code.UiModule.Views;
using Code.WeaponModule;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Zenject;

namespace Code
{
    public class StartupInstaller : MonoInstaller
    {
        [SerializeField] public UiRoot _uiRoot;
        [SerializeField] private GameLifeTimeScope _vContainerScope;
        
        public override void InstallBindings()
        {
            //force VContainer build
            _vContainerScope.SetZenjectContainer(Container);
            _vContainerScope.Build();
            
            var resolver = _vContainerScope.Container;
            Container.Bind<IObjectResolver>().FromInstance(resolver).AsSingle();
            //-----
            
            Container.Bind<UiRoot>().FromInstance(_uiRoot).AsSingle().NonLazy();
            
            CameraInstaller.Install(Container);
            SoundInstaller.Install(Container);
            LevelInstaller.Install(Container);
            PlayerControllerInstaller.Install(Container);
            WeaponInstaller.Install(Container);
            
            UiInstaller.Install(Container);
        }
    }
}
