using Code.PlayerControllerModule.Views;
using Zenject;

namespace Code.PlayerControllerModule.Services
{
    public class PlayerGroundChecker : ITickable
    {
        private PlayerView _playerView;
        
        public PlayerGroundChecker(PlayerView playerView)
        {
            _playerView = playerView;
        }

        public void Tick()
        {
            _playerView.isGrounded = _playerView.characterController.isGrounded;
        }
    }
}