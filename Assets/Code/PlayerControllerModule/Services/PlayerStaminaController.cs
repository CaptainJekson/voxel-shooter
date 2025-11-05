using System;
using Code.PlayerControllerModule.Configs;
using UnityEngine;
using VContainer;

namespace Code.PlayerControllerModule.Services
{
    public class PlayerStaminaController
    {
        private readonly PlayerConfig _playerConfig;

        private float _currentStamina;

        public float CurrentStamina => _currentStamina;

        public event Action<float> StaminaChanged; 

        public PlayerStaminaController(IObjectResolver objectResolver)
        {
            _playerConfig = objectResolver.Resolve<PlayerConfig>();
            _currentStamina = _playerConfig.MaxStamina;
        }

        public void RecoverStamina()
        {
            if (_currentStamina < _playerConfig.MaxStamina)
            {
                _currentStamina += _playerConfig.StaminaRecoveryRate * Time.deltaTime;
                
                StaminaChanged?.Invoke(_currentStamina);
            }
        }

        public void DecreaseStamina(float value)
        {
            _currentStamina -= value;
              
            StaminaChanged?.Invoke(_currentStamina);
        }
    }
}