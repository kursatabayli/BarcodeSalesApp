using System.Globalization;
using BarcodeSalesApp.Contracts.Options;
using BarcodeSalesApp.Contracts.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BarcodeSalesApp.App.Services;

public class LanguageService : ILanguageService
{
  private const string DefaultCulture = "en-US";
  private readonly IPreferences _preferences;
  private readonly LocalizationSettings _settings;
  public List<LanguageOption> Languages { get; private set; }

  public LanguageService(IPreferences preferences, IOptions<LocalizationSettings> settings)
  {
    _preferences = preferences;
    _settings = settings.Value;
    Languages = _settings.SupportedLanguages.OrderBy(l => l.DisplayName).ToList() ?? [];
  }


  public string CurrentCulture { get; private set; } = DefaultCulture;

  public event Func<string, Task>? LanguageChanged;

  public void LoadActiveLanguage()
  {
    string cultureCode = _preferences.Get("SelectedLanguage", string.Empty);

    if (string.IsNullOrWhiteSpace(cultureCode))
    {
      cultureCode = CultureInfo.CurrentUICulture.Name;
    }

    if (!Languages.Any(l => l.CultureCode == cultureCode))
    {
      cultureCode = DefaultCulture;
    }

    ApplyCulture(cultureCode);
    LanguageChanged?.Invoke(cultureCode);
  }

  public void SetActiveLanguage(string cultureCode)
  {
    if (CurrentCulture == cultureCode)
      return;

    ApplyCulture(cultureCode);

    _preferences.Set("SelectedLanguage", cultureCode);
    LanguageChanged?.Invoke(cultureCode);
  }

  private void ApplyCulture(string cultureCode)
  {
    try
    {
      var cultureInfo = new CultureInfo(cultureCode);
      CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
      CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

      CurrentCulture = cultureCode;
    }
    catch (Exception)
    {
      if (CurrentCulture != DefaultCulture)
      {
        ApplyCulture(DefaultCulture);
      }
    }
  }
}


