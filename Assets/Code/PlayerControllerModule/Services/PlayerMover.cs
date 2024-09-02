using Code.PlayerControllerModule.Configs;
using Code.PlayerControllerModule.Views;
using UnityEngine;

namespace Code.PlayerControllerModule.Services
{
    public class PlayerMover
    {
        private readonly PlayerView _playerView;
        private readonly PlayerConfig _playerConfig;
        private readonly Transform _transform;

        public PlayerMover(
            PlayerView playerView, 
            PlayerConfig playerConfig)
        {
            _playerView = playerView;
            _playerConfig = playerConfig;
            _transform = _playerView.transform;
        }

        public void Move(Vector3 moveDirection, float speed)
        {
            moveDirection = _transform.TransformDirection(moveDirection.normalized);

            _playerView.isGrounded = _playerView.characterController.isGrounded;

            if (_playerView.isGrounded)
            {
                _playerView.verticalVelocity = -_playerConfig.Gravity * Time.deltaTime;
                
                var groundNormal = GetGroundNormal();
                
                if (Vector3.Angle(groundNormal, Vector3.up) > _playerConfig.SlopeLimit)
                {
                    var crossUp = Vector3.Cross(Vector3.up, groundNormal);
                    var slideDirection = Vector3.Cross(crossUp, groundNormal);
                    moveDirection += slideDirection * _playerConfig.SlidePower;
                    
                    _playerView.isSlide = true;
                }
                else
                {
                    _playerView.isSlide = false;
                }
            }
            else
            {
                _playerView.verticalVelocity += _playerConfig.Gravity * Time.deltaTime;
            }

            var finalMove = moveDirection * speed * Time.deltaTime;
            finalMove.y += _playerView.verticalVelocity * Time.deltaTime;
            
            _playerView.characterController.Move(finalMove);
        }
        
        public void Rotate(float rotationY, float rotationX)
        {
            var rotY = _playerConfig.RotateSpeed * rotationY;
            var rotX = _playerConfig.RotateSpeed * rotationX;
            
            _playerView.currentXRotation -= rotX;
            
            _playerView.currentXRotation = Mathf.Clamp(_playerView.currentXRotation, _playerConfig.MinRotationX,
                _playerConfig.MaxRotationX);
            
            _transform.Rotate(0, rotY, 0);
            
            _playerView.currentYRotation = _transform.rotation.x;
            
            _playerView.headTransform.localRotation = Quaternion.Euler(_playerView.currentXRotation, _playerView.currentYRotation, 0);
        }

        private Vector3 GetGroundNormal()
        {
            return Physics.Raycast(_transform.position, Vector3.down, out var hit, _playerConfig.RaycastGroundCheckDistance)
                ? hit.normal : Vector3.up;
        }
    }
}