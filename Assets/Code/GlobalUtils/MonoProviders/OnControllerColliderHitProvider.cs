using System;
using UnityEngine;

namespace Code.GlobalUtils.MonoProviders
{
    public class OnControllerColliderHitProvider : MonoBehaviour
    {
        private Action<ControllerColliderHit> _action;

        public void Subscribe(Action<ControllerColliderHit> action)
        {
            _action += action;
        }
        
        public void Unsubscribe(Action<ControllerColliderHit> action)
        {
            _action -= action;
        }
        
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            _action?.Invoke(hit);
        } 
    }
}