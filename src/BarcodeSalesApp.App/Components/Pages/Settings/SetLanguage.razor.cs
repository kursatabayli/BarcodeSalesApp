using BarcodeSalesApp.App.Resources.Strings;
using BarcodeSalesApp.Contracts.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace BarcodeSalesApp.App.Components.Pages.Settings;

public partial class SetLanguage : ComponentBase
{
  [CascadingParameter] IMudDialogInstance MudDialog { get; set; } = default!;
  [Inject] ILanguageService LanguageService { get; set; } = default!;
  [Inject] IStringLocalizer<AppStrings> Localizer { get; set; } = default!;
  private string _selectedCulture = string.Empty;
  protected override void OnInitialized()
  {
    LanguageService.Languages.OrderBy(l => l.DisplayName);
    _selectedCulture = LanguageService.CurrentCulture;
    base.OnInitialized();
  }
  private void ChangeLanguage()
  {
    LanguageService.SetActiveLanguage(_selectedCulture);
    MudDialog.Close(DialogResult.Ok(true));
  }
  private static string GetImgPath(string cultureCode)
  {
    return $"/Images/Flags/{cultureCode}.png";
  }
  private void Cancel() => MudDialog.Cancel();
}
