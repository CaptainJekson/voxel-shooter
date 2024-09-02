using Code.PlayerControllerModule.Views;

namespace Code.PlayerControllerModule.Interfaces
{
    public interface ICharacterState
    {
        void OnEnterState();
        void OnExitState();
        void SetStateMachine(IStateMachine stateMachine);
        bool CanEnter();
        void Update();
        void FixedUpdate();
    }
}