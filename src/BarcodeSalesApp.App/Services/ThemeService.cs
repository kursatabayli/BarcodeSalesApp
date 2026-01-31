using BarcodeSalesApp.Contracts.Services;

namespace BarcodeSalesApp.App.Services;

public class ThemeService : IThemeService
{
  private readonly IPreferences _preferences;
  public bool IsDarkMode { get; private set; } = false;
  public event Func<Task>? OnThemeChanged;
  public ThemeService(IPreferences preferences)
  {
    _preferences = preferences;
  }

  public void LoadTheme()
  {
    IsDarkMode = _preferences.Get("IsDarkMode", false);
  }

  public void SetTheme(bool isDarkMode)
  {
    if (IsDarkMode == isDarkMode)
      return;

    IsDarkMode = isDarkMode;

    _preferences.Set("IsDarkMode", IsDarkMode);
    OnThemeChanged?.Invoke();
  }
}
