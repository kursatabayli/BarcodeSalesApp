using BarcodeSalesApp.App.Components.Helpers;
using BarcodeSalesApp.App.Resources.Strings;
using BarcodeSalesApp.Contracts.Options;
using BarcodeSalesApp.Contracts.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace BarcodeSalesApp.App.Components.Pages.Settings;

public partial class EditShortcut : ComponentBase
{
  [Inject] IShortcutService ShortcutService { get; set; } = default!;
  [Inject] NavigationManager NavigationManager { get; set; } = default!;
  [Inject] IDialogService DialogService { get; set; } = default!;
  [Inject] ISnackbar Snackbar { get; set; } = default!;
  [Inject] IStringLocalizer<AppStrings> Localizer { get; set; } = default!;

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
      Snackbar.Add(Localizer[AppStrings.UpdateShortcutSuccess], Severity.Success);
      NavigatePage("/");
    }
    catch
    {
      // Snackbar.Add(Localizer[AppStrings.UpdateShortcutError], Severity.Error);
    }
  }

  private async Task ResetShortcutAsync()
  {
    var parameters = new DialogParameters<DeleteItem>
            {
                { x=> x.ContentText, Localizer[AppStrings.ResetShortcutConfirmText] },
                { x=> x.Title, Localizer[AppStrings.ResetShortcutTitle] },
                { x=> x.SubmitButton, Localizer[AppStrings.Clear] }
            };

    var options = new DialogOptions
    {
      CloseOnEscapeKey = false,
      MaxWidth = MaxWidth.ExtraSmall,
      Position = DialogPosition.Center
    };

    var dialog = await DialogService.ShowAsync<DeleteItem>(null, parameters, options);
    var result = await dialog.Result;

    if (result is not null && !result.Canceled)
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
        Snackbar.Add(Localizer[AppStrings.ResetShortcutSuccess], Severity.Success);
      }
      catch
      {
        Snackbar.Add(Localizer[AppStrings.ResetShortcutError], Severity.Error);
      }
    }
  }

  private void NavigatePage(string url) => NavigationManager.NavigateTo(url);
}
