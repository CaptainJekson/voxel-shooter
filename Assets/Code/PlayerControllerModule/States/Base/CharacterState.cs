using Code.PlayerControllerModule.Interfaces;
using Code.PlayerControllerModule.Views;

namespace Code.PlayerControllerModule.States.Base
{
    public abstract class CharacterState : ICharacterState
    {
        public virtual void OnEnterState()
        {
            
        }

        public virtual void OnExitState()
        {
            
        }

        public abstract bool CanEnter();
        
        public virtual void Update()
        {
            
        }

        public virtual void FixedUpdate()
        {
            
        }
    }
}