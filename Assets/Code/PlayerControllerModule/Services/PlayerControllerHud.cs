using System.Globalization;
using Code.PlayerControllerModule.Configs;
using Code.PlayerControllerModule.Views;
using Code.UiModule.Interfaces;
using Code.UiModule.Services;
using Code.UiModule.Views.Enums;

namespace Code.PlayerControllerModule.Services
{
    public class PlayerControllerHud : IGuiController
    {
        private readonly PlayerConfig _playerConfig;
        private readonly UiCreator _uiCreator;
        private readonly PlayerStaminaController _playerStaminaController;
        
        private PlayerHudGui _playerHudGui;

        public PlayerControllerHud(
            PlayerConfig playerConfig, 
            UiCreator uiCreator, 
            PlayerStaminaController playerStaminaController)
        {
            _playerConfig = playerConfig;
            _uiCreator = uiCreator;
            _playerStaminaController = playerStaminaController;
            
            Show(); //todo должна будет вызываться там где будет создаваться игрок
        }

        public void Show()
        {
            _playerHudGui = _uiCreator.Create<PlayerHudGui>(UiType.Hud);

            OnStaminaChanged(_playerStaminaController.CurrentStamina);
            
            _playerStaminaController.StaminaChanged += OnStaminaChanged;
        }
        
        public void Close()
        {
            _playerStaminaController.StaminaChanged -= OnStaminaChanged;
        }

        private void OnStaminaChanged(float value)
        {
            _playerHudGui.StaminaBar.fillAmount = value / _playerConfig.MaxStamina;
            _playerHudGui.StaminaValueText.text = value.ToString("0");
        }
    }
}