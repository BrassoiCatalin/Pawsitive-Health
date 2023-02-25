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
    public partial class ProductsViewModel : ObservableObject
    {
        public ProductsViewModel()
        {
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

        [RelayCommand]
        private async void PageAppearing(object obj)
        {
            Products = await ApiDatabaseService.DatabaseService.GetAllProductsByClinicId(ActiveUser.Clinic.Id);
            //InitializePage();
            //facem un get la toate produsele;
            //daca e null, atunci return
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

        }

        private async void InitializePage()
        {
            Products = await ApiDatabaseService.DatabaseService.GetAllProductsByClinicId(ActiveUser.Clinic.Id);
        }
    }
}
