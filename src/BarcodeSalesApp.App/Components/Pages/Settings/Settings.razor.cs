using BarcodeSalesApp.App.Resources.Strings;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace BarcodeSalesApp.App.Components.Pages.Settings;

public partial class Settings : ComponentBase
{
  [Inject] NavigationManager NavigationManager { get; set; } = default!;
  [Inject] IStringLocalizer<AppStrings> Localizer { get; set; } = default!;
  [Inject] IDialogService DialogService { get; set; } = default!;

  private async Task ChangeLanguage()
  {

    var dialog = await DialogService.ShowAsync<SetLanguage>();
    var result = await dialog.Result;

    if (result is not null && !result.Canceled)
    {
      NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
    }
  }
}
