using System.Collections.Generic;
using Code.PlayerControllerModule.Enums;
using Code.PlayerControllerModule.Interfaces;
using Code.PlayerControllerModule.States;
using Zenject;

namespace Code.PlayerControllerModule.Services
{
    public class PlayerStateMachine : IStateMachine, ITickable, IFixedTickable
    {
        private readonly Dictionary<PlayerStateType, ICharacterState> _states;
        
        private ICharacterState _currentState;
        private PlayerStateType _currentStateType;

        public PlayerStateMachine(
            PlayerIdleState playerIdleState, 
            PlayerJumpState playerJumpState,
            PlayerMoveState playerMoveState,
            PlayerMoveSprintState playerMoveSprintState)
        {
            _states = new Dictionary<PlayerStateType, ICharacterState>
            {
                [PlayerStateType.Idle] = playerIdleState,
                [PlayerStateType.Jump] = playerJumpState,
                [PlayerStateType.Move] = playerMoveState,
                [PlayerStateType.MoveSprint] = playerMoveSprintState,
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
            foreach (var state in _states)
            {
                state.Value.SetStateMachine(this);
            }
            
            _currentStateType = PlayerStateType.Idle;
            _currentState = _states[_currentStateType];
            _currentState.OnEnterState();
        }
    }
}