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
    public partial class StartUpViewModel : ObservableObject
    {
        public StartUpViewModel()
        {
            InitializePage();
        }

        [ObservableProperty]
        private List<Order> _allOrders;
        [ObservableProperty]
        private List<Appointment> _allAppointments;
        [ObservableProperty]
        private bool _isBusy;

        private async void InitializePage()
        {
            AllOrders = await ApiDatabaseService.DatabaseService.GetAllOrdersByUserId(ActiveUser.User.Id);
            AllAppointments = await ApiDatabaseService.DatabaseService.GetAllAppointmentsByUserId(ActiveUser.User.Id);
        }

        [RelayCommand]
        private async void Refresh()
        {
            IsBusy = true;

            AllOrders = await ApiDatabaseService.DatabaseService.GetAllOrdersByUserId(ActiveUser.User.Id);
            AllAppointments = await ApiDatabaseService.DatabaseService.GetAllAppointmentsByUserId(ActiveUser.User.Id);

            IsBusy = false;
        }
    }
}
