using System;
using Code.GlobalUtils.MonoProviders;
using Code.PlayerControllerModule.Configs;
using Code.PlayerControllerModule.Views;
using UnityEngine;

namespace Code.PlayerControllerModule.Services
{
    public class PlayerCollisionHandler : IDisposable
    {
        private readonly OnControllerColliderHitProvider _colliderHitProvider;
        private readonly PlayerConfig _playerConfig;
        
        public PlayerCollisionHandler(
            PlayerView playerView, 
            PlayerConfig playerConfig)
        {
            _colliderHitProvider = playerView.colliderHitProvider;
            _playerConfig = playerConfig;
            
            _colliderHitProvider.Subscribe(OnColliderHit);
        }

        public void Dispose()
        {
            _colliderHitProvider.Unsubscribe(OnColliderHit);
        }
        
        private void OnColliderHit(ControllerColliderHit hit)
        {
            var body = hit.collider.attachedRigidbody;

            if (body == null || body.isKinematic)
            {
                return;
            }

            if (hit.moveDirection.y < -0.3f)
            {
                return;
            }
            
            var pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            body.AddForce(pushDirection * _playerConfig.PushPower, ForceMode.Force);
        }
    }
}