using Code.CameraModule.Interfaces;
using Code.CameraModule.Services;
using Zenject;

namespace Code.CameraModule
{
    public class CameraInstaller : Installer<CameraInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ICameraController>().To<CameraController>().AsSingle();
        }
    }
}