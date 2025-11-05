using Code.CameraModule.Interfaces;
using Code.CameraModule.Views;
using UnityEngine;
using VContainer;

namespace Code.CameraModule.Services
{
    public class CameraController : ICameraController
    {
        private readonly Transform _cameraTransform;
        private readonly TestClassInVContainer  _testClassInVContainer;
        
        public CameraController(IObjectResolver objectResolver)
        {
            _cameraTransform = objectResolver.Resolve<MainCamera>().transform;
            _testClassInVContainer = objectResolver.Resolve<TestClassInVContainer>();
        }

        public void SetMainCameraInParent(Transform parent)
        {
            _testClassInVContainer.Make();
            
            _cameraTransform.position = parent.position;
            _cameraTransform.SetParent(parent);
        }

        public void SetDefaultPosition()
        {
            _cameraTransform.SetParent(null); 
            _cameraTransform.position = Vector3.zero;
        }
    }
}