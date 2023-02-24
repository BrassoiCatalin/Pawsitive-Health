using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using YourPetsHealth.Models;
using YourPetsHealth.Services;
using YourPetsHealth.Utility;

namespace YourPetsHealth.ViewModels
{
    public partial class NewClinicViewModel : ObservableObject
    {
        #region Contructor...

        public NewClinicViewModel()
        {
            _navigationService = new NavigationService();

            var guid = Guid.NewGuid();
            //
        }

        #endregion

        #region Private Fields...

        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private TimeSpan _startHour;
        [ObservableProperty]
        private TimeSpan _endHour;
        [ObservableProperty]
        private string _city;
        [ObservableProperty]
        private string _street;
        [ObservableProperty]
        private string _number;

        private NavigationService _navigationService;

        #endregion

        #region Commands...

        [RelayCommand]
        private async void AddNewClinic()
        {
            Address address = new Address()
            {
                Id = Guid.NewGuid(),
                City = City,
                Street = Street,
                Number = Number,
            };

            var newClinic = new Clinic()
            {
                Id = Guid.NewGuid(),
                Name = Name,
                StartHour = StartHour,
                EndHour = EndHour,
                Address = address
            };

            ActiveUser.Clinic = newClinic;
            await ApiDatabaseService.DatabaseService.CreateNewClinic(newClinic);
            await ApiDatabaseService.DatabaseService.UpdateClinicIdForUser(newClinic.Id);
            await App.Current.MainPage.DisplayAlert("Succes", "Clinica a fost adaugata cu succes!", "OK");
            await _navigationService.PopAsync();
        }

        [RelayCommand]
        private async void Cancel()
        {
            await _navigationService.PopAsync();
        }

        #endregion

        #region Private Methods...

        

        #endregion
    }
}
