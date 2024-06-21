using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace GolfScorecard
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Setting MainPage to an instance of AppShell for Shell navigation
            // MainPage = new AppShell();
            // OR, if not using Shell, set to MainPage or another page:
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
