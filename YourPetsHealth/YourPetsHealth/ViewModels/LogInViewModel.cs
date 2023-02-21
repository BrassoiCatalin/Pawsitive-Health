using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using YourPetsHealth.Interfaces;
using YourPetsHealth.Services;
using YourPetsHealth.Views;

namespace YourPetsHealth.ViewModels
{
    public partial class LogInViewModel : ObservableObject
    {
        #region Contructors...

        public LogInViewModel()
        {
            _navigationService = new NavigationService();
        }

        #endregion

        #region Private Fields...

        private readonly INavigationService _navigationService;

        #endregion

        #region Commands...

        [RelayCommand]
        private async void LogIn()
        {
            //await _navigationService.PushAsync(new AppShell());
            //await _dialogService.ShowDialog("Ai fost autentificat cu succes.", "Success");

            App.Current.MainPage = new AppShell();
        }

        [RelayCommand]
        private async void SignUp()
        {
            await _navigationService.PushAsync(new SignUpView());
        }

        [RelayCommand]
        private async void ForgotPassword()
        {
            await _navigationService.PushAsync(new AppShell());
        }

        #endregion

    }
}
