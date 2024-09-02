using Code.PlayerControllerModule.Views;

namespace Code.PlayerControllerModule.Interfaces
{
    public interface ICharacterState
    {
        void OnEnterState();
        void OnExitState();
        bool CanEnter();
        void Update();
        void FixedUpdate();
    }
}