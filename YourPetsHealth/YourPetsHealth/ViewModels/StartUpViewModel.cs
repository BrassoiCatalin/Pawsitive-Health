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

            _ordersFromDatabase = FilterOrders(await ApiDatabaseService.DatabaseService.GetAllOrdersByUserId(ActiveUser.User.Id));

            foreach (var item in _ordersFromDatabase)
            {
                var orderToShow = new OrdersToShow
                {
                    AllItems = item.AllProducts.Aggregate("", (current, s) => current + (s + "; ")),
                    Price = item.TotalPrice,
                    ArriveDate = item.ArriveDate
                };
                orderToShow.AllItems = orderToShow.AllItems.Remove(orderToShow.AllItems.Length - 2, 2);
                _ordersToShowLocal.Add(orderToShow);
            }
            AllOrdersToShow = _ordersToShowLocal;

            AllAppointments = FilterAppointments(await ApiDatabaseService.DatabaseService.GetAllAppointmentsByUserId(ActiveUser.User.Id));
            foreach (var item in AllAppointments)
            {
                item.Procedures = item.Procedures.Remove(item.Procedures.Length - 2, 2);
            }
        }

        private List<Order> FilterOrders(List<Order> orders)
        {
            return orders.Where(x => x.ArriveDate > DateTime.UtcNow).ToList();
        }

        private List<Appointment> FilterAppointments(List<Appointment> appointments)
        {
            return appointments.Where(x => x.StartDateTime > DateTime.UtcNow).ToList();
        }

        [RelayCommand]
        private async void Refresh()
        {
            IsBusy = true;

            InitializePage();

            IsBusy = false;
            //edit animal profiles
            //guides
            //profile page for master detail page
            //forgot password (maybe?)
            //about page
            //background image from Tinel
        }
    }
}
