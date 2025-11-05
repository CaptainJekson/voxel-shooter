using Code.PlayerControllerModule.Configs;
using Code.PlayerControllerModule.Enums;
using Code.PlayerControllerModule.Interfaces;
using Code.PlayerControllerModule.Services;
using Code.PlayerControllerModule.States.Base;
using Code.PlayerControllerModule.Views;
using UnityEngine;
using VContainer;

namespace Code.PlayerControllerModule.States
{
    public class PlayerMoveSprintState : CharacterState
    {
        private readonly PlayerView _playerView;
        private readonly PlayerConfig _playerConfig;
        private readonly PlayerMover _playerMover;
        private readonly PlayerStaminaController _playerStaminaController;
        private readonly IPlayerInputProvider _playerInputProvider;
        private readonly PlayerSoundConfig _playerSoundConfig;
        
        private PlayerStateMachine _playerStateMachine;
        
        public PlayerMoveSprintState(
            IObjectResolver objectResolver,
            PlayerStaminaController playerStaminaController, 
            PlayerMover playerMover,
            IPlayerInputProvider playerInputProvider)
        {
            _playerConfig = objectResolver.Resolve<PlayerConfig>();
            _playerView = objectResolver.Resolve<PlayerView>();
            _playerStaminaController = playerStaminaController;
            _playerMover = playerMover;
            _playerInputProvider = playerInputProvider;
            _playerSoundConfig = objectResolver.Resolve<PlayerSoundConfig>();
        }
        
        public override void OnEnterState()
        {
            _playerView.stepAudioSource.clip = _playerSoundConfig.StepsFast;
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
                   && _playerInputProvider.GetSprintInput()
                   && (!_playerInputProvider.GetJumpInput() || _playerStaminaController.CurrentStamina < _playerConfig.JumpStaminaCost)
                   && _playerStaminaController.CurrentStamina > _playerConfig.StaminaMinStartSprint
                   && _playerView.isGrounded;
        }

        public override void SetStateMachine(IStateMachine stateMachine)
        {
            _playerStateMachine = (PlayerStateMachine) stateMachine;
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