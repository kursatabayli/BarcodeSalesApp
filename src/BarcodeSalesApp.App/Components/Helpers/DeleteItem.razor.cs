using BarcodeSalesApp.App.Resources.Strings;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace BarcodeSalesApp.App.Components.Helpers;

public partial class DeleteItem : ComponentBase
{
  [Inject] private IStringLocalizer<AppStrings> Localizer { get; set; } = null!;
  [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;

  [Parameter] public required string Title { get; set; }
  [Parameter] public required string ContentText { get; set; }

  [Parameter] public required string SubmitButton { get; set; }

  private void Submit() => MudDialog.Close(DialogResult.Ok(true));
  private void Cancel() => MudDialog.Cancel();
}
