using BarcodeSalesApp.Contracts.Options;

namespace BarcodeSalesApp.Contracts.Services;

public interface ILanguageService
{
  List<LanguageOption> Languages { get; }
  string CurrentCulture { get; }
  event Func<string, Task>? LanguageChanged;
  void SetActiveLanguage(string cultureCode);
  void LoadActiveLanguage();
}
