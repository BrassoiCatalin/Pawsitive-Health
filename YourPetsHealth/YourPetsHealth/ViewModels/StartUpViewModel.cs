using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YourPetsHealth.Models;
using YourPetsHealth.Services;
using YourPetsHealth.Utility;

namespace YourPetsHealth.ViewModels
{
    public partial class StartUpViewModel : ObservableObject
    {
        public StartUpViewModel()
        {
            InitializePage();
        }

        [ObservableProperty]
        private List<OrdersToShow> _allOrdersToShow;
        [ObservableProperty]
        private List<Appointment> _allAppointments;
        [ObservableProperty]
        private bool _isBusy;

        private List<Order> _ordersFromDatabase;

        private async void InitializePage()
        {
            var _ordersToShowLocal = new List<OrdersToShow>();

            _ordersFromDatabase = await ApiDatabaseService.DatabaseService.GetAllOrdersByUserId(ActiveUser.User.Id);

            foreach (var item in _ordersFromDatabase)
            {
                var orderToShow = new OrdersToShow
                {
                    AllItems = item.AllProducts.Aggregate("", (current, s) => current + (s + "; ")),
                    Price = item.TotalPrice
                };
                orderToShow.AllItems = orderToShow.AllItems.Remove(orderToShow.AllItems.Length - 2, 2);
                _ordersToShowLocal.Add(orderToShow);
            }
            AllOrdersToShow = _ordersToShowLocal;

            AllAppointments = await ApiDatabaseService.DatabaseService.GetAllAppointmentsByUserId(ActiveUser.User.Id);
            foreach (var item in AllAppointments)
            {
                item.Procedures = item.Procedures.Remove(item.Procedures.Length - 2, 2);
            }
        }

        [RelayCommand]
        private async void Refresh()
        {
            IsBusy = true;

            InitializePage();

            IsBusy = false;
            //fix hour display everywhere
            //better design manage clinic view
            //filter orders/appointments so we only show those in the future
            //background image from Tinel
        }
    }
}
