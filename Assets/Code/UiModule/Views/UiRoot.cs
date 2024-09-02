using UnityEngine;

namespace Code.UiModule.Views
{
    public class UiRoot : MonoBehaviour
    {
        [field: SerializeField] public RectTransform HudCanvas { get; private set; }
        [field: SerializeField] public RectTransform WindowsCanvas { get; private set; }
        [field: SerializeField] public RectTransform PopupCanvas { get; private set; }
    }
}