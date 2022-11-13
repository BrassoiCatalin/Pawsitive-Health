using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YourPetsHealth.Views;

namespace YourPetsHealth
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SignInView();
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
    }
}
