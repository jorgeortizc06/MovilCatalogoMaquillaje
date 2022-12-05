using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App4._2
{
    public partial class App : Application
    {
        public static new App Current { get; set; }

        public string LogonName { get; set; }

        public App()
        {
            InitializeComponent();

            Current = this;

            LoginFlow();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public void MainFlow()
        {
            var mainPage = new MainPage();

            MainPage = mainPage;

            mainPage.SetInitialPage();
        }

        public void LoginFlow()
        {
            MainPage = new NavigationPage(new LoginPage())
            {
                BarBackgroundColor = Color.Navy,
                BarTextColor = Color.White
            };
        }

        public void SessionClose()
        {           
            LoginFlow();
        }
    }
}
