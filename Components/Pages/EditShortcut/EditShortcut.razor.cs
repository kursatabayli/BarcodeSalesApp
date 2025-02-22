using Microsoft.AspNetCore.Components;
using Barkod.Interfaces;
using Barkod.Models;
using System.Threading.Tasks;
using MudBlazor;
using Barkod.Helpers;
using Microsoft.Extensions.Localization;
using Barkod.Resources.Locales;
using Barkod.Constants;
using Barkod.Services;

namespace Barkod.Components.Pages.EditShortcut
{
    public partial class EditShortcut : ComponentBase
    {
        [Inject] IShortcutService ShortcutService { get; set; }
        [Inject] INavigationService NavigationService { get; set; }
        [Inject] IDialogService DialogService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }        
        [Inject] IStringLocalizer<Lang> Localizer { get; set; }

        private ShortcutOption currentShortcut = new();

        protected override void OnInitialized()
        {
            ShortcutService.LoadShortcuts();
            currentShortcut = ShortcutService.Shortcut;
        }

        private async Task UpdateShortcutAsync()
        {
            try
            {
                ShortcutService.SetShortcuts(currentShortcut);
                Snackbar.Add(Localizer["UpdateShortcutSuccess"], Severity.Success);
                NavigatePage("/");
            }
            catch
            {
                Snackbar.Add(Localizer["UpdateShortcutError"], Severity.Error);
            }
        }

        private async Task ResetShortcutAsync()
        {
            var parameters = new DialogParameters
            {
                { "ContentText", Localizer["ResetShortcutConfirmText"] },
                { "Title", Localizer["ResetShortcutTitle"] },
                { "SubmitButton", Localizer["Clear"] }
            };

            var options = new DialogOptions
            {
                CloseOnEscapeKey = false,
                MaxWidth = MaxWidth.ExtraSmall,
                Position = DialogPosition.Center
            };

            var dialog = await DialogService.ShowAsync<DeleteItem>(null, parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                try
                {
                    ShortcutService.SetShortcuts(new ShortcutOption
                    {
                        SaleKey = string.Empty,
                        ClearKey = string.Empty
                    });
                    currentShortcut = ShortcutService.Shortcut;
                    StateHasChanged();
                    Snackbar.Add(Localizer["ResetShortcutSuccess"], Severity.Success);
                }
                catch
                {
                    Snackbar.Add(Localizer["ResetShortcutError"], Severity.Error);
                }
            }
        }

        private void NavigatePage(string url) => NavigationService.NavigateTo(url);

    }
}
