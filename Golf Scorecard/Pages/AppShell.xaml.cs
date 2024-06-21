using Microsoft.Maui.Controls;

namespace GolfScorecard
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));

            NavigateToAboutCommand = new Command(async () => await GoToAsync(nameof(AboutPage)));
            NavigateToSettingsCommand = new Command(async () => await GoToAsync(nameof(SettingsPage)));
        }

        public Command NavigateToAboutCommand { get; }
        public Command NavigateToSettingsCommand { get; }
    }
}
