using Code.PlayerControllerModule.Configs;
using Code.PlayerControllerModule.Views;
using Code.UiModule.Interfaces;
using Code.UiModule.Services;
using Code.UiModule.Views.Enums;
using Code.WeaponModule.Configs;
using Code.WeaponModule.Enums;
using Code.WeaponModule.Services;

namespace Code.PlayerControllerModule.Services
{
    public class PlayerControllerHud : IGuiController
    {
        private readonly PlayerConfig _playerConfig;
        private readonly UiCreator _uiCreator;
        private readonly PlayerStaminaController _playerStaminaController;
        private readonly WeaponConfigCollection _weaponConfigCollection;
        private readonly WeaponStorage _weaponStorage;
        
        private PlayerHudGui _playerHudGui;
        private WeaponModelType _selectedWeapon;

        public PlayerControllerHud(
            PlayerConfig playerConfig, 
            UiCreator uiCreator, 
            PlayerStaminaController playerStaminaController,
            WeaponConfigCollection weaponConfigCollection,
            WeaponStorage weaponStorage)
        {
            _playerConfig = playerConfig;
            _uiCreator = uiCreator;
            _playerStaminaController = playerStaminaController;
            _weaponConfigCollection = weaponConfigCollection;
            _weaponStorage = weaponStorage;
            
            Show(); //todo должна будет вызываться там где будет создаваться игрок
        }

        public void Show()
        {
            _playerHudGui = _uiCreator.Create<PlayerHudGui>(UiType.Hud);

            OnStaminaChanged(_playerStaminaController.CurrentStamina);
            
            _playerStaminaController.StaminaChanged += OnStaminaChanged;
            _weaponStorage.WeaponChanged += OnWeaponChanged;
            _weaponStorage.WeaponAmmoChanged += OnWeaponAmmoChanged;
        }

        private void OnWeaponChanged(WeaponModelType weaponModelType)
        {
            if (!_weaponConfigCollection.WeaponConfigsByType.TryGetValue(weaponModelType, out var weaponConfig))
            {
                return;
            }

            _selectedWeapon = weaponModelType;
            _playerHudGui.WeaponIcon.sprite = weaponConfig.WeaponIcon;
        }
        
        private void OnWeaponAmmoChanged(WeaponModelType weaponModelType, int magazineAmmo, int ammo)
        {
            if (_selectedWeapon != weaponModelType)
            {
                return;
            }
            
            _playerHudGui.MagazineAmmoText.text = magazineAmmo.ToString();
            _playerHudGui.AllAmmoText.text = ammo.ToString();
        }

        public void Close()
        {
            _playerStaminaController.StaminaChanged -= OnStaminaChanged;
            _weaponStorage.WeaponChanged += OnWeaponChanged;
        }

        private void OnStaminaChanged(float value)
        {
            _playerHudGui.StaminaBar.fillAmount = value / _playerConfig.MaxStamina;
            _playerHudGui.StaminaValueText.text = value.ToString("0");
        }
    }
}