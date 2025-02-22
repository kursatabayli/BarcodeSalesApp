using Barkod.Constants;
using Barkod.Interfaces;
using Microsoft.Windows.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;

namespace Barkod.Services
{
    public class ShortcutService : IShortcutService
    {
        public ShortcutOption Shortcut { get; private set; } = new();

        public void SetShortcuts(ShortcutOption shortcut)
        {
            Preferences.Set("SaleKey", shortcut.SaleKey);
            Preferences.Set("ClearKey", shortcut.ClearKey);

            Shortcut = shortcut;
        }

        public void LoadShortcuts()
        {
            var saleKey = Preferences.Get("SaleKey", string.Empty);
            var clearKey = Preferences.Get("ClearKey", string.Empty);

            Shortcut = new ShortcutOption
            {
                SaleKey = saleKey,
                ClearKey = clearKey
            };
        }

    }
}
