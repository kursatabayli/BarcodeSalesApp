using Barkod.Constants;
using Barkod.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barkod.Services
{
    public class UIStateService : IUIStateService
    {
        public event Action? OnChange;
        public UIStateOption UIState { get; private set; } = new(false, false);

        public void LoadUIState()
        {
            UIState = new UIStateOption(
                isDarkMode: Preferences.Get("Theme", true),
                drawerOpen: Preferences.Get("Sidebar", true)
            );
        }

        public void SetUIState(UIStateOption uiState)
        {
            Preferences.Set("Theme", uiState.isDarkMode);
            Preferences.Set("Sidebar", uiState.drawerOpen);

            UIState = uiState;
            OnChange?.Invoke();
        }
    }
}
