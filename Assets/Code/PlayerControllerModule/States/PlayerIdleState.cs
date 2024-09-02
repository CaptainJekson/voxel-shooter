using Code.PlayerControllerModule.Configs;
using Code.PlayerControllerModule.Services;
using Code.PlayerControllerModule.States.Base;
using Code.PlayerControllerModule.Views;
using Code.PlayerModule.Interfaces;
using UnityEngine;

namespace Code.PlayerControllerModule.States
{
    public class PlayerIdleState : CharacterState
    {
        private readonly PlayerView _playerView;
        private readonly PlayerMover _playerMover;
        private readonly PlayerStaminaController _playerStaminaController;
        private readonly PlayerConfig _playerConfig;
        private readonly IPlayerInputProvider _playerInputProvider;
        
        public PlayerIdleState(
            PlayerView playerView, 
            PlayerMover playerMover, 
            PlayerStaminaController playerStaminaController,
            PlayerConfig playerConfig, 
            IPlayerInputProvider playerInputProvider)
        {
            _playerView = playerView;
            _playerMover = playerMover;
            _playerStaminaController = playerStaminaController;
            _playerConfig = playerConfig;
            _playerInputProvider = playerInputProvider;
        }
        
        public override void OnEnterState()
        {
        }

        public override void OnExitState()
        {
        }

        public override bool CanEnter()
        {
            return _playerInputProvider.GetMoveDirection() == Vector3.zero 
                   && (!_playerInputProvider.GetJumpInput() || _playerStaminaController.CurrentStamina < _playerConfig.JumpStaminaCost)
                   && _playerView.isGrounded;
        }

        public override void Update()
        {
            _playerMover.Move(Vector3.zero, 0.0f);
            _playerMover.Rotate(_playerInputProvider.GetRotationY(), _playerInputProvider.GetRotationX());
            _playerStaminaController.RecoverStamina();
        }

        private void CameraRotate()
        {
            var rotateSpeed = _playerConfig.RotateSpeed;
            
            var rotY = rotateSpeed * _playerInputProvider.GetRotationY();
            var rotX = rotateSpeed * _playerInputProvider.GetRotationX();

            _playerView.currentXRotation -= rotX;
            _playerView.currentYRotation += rotY;
            
            _playerView.currentXRotation = Mathf.Clamp(_playerView.currentXRotation, 
                _playerConfig.MinRotationX, _playerConfig.MaxRotationX);
            
            _playerView.headTransform.localRotation = Quaternion.Euler(_playerView.currentXRotation, 
                _playerView.currentYRotation, 0);
        }
    }
}