using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barkod.Helpers
{
    public static class SearchManager
    {
        public static IEnumerable<T> FilterBySearchText<T>(this IEnumerable<T> source, string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText)) return source;

            return source.Where(item =>
                (item?.GetPropertyValue("Barcode") ?? "").Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                (item?.GetPropertyValue("Name") ?? "").Contains(searchText, StringComparison.OrdinalIgnoreCase));
        }

        private static string GetPropertyValue<T>(this T item, string propertyName)
        {
            return item?.GetType()
                       .GetProperty(propertyName)?
                       .GetValue(item, null)?
                       .ToString() ?? "";
        }
    }
}
