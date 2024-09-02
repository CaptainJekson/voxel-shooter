using Code.CameraModule.Interfaces;
using Code.CameraModule.Views;
using UnityEngine;

namespace Code.CameraModule.Services
{
    public class CameraController : ICameraController
    {
        private readonly MainCamera _mainCamera;
        private readonly Transform _cameraTransform;
        
        public CameraController(MainCamera mainCamera)
        {
            _mainCamera = mainCamera;
            _cameraTransform = _mainCamera.transform;
        }

        public void SetMainCameraInParent(Transform parent)
        {
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