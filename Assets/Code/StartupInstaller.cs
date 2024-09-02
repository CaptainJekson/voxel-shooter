using Code.CameraModule;
using Code.CameraModule.Views;
using Code.LevelModule;
using Code.PlayerControllerModule;
using Code.SoundModule;
using Code.UiModule;
using Code.UiModule.Views;
using Code.WeaponModule;
using UnityEngine;
using Zenject;

namespace Code
{
    public class StartupInstaller : MonoInstaller
    {
        [SerializeField] public MainCamera _mainCamera;
        [SerializeField] public UiRoot _uiRoot;
        
        public override void InstallBindings()
        {
            Container.Bind<MainCamera>().FromInstance(_mainCamera).AsSingle().NonLazy();
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
