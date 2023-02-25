using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using YourPetsHealth.Interfaces;
using YourPetsHealth.Services;
using YourPetsHealth.Utility;
using YourPetsHealth.Views;

namespace YourPetsHealth.ViewModels
{
    public partial class ManageClinicViewModel : ObservableObject
    {
        #region Constructors...

        public ManageClinicViewModel()
        {
            _navigationService = new NavigationService();

            AreTopTextAndButtonVisible = true;
            IsExistingClinicVisible = false;

            ClinicName = ActiveUser.Clinic.Name;
            ClinicStartHour = ActiveUser.Clinic.StartHour;
            ClinicEndHour = ActiveUser.Clinic.EndHour;
            ClinicCity = ActiveUser.Clinic.Address.City;
            ClinicStreet = ActiveUser.Clinic.Address.Street;
            ClinicNumber = ActiveUser.Clinic.Address.Number;
        }

        #endregion

        #region Private Fields...

        [ObservableProperty]
        private bool _areTopTextAndButtonVisible;
        [ObservableProperty]
        private bool _isExistingClinicVisible;
        private readonly INavigationService _navigationService;
        [ObservableProperty]
        private string _clinicName;
        [ObservableProperty]
        private TimeSpan _clinicStartHour;
        [ObservableProperty]
        private TimeSpan _clinicEndHour;
        [ObservableProperty]
        private string _clinicCity;
        [ObservableProperty]
        private string _clinicStreet;
        [ObservableProperty]
        private string _clinicNumber;

        #endregion

        #region Commands...

        [RelayCommand]
        private async void AddNewClinic()
        {
            await _navigationService.PushAsync(new NewClinicView());
        }

        [RelayCommand]
        private void PageAppearing(object obj)
        {
            if(ActiveUser.Clinic is null)
            {
                return;
            }
            AreTopTextAndButtonVisible = false;
            IsExistingClinicVisible = true;
        }

        [RelayCommand]
        private async void ViewProducts()
        {
            await _navigationService.PushAsync(new ProductsView());
        }

        [RelayCommand]
        private async void ViewProcedures()
        {
            await _navigationService.PushAsync(new NewClinicView());
        }

        [RelayCommand]
        private async void ChangeSchedule()
        {
            await _navigationService.PushAsync(new NewClinicView());
        }

        [RelayCommand]
        private async void DeleteClinic()
        {
            await _navigationService.PushAsync(new NewClinicView());
        }

        #endregion
    }
}
