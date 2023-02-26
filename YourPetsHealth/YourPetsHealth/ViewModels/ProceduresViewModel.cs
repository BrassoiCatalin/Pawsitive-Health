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

        [RelayCommand]
        private async void PageAppearing(object obj)
        {
            Procedures = await ApiDatabaseService.DatabaseService.GetAllProceduresByClinicId(ActiveUser.Clinic.Id);
            //InitializePage();
            //facem un get la toate produsele;
            //daca e null, atunci return
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

        private async void InitializePage()
        {
            Procedures = await ApiDatabaseService.DatabaseService.GetAllProceduresByClinicId(ActiveUser.Clinic.Id);
        }
    }
}
