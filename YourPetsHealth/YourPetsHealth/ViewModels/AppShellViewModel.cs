using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using YourPetsHealth.Models;
using YourPetsHealth.Utility;
using YourPetsHealth.Views;

namespace YourPetsHealth.ViewModels
{
    public partial class AppShellViewModel : ObservableObject
    {
        #region Constructor...

        public AppShellViewModel()
        {

        }

        #endregion

        #region Commands... 

        [RelayCommand]
        private void LogOut()
        {
            ActiveUser.User = new User();
            ActiveUser.Clinic = null;
            App.Current.MainPage = new NavigationPage(new LogInView());
        }

        #endregion

    }
}
