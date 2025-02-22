using Barkod.Interfaces;

namespace Barkod
{
    public partial class App : Application
    {
        public App(IServiceProvider services)
        {
            InitializeComponent();

            // Servisleri burada kullanın
            var languageService = services.GetRequiredService<ILanguageService>();
            languageService.LoadActiveLanguage();

            var shortcutService = services.GetRequiredService<IShortcutService>();
            shortcutService.LoadShortcuts();

            var uiStateService = services.GetRequiredService<IUIStateService>();
            uiStateService.LoadUIState();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage()) { Title = "Barkod" };
        }
    }
}
