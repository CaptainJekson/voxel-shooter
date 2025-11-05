using Code.CameraModule.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Zenject;

namespace Code
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField] public MainCamera _mainCamera;
        
        private DiContainer _zenjectContainer;
        
        public void SetZenjectContainer(DiContainer zenjectContainer)
        {
            _zenjectContainer = zenjectContainer;
        }
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_zenjectContainer);
            //-----
            
            builder.RegisterInstance(_mainCamera);
            
            builder.Register<TestClassInVContainer>(Lifetime.Singleton);
        }
    }
}