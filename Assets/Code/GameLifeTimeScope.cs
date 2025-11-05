using Code.CameraModule.Interfaces;
using Code.CameraModule.Services;
using Code.CameraModule.Views;
using Code.PlayerControllerModule.Configs;
using Code.PlayerControllerModule.Services;
using Code.PlayerControllerModule.Views;
using Code.UiModule.Configs;
using Code.UiModule.Services;
using Code.UiModule.Views;
using Code.WeaponModule.Configs;
using Code.WeaponModule.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Zenject;

namespace Code
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField] public MainCamera _mainCamera;
        [SerializeField] public UiRoot _uiRoot;
        
        private DiContainer _zenjectContainer;
        
        public void SetZenjectContainer(DiContainer zenjectContainer)
        {
            _zenjectContainer = zenjectContainer;
        }
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_zenjectContainer);
            //-----
            
            //ui configs
            builder.RegisterInstance(Resources.Load<UiHudConfig>("Configs/UiModule/UiHudConfig"));
            builder.RegisterInstance(Resources.Load<UiWindowConfig>("Configs/UiModule/UiWindowConfig"));
            builder.RegisterInstance(Resources.Load<UiPopupConfig>("Configs/UiModule/UiPopupConfig"));
            
            //player configs
            builder.RegisterInstance(Resources.Load<PlayerConfig>("Configs/PlayerControllerModule/PlayerConfig"));
            builder.RegisterInstance(Resources.Load<PlayerSoundConfig>("Configs/PlayerControllerModule/PlayerSoundConfig"));
            
            //weapon
            builder.RegisterInstance(Resources.Load<WeaponConfigCollection>("Configs/WeaponModule/WeaponConfig"));
            
            //instances
            builder.RegisterInstance(_mainCamera);
            builder.RegisterInstance(_uiRoot);

            //camera
            builder.Register<ICameraController, CameraController>(Lifetime.Singleton);
            
            //player factory
            builder.Register<PlayerFactory>(Lifetime.Singleton);
            builder.Register(resolver =>
            {
                var factory = resolver.Resolve<PlayerFactory>();
                return factory.Create();
            }, Lifetime.Singleton);
            
            //player
            //todo 
            
            //weapon
            builder.Register<WeaponStorage>(Lifetime.Singleton);
            builder.Register<WeaponItemCollector>(Lifetime.Singleton);
            
            //ui
            builder.Register<UiCreator>(Lifetime.Singleton);
        }
    }
}