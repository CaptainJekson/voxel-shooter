using System.Collections.Generic;
using System.ComponentModel;
using Code.PlayerControllerModule.Configs;
using Code.PlayerControllerModule.Enums;
using Code.PlayerControllerModule.Interfaces;
using Code.PlayerControllerModule.States;
using Code.PlayerControllerModule.Views;
using Code.PlayerModule.Interfaces;
using Zenject;

namespace Code.PlayerControllerModule.Services
{
    public class PlayerStateMachine : ITickable, IFixedTickable
    {
        private readonly Dictionary<PlayerStateType, ICharacterState> _states;
        
        private ICharacterState _currentState;
        private PlayerStateType _currentStateType;

        public PlayerStateMachine(
            PlayerView playerView, 
            PlayerMover playerMover, 
            PlayerStaminaController playerStaminaController,
            IPlayerInputProvider playerInputProvider, 
            PlayerConfig playerConfig, 
            PlayerSoundConfig playerSoundConfig)
        {
            _states = new Dictionary<PlayerStateType, ICharacterState>
            {
                { PlayerStateType.Idle, new PlayerIdleState(
                    playerView, 
                    playerMover, 
                    playerStaminaController,
                    playerConfig,
                    playerInputProvider) 
                },
                { PlayerStateType.Jump, new PlayerJumpState(
                    playerView, 
                    playerMover, 
                    playerStaminaController,
                    playerInputProvider, 
                    playerConfig,
                    playerSoundConfig) },
                { PlayerStateType.Move, new PlayerMoveState(
                    playerView, 
                    playerConfig,
                    playerMover, 
                    playerStaminaController,
                    playerInputProvider, 
                    playerSoundConfig) },
                { PlayerStateType.MoveSprint, new PlayerMoveSprintState(
                    this, 
                    playerConfig, 
                    playerView, 
                    playerStaminaController,
                    playerMover,
                    playerInputProvider, 
                    playerSoundConfig) },
            };

            InitializeState();
        }

        public void Tick()
        {
            _currentState.Update();

            foreach (var state in _states)
            {
                if (state.Value.CanEnter())
                {
                    ChangeState(state.Key);
                }
            }
        }

        public void FixedTick()
        {
            _currentState.FixedUpdate();
        }
        
        public void ChangeState(PlayerStateType state)
        {
            if (_currentStateType == state)
            {
                return;
            }

            _currentState.OnExitState();
            _currentState = _states[state];
            _currentStateType = state;
            _currentState.OnEnterState();
        }

        private void InitializeState()
        {
            _currentStateType = PlayerStateType.Idle;
            _currentState = _states[_currentStateType];
            _currentState.OnEnterState();
        }
    }
}