using Code.PlayerControllerModule.Configs;
using Code.PlayerControllerModule.Services;
using Code.PlayerControllerModule.States.Base;
using Code.PlayerControllerModule.Views;
using Code.PlayerModule.Interfaces;
using UnityEngine;

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
            PlayerView playerView, 
            PlayerMover playerMover, 
            PlayerStaminaController playerStaminaController, 
            IPlayerInputProvider playerInputProvider, 
            PlayerConfig playerConfig, 
            PlayerSoundConfig playerSoundConfig)
        {
            _playerView = playerView;
            _playerMover = playerMover;
            _playerStaminaController = playerStaminaController;
            _playerConfig = playerConfig;
            _playerInputProvider = playerInputProvider;
            _playerSoundConfig = playerSoundConfig;
        }

        public override void OnEnterState()
        {
            _playerView.mouthAudioSource.clip = _playerSoundConfig.JumpSound;
            _playerView.mouthAudioSource.loop = false;
            _playerView.mouthAudioSource.Play();
        }

        public override void OnExitState()
        {
            _playerView.mouthAudioSource.Stop();
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