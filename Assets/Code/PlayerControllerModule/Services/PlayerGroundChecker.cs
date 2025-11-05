using Code.PlayerControllerModule.Views;
using VContainer;
using Zenject;

namespace Code.PlayerControllerModule.Services
{
    public class PlayerGroundChecker : ITickable
    {
        private PlayerView _playerView;
        
        public PlayerGroundChecker(IObjectResolver objectResolver)
        {
            _playerView = objectResolver.Resolve<PlayerView>();
        }

        public void Tick()
        {
            _playerView.isGrounded = _playerView.characterController.isGrounded;
        }
    }
}