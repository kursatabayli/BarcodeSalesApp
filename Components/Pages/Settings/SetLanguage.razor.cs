using Barkod.Constants;
using Barkod.Helpers;
using Barkod.Interfaces;
using Barkod.Resources.Locales;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Barkod.Components.Pages.Settings
{
    public partial class SetLanguage : ComponentBase
    {
        [Inject] ILanguageService LanguageService { get; set; }
        [Inject] IStringLocalizer<Lang> Localizer { get; set; }
        [Inject] INavigationService NavigationService { get; set; }

        private LanguageOption activeLanguage = new();

        protected override void OnInitialized()
        {
            LanguageService.LoadActiveLanguage();
            activeLanguage = LanguageService.Languages.First(lang => lang.IsActive);
        }

        private void ChangeLanguage(string cultureCode)
        {
            LanguageService.SetActiveLanguage(cultureCode);
            activeLanguage = LanguageService.Languages.First(lang => lang.IsActive);
            NavigationService.NavigateTo("/settings", true);
        }
    }
}
