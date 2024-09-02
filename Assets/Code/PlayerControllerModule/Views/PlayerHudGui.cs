using Code.UiModule.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.PlayerControllerModule.Views
{
    public class PlayerHudGui : GuiComponent
    {
        [field: SerializeField] public Image StaminaBar { get; private set; }
        [field: SerializeField] public TextMeshProUGUI StaminaValueText { get; private set; }
    }
}