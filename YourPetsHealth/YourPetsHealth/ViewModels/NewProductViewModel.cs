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
    public partial class NewProductViewModel : ObservableObject
    {
        #region Constructors...

        public NewProductViewModel()
        {
            _navigationService = new NavigationService();
        }

        #endregion

        #region Properties...

        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private string _price;
        private readonly INavigationService _navigationService;

        #endregion

        #region Commands...

        [RelayCommand]
        private async void AddNewProduct()
        {
            if (!CheckPrice())
            {
                return;
            }

            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Price = Convert.ToDouble(Price),
                ClinicId = ActiveUser.Clinic.Id
            };

            await App.Current.MainPage.DisplayAlert("Succes!", "Produs adaugat cu succes", "OK");
            await ApiDatabaseService.DatabaseService.CreateNewProduct(product);
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

        #endregion

    }
}
