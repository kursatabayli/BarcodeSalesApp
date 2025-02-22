using Barkod.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barkod.Interfaces
{
    public interface IUIStateService
    {
        event Action OnChange;
        UIStateOption UIState { get; }
        void SetUIState(UIStateOption uiState);
        void LoadUIState();
    }
}
