using Code.CameraModule.Interfaces;
using Code.PlayerControllerModule.Configs;
using Code.PlayerControllerModule.Views;
using UnityEngine;
using VContainer;

namespace Code.PlayerControllerModule.Services
{
    public class PlayerFactory
    {
        private readonly PlayerConfig _playerConfig;
        private readonly ICameraController _cameraController;
        private readonly IObjectResolver _objectObjectResolver;
        
        public PlayerFactory(PlayerConfig playerConfig, ICameraController cameraController, IObjectResolver objectResolver)
        {
            _playerConfig = playerConfig;
            _cameraController = cameraController;
            _objectObjectResolver = objectResolver;
        }

        public PlayerView Create()
        {
            var prefab = _playerConfig.PlayerView;
            var spawnedPlayer = Object.Instantiate(prefab, _playerConfig.StartPosition, Quaternion.identity);
            
            _objectObjectResolver.Inject(spawnedPlayer);
            _cameraController.SetMainCameraInParent(spawnedPlayer.headTransform);

            return spawnedPlayer;
        }
    }
}