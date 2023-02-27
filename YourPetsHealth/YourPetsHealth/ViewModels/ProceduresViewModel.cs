using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using YourPetsHealth.Interfaces;
using YourPetsHealth.Models;
using YourPetsHealth.Services;
using YourPetsHealth.Utility;
using YourPetsHealth.Views;

namespace YourPetsHealth.ViewModels
{
    public partial class ProceduresViewModel : ObservableObject
    {
        public ProceduresViewModel()
        {
            _navigationService = new NavigationService();

            IsTopTextVisible = true;
            ProceduresVisible = false;
            Procedures = new List<Procedure>();
            InitializePage();
        }

        [ObservableProperty]
        private bool _isTopTextVisible;
        [ObservableProperty]
        private bool _proceduresVisible;
        [ObservableProperty]
        private List<Procedure> _procedures;
        private readonly INavigationService _navigationService;
        [ObservableProperty]
        private Procedure _selectedProcedure;

        [RelayCommand]
        private async void PageAppearing(object obj)
        {
            Procedures = await ApiDatabaseService.DatabaseService.GetAllProceduresByClinicId(ActiveUser.Clinic.Id);

            if (Procedures.Count == 0)
            {
                return;
            }
            IsTopTextVisible = false;
            ProceduresVisible = true;
        }

        [RelayCommand]
        private async void AddNewProcedure()
        {
            await _navigationService.PushAsync(new NewProcedureView());
        }
        [RelayCommand]
        private async void Back()
        {
            await _navigationService.PopAsync();
        }
        [RelayCommand]
        private async void Delete()
        {
            if(SelectedProcedure == null)
            {
                await App.Current.MainPage.DisplayAlert("Atentie",
               "Intai trebuie sa selectezi un serviciu.", "OK");
                return;
            }

            var answer = await App.Current.MainPage.DisplayAlert("Confirma", 
                "Esti sigur ca vrei sa stergi acest serviciu?", "Da", "Nu");
            
            if(!answer)
            {
                return;
            }
            else
            {
                await ApiDatabaseService.DatabaseService.DeleteProcedure(SelectedProcedure);
            }

            Procedures = await ApiDatabaseService.DatabaseService.GetAllProceduresByClinicId(ActiveUser.Clinic.Id);
        }

        private async void InitializePage()
        {
            Procedures = await ApiDatabaseService.DatabaseService.GetAllProceduresByClinicId(ActiveUser.Clinic.Id);
        }
    }
}
