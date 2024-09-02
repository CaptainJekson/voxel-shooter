using System;
using System.Collections.Generic;
using Code.UiModule.Configs;
using Code.UiModule.Views;
using Code.UiModule.Views.Enums;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.UiModule.Services
{
    public class UiCreator
    {
        private readonly UiRoot _uiRoot;
        private readonly UiHudConfig _uiHudConfig;
        private readonly UiWindowConfig _uiWindowConfig;
        private readonly UiPopupConfig _uiPopupConfig;
        private readonly List<GuiComponent> _uiCreated;
    
        public UiCreator(UiRoot uiRoot, UiHudConfig uiHudConfig, UiWindowConfig uiWindowConfig, UiPopupConfig uiPopupConfig)
        {
            _uiRoot = uiRoot;
            _uiHudConfig = uiHudConfig;
            _uiWindowConfig = uiWindowConfig;
            _uiPopupConfig = uiPopupConfig;
            _uiCreated = new List<GuiComponent>();
        }
    
        public T Create<T>(UiType uiType) where T : GuiComponent
        {
            T templateGui;
            RectTransform parent;
            
            switch (uiType)
            {
                case UiType.Hud:
                    templateGui = GetGuiComponent<T>(_uiHudConfig.Huds);
                    parent = _uiRoot.HudCanvas;
                    break;
                case UiType.Windows:
                    templateGui = GetGuiComponent<T>(_uiWindowConfig.Windows);
                    parent = _uiRoot.WindowsCanvas;
                    break;
                case UiType.Popup:
                    templateGui = GetGuiComponent<T>(_uiPopupConfig.Popups);
                    parent = _uiRoot.PopupCanvas;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(uiType), uiType, null);
            }

            var instanceGui = Object.Instantiate(templateGui, parent);

            return instanceGui;
        }

        public void Close<T>() where T : GuiComponent
        {
            foreach (var instance in _uiCreated)
            {
                if (instance.GetType() != typeof(T))
                {
                    continue;
                }
                
                _uiCreated.Remove(instance);
                Object.Destroy(instance.gameObject);
                break;
            }
        }
        
        private T GetGuiComponent<T>(IEnumerable<GuiComponent> guiComponents) where T : GuiComponent
        {
            foreach (var hud in guiComponents)
            {
                if (hud.GetType() != typeof(T))
                {
                    continue;
                }

                return (T)hud;
            }

            return null;
        }
    }
}