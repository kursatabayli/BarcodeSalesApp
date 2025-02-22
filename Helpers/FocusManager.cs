using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barkod.Helpers
{
    public class FocusManager
    {
        private MudNumericField<long?> _inputField;
        private bool _suspendFocus = false;

        public void Initialize(MudNumericField<long?> inputField)
        {
            _inputField = inputField;
            _suspendFocus = false;

        }

        public async Task FocusInputAsync()
        {
            if (_inputField != null && !_suspendFocus)
            {
                await Task.Delay(50);
                await _inputField.FocusAsync();
            }
        }
        public async Task BlurInputAsync()
        {
            if (_inputField != null)
                await _inputField.BlurAsync();
        }
        public async Task HandleBlurAsync()
        {
            if (!_suspendFocus && _inputField != null)
            {
                await Task.Delay(50);
                await _inputField.FocusAsync();
            }
        }

        public async Task OnSelectClosed()
        {
            ChangeFocus();

            await FocusInputAsync();
        }


        public void ChangeFocus() => _suspendFocus = !_suspendFocus;


        public void Dispose() => _inputField = null;
    }
}
