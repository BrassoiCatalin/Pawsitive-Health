﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using YourPetsHealth.Interfaces;
using YourPetsHealth.Models;
using YourPetsHealth.Services;
using YourPetsHealth.Utility;
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

        [ObservableProperty]
        private string _email;
        [ObservableProperty]
        private string _password;
        private readonly INavigationService _navigationService;

        #endregion

        #region Commands...

        [RelayCommand]
        private async void LogIn()
        {
            User user = await ApiDatabaseService.DatabaseService.Login(Email, Password);

            if(user == null)
            {
                await App.Current.MainPage.DisplayAlert("Eroare!", "Credentiale invalide!", "OK");
                return;
            }

            ActiveUser.User = user;
            if(user.ClinicId == Guid.Empty)
            {
                ActiveUser.Clinic = null;
            }
            else
            {
                //se ia din baza de date clinica userului curent
                ActiveUser.Clinic = await ApiDatabaseService.DatabaseService.GetClinicByActiveUserId();
            }

            ActiveUser.ProductsToBuy = new List<Product>();
            ActiveUser.Orders = await ApiDatabaseService.DatabaseService.GetAllOrdersByUserId(ActiveUser.User.Id);
            ActiveUser.Appointments = await ApiDatabaseService.DatabaseService.GetAllAppointmentsByUserId(ActiveUser.User.Id);
            ActiveUser.Pets = await ApiDatabaseService.DatabaseService.GetAllPetsByUserId(ActiveUser.User.Id);

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
