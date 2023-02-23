using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using YourPetsHealth.Services;

namespace YourPetsHealth.ViewModels
{
    public partial class NewClinicViewModel : ObservableObject
    {
        #region Contructor...

        public NewClinicViewModel()
        {
            _navigationService = new NavigationService();
        }

        #endregion

        #region Private Fields...

        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private string _startHour;
        [ObservableProperty]
        private string _endHour;
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
            await App.Current.MainPage.DisplayAlert("Succes", "Clinica a fost adaugata cu succes!", "OK");
            await _navigationService.PopAsync();
        }

        [RelayCommand]
        private async void Cancel()
        {
            await _navigationService.PopAsync();
        }

        #endregion
    }
}
