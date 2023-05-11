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
    public partial class ManageClinicViewModel : ObservableObject
    {
        #region Constructors...

        public ManageClinicViewModel()
        {
            _navigationService = new NavigationService();

            AreTopTextAndButtonVisible = true;
            IsExistingClinicVisible = false;

            if (ActiveUser.Clinic != null)
            {
                ClinicName = ActiveUser.Clinic.Name;
                ClinicStartHour = ActiveUser.Clinic.StartHour;
                ClinicEndHour = ActiveUser.Clinic.EndHour;
                ClinicCity = ActiveUser.Clinic.Address.City;
                ClinicStreet = ActiveUser.Clinic.Address.Street;
                ClinicNumber = ActiveUser.Clinic.Address.Number;
            }
        }

        #endregion

        #region Private Fields...

        [ObservableProperty]
        private bool _areTopTextAndButtonVisible;
        [ObservableProperty]
        private bool _isExistingClinicVisible;
        private readonly INavigationService _navigationService;
        [ObservableProperty]
        private string _clinicName;
        [ObservableProperty]
        private TimeSpan _clinicStartHour;
        [ObservableProperty]
        private TimeSpan _clinicEndHour;
        [ObservableProperty]
        private string _clinicCity;
        [ObservableProperty]
        private string _clinicStreet;
        [ObservableProperty]
        private string _clinicNumber;

        #endregion

        #region Commands...

        [RelayCommand]
        private async void AddNewClinic()
        {
            await _navigationService.PushAsync(new NewClinicView());
        }

        [RelayCommand]
        private void PageAppearing(object obj)
        {
            if (ActiveUser.Clinic is null)
            {
                return;
            }
            AreTopTextAndButtonVisible = false;
            IsExistingClinicVisible = true;

            ClinicName = ActiveUser.Clinic.Name;
            ClinicStartHour = ActiveUser.Clinic.StartHour;
            ClinicEndHour = ActiveUser.Clinic.EndHour;
            ClinicCity = ActiveUser.Clinic.Address.City;
            ClinicStreet = ActiveUser.Clinic.Address.Street;
            ClinicNumber = ActiveUser.Clinic.Address.Number;
        }

        [RelayCommand]
        private async void ViewProducts()
        {
            await _navigationService.PushAsync(new ProductsView());
        }

        [RelayCommand]
        private async void ViewProcedures()
        {
            await _navigationService.PushAsync(new ProceduresView());
        }

        [RelayCommand]
        private async void DeleteClinic()
        {
            var response = await App.Current.MainPage.DisplayAlert("Atentie",
                "Stergand clinica inseamna sa stergi toate produsele si serviciile asociate cu aceasta clinica." +
                "De asemenea, vei sterge si toate comenzile clientilor si programarile facute de acestia." +
                "Doresti sa continui?",
                "Da", "Nu");

            if(response)
            {
                var allProducts = await ApiDatabaseService.DatabaseService.GetAllProductsByClinicId(ActiveUser.Clinic.Id);
                var allProcedures = await ApiDatabaseService.DatabaseService.GetAllProceduresByClinicId(ActiveUser.Clinic.Id);
                var allOrders = await ApiDatabaseService.DatabaseService.GetAllOrdersByClinicId(ActiveUser.Clinic.Id);
                var allAppointments = await ApiDatabaseService.DatabaseService.GetAllAppointmentsByClinicId(ActiveUser.Clinic.Id);

                foreach (var product in allProducts)
                {
                    await ApiDatabaseService.DatabaseService.DeleteProduct(product);
                }

                foreach (var procedure in allProcedures)
                {
                    await ApiDatabaseService.DatabaseService.DeleteProcedure(procedure);
                }

                foreach (var order in allOrders)
                {
                    await ApiDatabaseService.DatabaseService.DeleteOrder(order);
                }

                foreach (var appointment in allAppointments)
                {
                    await ApiDatabaseService.DatabaseService.DeleteAppointment(appointment);
                }

                await ApiDatabaseService.DatabaseService.DeleteClinic(ActiveUser.Clinic);
                ActiveUser.Clinic = null;
                await ApiDatabaseService.DatabaseService.UpdateClinicIdForUser(Guid.Empty);
                await ApiDatabaseService.DatabaseService.UpdateUserRoleToCustomer();
                AreTopTextAndButtonVisible = true;
                IsExistingClinicVisible = false;
            }
            else
            {
                return;
            }
        }

        #endregion
    }
}
