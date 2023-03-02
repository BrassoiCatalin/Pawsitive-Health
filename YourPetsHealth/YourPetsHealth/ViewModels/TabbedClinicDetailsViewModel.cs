using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using YourPetsHealth.Models;
using YourPetsHealth.Services;
using YourPetsHealth.Utility;

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
        }

        [RelayCommand]
        private async void BuyProduct(object obj)
        {
            if (obj == null)
            {
                return;
            }

            Product newProduct = (Product)obj;

            if (ActiveUser.ProductsToBuy.Count >= 1 
                && !newProduct.ClinicId.Equals(ActiveUser.ProductsToBuy[0].ClinicId))
            {
                await App.Current.MainPage.DisplayAlert("Atentie!"
                    , "Nu e posibil sa cumpreri deaodata produse de la mai multe clinici."
                    , "OK");
                return;
            }    

            ActiveUser.ProductsToBuy.Add((Product)obj);
            await App.Current.MainPage.DisplayAlert("Succes", "Produs adaugat cu succes!", "OK");
        }
    }
}
