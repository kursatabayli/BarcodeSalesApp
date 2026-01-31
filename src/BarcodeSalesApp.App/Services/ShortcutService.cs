using BarcodeSalesApp.Contracts.Options;
using BarcodeSalesApp.Contracts.Services;

namespace BarcodeSalesApp.App.Services;

public class ShortcutService : IShortcutService
{
  private readonly IPreferences _preferences;
  public ShortcutOption Shortcut { get; private set; } = null!;

  public ShortcutService(IPreferences preferences)
  {
    _preferences = preferences;
    LoadShortcuts();
  }

  public void SetShortcuts(ShortcutOption shortcut)
  {
    _preferences.Set("SaleKey", shortcut.SaleKey);
    _preferences.Set("ClearKey", shortcut.ClearKey);

    Shortcut = shortcut;
  }

  public void LoadShortcuts()
  {
    var saleKey = _preferences.Get("SaleKey", string.Empty);
    var clearKey = _preferences.Get("ClearKey", string.Empty);

    Shortcut = new ShortcutOption
    {
      SaleKey = saleKey,
      ClearKey = clearKey
    };
  }

}
