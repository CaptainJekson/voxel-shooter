using Code.PlayerControllerModule.Configs;
using Code.PlayerControllerModule.Interfaces;
using Code.PlayerControllerModule.Services;
using Code.PlayerControllerModule.States.Base;
using Code.PlayerControllerModule.Views;
using UnityEngine;
using VContainer;

namespace Code.PlayerControllerModule.States
{
    public class PlayerJumpState : CharacterState
    {
        private readonly PlayerView _playerView;
        private readonly PlayerMover _playerMover;
        private readonly PlayerStaminaController _playerStaminaController;
        private readonly PlayerConfig _playerConfig;
        private readonly IPlayerInputProvider _playerInputProvider;
        private readonly PlayerSoundConfig _playerSoundConfig;
        
        public PlayerJumpState(
            PlayerMover playerMover, 
            PlayerStaminaController playerStaminaController, 
            IPlayerInputProvider playerInputProvider, 
            IObjectResolver objectResolver)
        {
            _playerView = objectResolver.Resolve<PlayerView>();
            _playerMover = playerMover;
            _playerStaminaController = playerStaminaController;
            _playerConfig = objectResolver.Resolve<PlayerConfig>();
            _playerInputProvider = playerInputProvider;
            _playerSoundConfig = objectResolver.Resolve<PlayerSoundConfig>();
        }

        public override void OnEnterState()
        {
            _playerView.jumpAudioSource.clip = _playerSoundConfig.JumpSound;
            _playerView.jumpAudioSource.loop = false;
            _playerView.jumpAudioSource.Play();
        }

        public override void OnExitState()
        {
        }

        public override bool CanEnter()
        {
            return _playerInputProvider.GetJumpInput() 
                   && !_playerView.isSlide 
                   && _playerView.isGrounded
                   && _playerStaminaController.CurrentStamina >= _playerConfig.JumpStaminaCost;
        }

        public override void Update()
        {
            _playerMover.Move(_playerInputProvider.GetMoveDirection(), _playerConfig.Speed);
            _playerMover.Rotate(_playerInputProvider.GetRotationY(), _playerInputProvider.GetRotationX());

            if (!CanEnter())
            {
                return;
            }
            
            Jump();
            _playerStaminaController.DecreaseStamina(_playerConfig.JumpStaminaCost);
        }

        private void Jump()
        {
            _playerView.verticalVelocity = Mathf.Sqrt(_playerConfig.JumpHeight * -2f * _playerConfig.Gravity);
        }
    }
}