using Barkod.Constants;
using Barkod.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barkod.Services
{
    public class LanguageService : ILanguageService
    {
        public List<LanguageOption> Languages { get; private set; } = new List<LanguageOption>        
        {
            new LanguageOption { CultureCode = "tr-TR", IsActive = false },
            new LanguageOption { CultureCode = "en-US", IsActive = false },
        };


        public void SetActiveLanguage(string cultureCode)
        {
            var activeLanguage = Languages.Find(lang => lang.IsActive);
            if (activeLanguage != null)
                activeLanguage.IsActive = false;

            var newLanguage = Languages.FirstOrDefault(lang => lang.CultureCode == cultureCode);
            if (newLanguage != null && !newLanguage.IsActive)
            {
                newLanguage.IsActive = true;
                var cultureInfo = new System.Globalization.CultureInfo(newLanguage.CultureCode);
                System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
                Preferences.Set("SelectedLanguage", cultureCode);
            }
        }

        public void LoadActiveLanguage()
        {
            var savedCultureCode = Preferences.Get("SelectedLanguage", "en-US");
            SetActiveLanguage(savedCultureCode);
        }

    }
}
