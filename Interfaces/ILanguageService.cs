using Barkod.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barkod.Interfaces
{
    public interface ILanguageService
    {
        List<LanguageOption> Languages { get; }
        void SetActiveLanguage(string cultureCode);
        void LoadActiveLanguage();
    }

}
