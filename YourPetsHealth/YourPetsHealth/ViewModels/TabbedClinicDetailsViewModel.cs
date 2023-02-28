using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using YourPetsHealth.Models;
using YourPetsHealth.Services;

namespace YourPetsHealth.ViewModels
{
    public partial class TabbedClinicDetailsViewModel : ObservableObject
    {
        public TabbedClinicDetailsViewModel()
        {
            
        }

        public TabbedClinicDetailsViewModel(Clinic clinic)
        {
            InitializePages(clinic);
            var x = 9;
        }

        [ObservableProperty]
        private User _clinicOwner;
        [ObservableProperty]
        private List<Product> _productsList;
        [ObservableProperty]
        private List<Procedure> _proceduresList;

        private async void InitializePages(Clinic clinic)
        {
            ClinicOwner = await ApiDatabaseService.DatabaseService.GetUserByClinicId(clinic.Id);
            ProductsList = await ApiDatabaseService.DatabaseService.GetAllProductsByClinicId(clinic.Id);
            ProceduresList = await ApiDatabaseService.DatabaseService.GetAllProceduresByClinicId(clinic.Id);
            var x = 9;
        }
    }
}
