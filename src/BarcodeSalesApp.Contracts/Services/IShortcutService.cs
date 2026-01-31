using BarcodeSalesApp.Contracts.Options;

namespace BarcodeSalesApp.Contracts.Services;

public interface IShortcutService
{
  ShortcutOption Shortcut { get; }
  void SetShortcuts(ShortcutOption shortcut);
  void LoadShortcuts();
}
