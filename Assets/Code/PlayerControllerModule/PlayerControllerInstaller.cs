using Code.PlayerControllerModule.Configs;
using Code.PlayerControllerModule.Services;
using Code.PlayerControllerModule.States;
using Code.PlayerControllerModule.Views;
using Code.PlayerModule.Interfaces;
using Zenject;

namespace Code.PlayerControllerModule
{
    public class PlayerControllerInstaller : Installer<PlayerControllerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerConfig>().FromScriptableObjectResource("Configs/PlayerControllerModule/PlayerConfig")
                .AsSingle();
            
            Container.Bind<PlayerSoundConfig>().FromScriptableObjectResource("Configs/PlayerControllerModule/PlayerSoundConfig")
                .AsSingle();
            
            Container.Bind<PlayerView>().FromFactory<PlayerFactory>().AsSingle().NonLazy();
            
            //controller logic
            Container.Bind<IPlayerInputProvider>().To<PlayerDesktopInputProvider>().AsSingle();
            Container.Bind<PlayerMover>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerGroundChecker>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerCollisionHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerStaminaController>().AsSingle();

            Container.Bind<PlayerIdleState>().AsSingle();
            Container.Bind<PlayerJumpState>().AsSingle();
            Container.Bind<PlayerMoveState>().AsSingle();
            Container.Bind<PlayerMoveSprintState>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlayerStateMachine>().AsSingle();
            
            //ui
            Container.Bind<PlayerControllerHud>().AsSingle().NonLazy();
        }
    }
}