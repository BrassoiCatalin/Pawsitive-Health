using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class BrowseClinicsViewModel : ObservableObject
    {
        #region Constructors...

        public BrowseClinicsViewModel()
        {
            _navigationService = new NavigationService();
        }

        #endregion

        #region Properties...

        private INavigationService _navigationService;
        [ObservableProperty]
        private List<Clinic> _clinics;
        [ObservableProperty]
        private Clinic _selectedClinic;

        #endregion

        #region Commands...

        [RelayCommand]
        private async void PageAppearing(object obj)
        {
            SelectedClinic = null;
            Clinics = await ApiDatabaseService.DatabaseService.GetAllClinics();
            //if (Clinics.Count == 0)
            //{
            //    return;
            //}
            //IsTopTextVisible = false;
            //ProceduresVisible = true;
        }

        [RelayCommand]
        private async void SeeDetails(object obj)
        {
            if(SelectedClinic == null)
            {
                await App.Current.MainPage.DisplayAlert("Atentie",
               "Intai trebuie sa selectezi o clinica.", "OK");
                return;
            }
            
            await _navigationService.PushAsync(new TabbedClinicDetailsView(SelectedClinic));
        }

        #endregion

    }
}
