using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using YourPetsHealth.Interfaces;
using YourPetsHealth.Models;
using YourPetsHealth.Services;
using YourPetsHealth.Utility;

namespace YourPetsHealth.ViewModels
{
    public partial class NewProcedureViewModel : ObservableObject
    {
        #region Constructors...

        public NewProcedureViewModel()
        {
            _navigationService = new NavigationService();
        }

        #endregion

        #region Properties...

        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private string _price;
        [ObservableProperty]
        private string _time;
        private readonly INavigationService _navigationService;

        #endregion

        #region Commands...

        [RelayCommand]
        private async void AddNewProcedure()
        {
            if (!CheckPrice() || !CheckTime())
            {
                return;
            }

            var procedure = new Procedure()
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Price = Convert.ToDouble(Price),
                Time = Convert.ToInt32(Time),
                ClinicId = ActiveUser.Clinic.Id
            };

            await App.Current.MainPage.DisplayAlert("Succes!", "Serviciu adaugat cu succes", "OK");
            await ApiDatabaseService.DatabaseService.CreateNewProcedure(procedure);
        }

        [RelayCommand]
        private async void Cancel()
        {
            await _navigationService.PopAsync();
        }

        #endregion

        #region Private Methods...

        private bool CheckPrice()
        {
            bool isMatch = Regex.IsMatch(Price, @"^[+-]?\d+(\.\d+)?$");

            if (!isMatch)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Pretul nu poate contine caractere invalide!", "OK");
                return false;
            }
            return true;
        }

        private bool CheckTime()
        {
            bool isMatch = Regex.IsMatch(Time, "^[0-9]+$");

            if (!isMatch)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Timpul trebuie sa fie un numar intreg!", "OK");
                return false;
            }
            return true;
        }

        #endregion

    }
}
