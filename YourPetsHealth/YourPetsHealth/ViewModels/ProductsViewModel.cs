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
    public partial class ProductsViewModel : ObservableObject
    {
        public ProductsViewModel()
        {
            _navigationService = new NavigationService();

            IsTopTextVisible = true;
            ProductsVisible = false;
            Products = new List<Product>();
            InitializePage();
        }

        [ObservableProperty]
        private bool _isTopTextVisible;
        [ObservableProperty]
        private bool _productsVisible;
        [ObservableProperty]
        private List<Product> _products;
        private readonly INavigationService _navigationService;
        [ObservableProperty]
        private Product _selectedProduct;


        [RelayCommand]
        private async void PageAppearing(object obj)
        {
            Products = await ApiDatabaseService.DatabaseService.GetAllProductsByClinicId(ActiveUser.Clinic.Id);

            if (Products.Count == 0)
            {
                return;
            }
            IsTopTextVisible = false;
            ProductsVisible = true;
        }

        [RelayCommand]
        private async void AddNewProduct()
        {
            await _navigationService.PushAsync(new NewProductView());
        }
        [RelayCommand]
        private async void Back()
        {
            await _navigationService.PopAsync();
        }
        [RelayCommand]
        private async void Delete()
        {
            if (SelectedProduct == null)
            {
                await App.Current.MainPage.DisplayAlert("Eroare",
               "Intai trebuie sa selectezi un produs.", "OK");
                return;
            }

            var answer = await App.Current.MainPage.DisplayAlert("Confirma",
                "Esti sigur ca vrei sa stergi acest produs?", "Da", "Nu");

            if (!answer)
            {
                return;
            }
            else
            {
                await ApiDatabaseService.DatabaseService.DeleteProduct(SelectedProduct);
            }

            Products = await ApiDatabaseService.DatabaseService.GetAllProductsByClinicId(ActiveUser.Clinic.Id);
        }

        private async void InitializePage()
        {
            Products = await ApiDatabaseService.DatabaseService.GetAllProductsByClinicId(ActiveUser.Clinic.Id);
        }
    }
}
