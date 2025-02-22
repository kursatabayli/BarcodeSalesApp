using Barkod.Resources.Locales;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barkod.Helpers
{
    public partial class DeleteItem : ComponentBase
    {
        [Inject] IStringLocalizer<Lang> Localizer { get; set; }
        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string ContentText { get; set; }
        
        [Parameter]
        public string SubmitButton { get; set; }

        private void Submit() => MudDialog.Close(DialogResult.Ok(true));
        private void Cancel() => MudDialog.Cancel();
    }
}
