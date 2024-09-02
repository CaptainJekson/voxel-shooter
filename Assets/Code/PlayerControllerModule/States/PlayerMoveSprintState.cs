using Code.PlayerControllerModule.Configs;
using Code.PlayerControllerModule.Enums;
using Code.PlayerControllerModule.Services;
using Code.PlayerControllerModule.States.Base;
using Code.PlayerControllerModule.Views;
using Code.PlayerModule.Interfaces;
using UnityEngine;

namespace Code.PlayerControllerModule.States
{
    public class PlayerMoveSprintState : CharacterState
    {
        private readonly PlayerView _playerView;
        private readonly PlayerConfig _playerConfig;
        private readonly PlayerMover _playerMover;
        private readonly PlayerStaminaController _playerStaminaController;
        private readonly IPlayerInputProvider _playerInputProvider;
        private readonly PlayerStateMachine _playerStateMachine;
        private readonly PlayerSoundConfig _playerSoundConfig;
        
        public PlayerMoveSprintState(
            PlayerStateMachine playerStateMachine, 
            PlayerConfig playerConfig, 
            PlayerView playerView, 
            PlayerStaminaController playerStaminaController, 
            PlayerMover playerMover,
            IPlayerInputProvider playerInputProvider, 
            PlayerSoundConfig playerSoundConfig)
        {
            _playerStateMachine = playerStateMachine;
            _playerConfig = playerConfig;
            _playerView = playerView;
            _playerStaminaController = playerStaminaController;
            _playerMover = playerMover;
            _playerInputProvider = playerInputProvider;
            _playerSoundConfig = playerSoundConfig;
        }
        
        public override void OnEnterState()
        {
            _playerView.mouthAudioSource.clip = _playerSoundConfig.StepsFast;
            _playerView.mouthAudioSource.loop = true;
            _playerView.mouthAudioSource.Play();
        }

        public override void OnExitState()
        {
            _playerView.mouthAudioSource.Stop();
        }

        public override bool CanEnter()
        {
            return _playerInputProvider.GetMoveDirection() != Vector3.zero
                   && _playerInputProvider.GetSprintInput()
                   && (!_playerInputProvider.GetJumpInput() || _playerStaminaController.CurrentStamina < _playerConfig.JumpStaminaCost)
                   && _playerStaminaController.CurrentStamina > _playerConfig.StaminaMinStartSprint
                   && _playerView.isGrounded;
        }

        public override void Update()
        {
            _playerMover.Move(_playerInputProvider.GetMoveDirection(), _playerConfig.Speed * _playerConfig.SprintMultiplier);
            _playerMover.Rotate(_playerInputProvider.GetRotationY(), _playerInputProvider.GetRotationX());

            _playerStaminaController.DecreaseStamina(_playerConfig.StaminaDrainRate * Time.deltaTime);
            
            if (_playerStaminaController.CurrentStamina > 0.0f)
            {
                return;
            }
            
            _playerStateMachine.ChangeState(PlayerStateType.Move);
        }
    }
}