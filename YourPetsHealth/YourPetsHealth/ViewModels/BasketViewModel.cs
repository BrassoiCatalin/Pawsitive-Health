using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourPetsHealth.Models;
using YourPetsHealth.Services;
using YourPetsHealth.Utility;

namespace YourPetsHealth.ViewModels
{
    public partial class BasketViewModel : ObservableObject
    {
        #region Constructors...

        public BasketViewModel()
        {
            
        }

        #endregion

        #region Properties...

        [ObservableProperty]
        private List<Product> _products;
        [ObservableProperty]
        private double _totalPrice;

        #endregion

        #region Commands...

        [RelayCommand]
        private void PageAppearing()
        {
            Products = null;
            Products = ActiveUser.ProductsToBuy;
            //da eroare cand nu avem produse in cos!!!
            TotalPrice = Products.Sum(x => x.Price);
        }

        [RelayCommand]
        private void DeleteProductFromBasket(object obj)
        {
            ActiveUser.ProductsToBuy.Remove((Product)obj);

            Products = null;
            Products = ActiveUser.ProductsToBuy;
            TotalPrice = Products.Sum(x => x.Price);
        }

        [RelayCommand]
        private async void AddNewOrder()
        {
            if(Products.Count == 0)
            {
                await App.Current.MainPage.DisplayAlert("Eroare", "Cosul de cumparaturi este gol!", "OK");
                return;
            }

            var newOrder = new Order()
            {
                Id = Guid.NewGuid(),
                UserId = ActiveUser.User.Id,
                ClinicId = Products[0].ClinicId,
                CreateDate = DateTime.Now,
                ArriveDate = DateTime.Now.AddDays(2),
            };

            if(newOrder.ArriveDate.DayOfWeek == DayOfWeek.Saturday)
            {
                newOrder.ArriveDate = newOrder.ArriveDate.AddDays(2);
            }
            else if(newOrder.ArriveDate.DayOfWeek == DayOfWeek.Sunday)
            {
                newOrder.ArriveDate = newOrder.ArriveDate.AddDays(1);
            }

            newOrder.TotalPrice = Products.Sum(x => x.Price);
            newOrder.AllProducts = new List<string>();

            foreach (var item in Products)
            {
                newOrder.AllProducts.Add(item.Name);
            }

            ActiveUser.Orders.Add(newOrder);
            await ApiDatabaseService.DatabaseService.CreateNewOrder(newOrder);
            await App.Current.MainPage.DisplayAlert("Succes!", "Comanda a fost plasata cu success!", "OK");

            ActiveUser.ProductsToBuy.RemoveRange(0, ActiveUser.ProductsToBuy.Count);
            Products = null;
            Products = ActiveUser.ProductsToBuy;
            TotalPrice = Products.Sum(x => x.Price);
        }

        #endregion

    }
}
