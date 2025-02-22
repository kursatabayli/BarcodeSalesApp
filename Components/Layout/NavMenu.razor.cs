using Barkod.Constants;
using Barkod.Interfaces;
using Barkod.Resources.Locales;
using Barkod.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barkod.Components.Layout
{
    public partial class NavMenu : ComponentBase, IDisposable
    {
        [Inject] IUIStateService UIStateService { get; set; }
        [Inject] IStringLocalizer<Lang> Localizer { get; set; }
        private UIStateOption UIStateOption { get; set; }

        protected override void OnInitialized()
        {
            UIStateService.OnChange += HandleStateChange;
            UIStateService.LoadUIState();
            UIStateOption = UIStateService.UIState;
            base.OnInitialized();
        }

        private async Task HandleDrawerOpenChanged(bool newValue)
        {
            var newState = UIStateOption with { drawerOpen = newValue };
            UIStateService.SetUIState(newState);
        }

        private void HandleStateChange()
        {
            UIStateOption = UIStateService.UIState;
            StateHasChanged();
        }

        public void Dispose() => UIStateService.OnChange -= HandleStateChange;
    }
}
