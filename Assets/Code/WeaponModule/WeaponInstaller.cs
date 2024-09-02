using Code.WeaponModule.Configs;
using Code.WeaponModule.Services;
using Zenject;

namespace Code.WeaponModule
{
    public class WeaponInstaller : Installer<WeaponInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<WeaponConfigCollection>().FromScriptableObjectResource("Configs/WeaponModule/WeaponConfig").AsSingle();

            Container.BindInterfacesAndSelfTo<WeaponStorage>().AsSingle();
            Container.Bind<WeaponItemCollector>().AsSingle();
        }
    }
}