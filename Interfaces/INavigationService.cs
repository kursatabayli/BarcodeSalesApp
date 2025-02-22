using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barkod.Interfaces
{
    public interface INavigationService
    {
        void NavigateTo(string uri, bool forcereload = false);
    }
}
