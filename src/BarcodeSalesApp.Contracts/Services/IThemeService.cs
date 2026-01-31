namespace BarcodeSalesApp.Contracts.Services;

public interface IThemeService
{
  bool IsDarkMode { get; }
  event Func<Task> OnThemeChanged;

  void LoadTheme();

  void SetTheme(bool isDarkMode);
}
