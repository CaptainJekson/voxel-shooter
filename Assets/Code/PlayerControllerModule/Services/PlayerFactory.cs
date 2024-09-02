using Code.CameraModule.Interfaces;
using Code.PlayerControllerModule.Configs;
using Code.PlayerControllerModule.Views;
using UnityEngine;
using Zenject;

namespace Code.PlayerControllerModule.Services
{
    public class PlayerFactory : IFactory<PlayerView>
    {
        private readonly PlayerConfig _playerConfig;
        private readonly ICameraController _cameraController;
        
        public PlayerFactory(PlayerConfig playerConfig, ICameraController cameraController)
        {
            _playerConfig = playerConfig;
            _cameraController = cameraController;
        }
        
        public PlayerView Create()
        {
            //TODO далее камеру надо будет перемещать не здесь, а в другом модуле который будет отвечать за запуск сцены с уровнем
            
            var spawnedPlayer = Object.Instantiate(_playerConfig.PlayerView, _playerConfig.StartPosition, Quaternion.identity);
            _cameraController.SetMainCameraInParent(spawnedPlayer.headTransform);
            return spawnedPlayer;
        }
    }
}