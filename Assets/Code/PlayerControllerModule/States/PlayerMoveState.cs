using Code.PlayerControllerModule.Configs;
using Code.PlayerControllerModule.Interfaces;
using Code.PlayerControllerModule.Services;
using Code.PlayerControllerModule.States.Base;
using Code.PlayerControllerModule.Views;
using UnityEngine;
using VContainer;

namespace Code.PlayerControllerModule.States
{
    public class PlayerMoveState : CharacterState
    {
        private readonly PlayerView _playerView;
        private readonly PlayerConfig _playerConfig;
        private readonly PlayerMover _playerMover;
        private readonly PlayerStaminaController _playerStaminaController;
        private readonly IPlayerInputProvider _playerInputProvider;
        private readonly PlayerSoundConfig _playerSoundConfig;
        
        private float _verticalVelocity;
        
        public PlayerMoveState(
            IObjectResolver objectResolver,
            PlayerMover playerMover, 
            PlayerStaminaController playerStaminaController,
            IPlayerInputProvider playerInputProvider)
        {
            _playerView = objectResolver.Resolve<PlayerView>();
            _playerConfig = objectResolver.Resolve<PlayerConfig>();
            _playerMover = playerMover;
            _playerStaminaController = playerStaminaController;
            _playerInputProvider = playerInputProvider;
            _playerSoundConfig = objectResolver.Resolve<PlayerSoundConfig>();
        }

        public override void OnEnterState()
        {
            _playerView.stepAudioSource.clip = _playerSoundConfig.Steps;
            _playerView.stepAudioSource.loop = true;
            _playerView.stepAudioSource.Play();
        }

        public override void OnExitState()
        {
            _playerView.stepAudioSource.Stop();
        }

        public override bool CanEnter()
        {
            return _playerInputProvider.GetMoveDirection() != Vector3.zero
                   && !_playerInputProvider.GetSprintInput() 
                   && (!_playerInputProvider.GetJumpInput() || _playerStaminaController.CurrentStamina < _playerConfig.JumpStaminaCost)
                   && _playerView.isGrounded;
        }

        public override void Update()
        {
            _playerMover.Move(_playerInputProvider.GetMoveDirection(), _playerConfig.Speed);
            _playerMover.Rotate(_playerInputProvider.GetRotationY(), _playerInputProvider.GetRotationX());
            _playerStaminaController.RecoverStamina();
        }
    }
}