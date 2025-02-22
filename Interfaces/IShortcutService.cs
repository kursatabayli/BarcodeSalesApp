using Barkod.Constants;

namespace Barkod.Interfaces
{
    public interface IShortcutService
    {
        ShortcutOption Shortcut { get; }
        void SetShortcuts(ShortcutOption shortcut);
        void LoadShortcuts();
    }
}
