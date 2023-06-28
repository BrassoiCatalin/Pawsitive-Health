using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using YourPetsHealth.Interfaces;
using YourPetsHealth.Models;
using YourPetsHealth.Services;
using YourPetsHealth.Utility;
using YourPetsHealth.Views;

namespace YourPetsHealth.ViewModels
{
    public partial class ProfileViewModel : ObservableObject
    {
        #region Constructors...

        public ProfileViewModel()
        {
            InitializePage();
            _navigationService = new NavigationService();
        }

        #endregion

        #region Private Fields...

        [ObservableProperty]
        private User _user;
        private readonly INavigationService _navigationService;

        #endregion

        #region Commands...

        [RelayCommand]
        private async Task EditProfileAsync()
        {
            await _navigationService.PushAsync(new EditPersonalDataView());
        }

        [RelayCommand]
        private async void DeleteProfile()
        {
            var pets = await ApiDatabaseService.DatabaseService.GetAllPetsByUserId(ActiveUser.User.Id);

            if (ActiveUser.Clinic == null)
            {
                foreach (var item in pets)
                {
                    await ApiDatabaseService.DatabaseService.DeletePet(item);
                }

                await ApiDatabaseService.DatabaseService.DeleteUser(ActiveUser.User);
                await App.Current.MainPage.DisplayAlert("Atentie!", "Userul a fost sters cu succes!", "Ok");
                ActiveUser.User = new User();
                ActiveUser.Clinic = null;
                App.Current.MainPage = new NavigationPage(new LogInView());
            }

            else
            {
                var response = await App.Current.MainPage.DisplayAlert("Atentie",
                "Stergand profilul inseamna sa stergi si clinica asociata. " +
                "Toate produsele si serviciile asociate, precum si comenzile clientilor si programarile " +
                "facute de acestia la aceasta clinica vor fi sterse. " +
                "Doresti sa continui?",
                "Da", "Nu");

                if (response)
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

                    foreach (var item in pets)
                    {
                        await ApiDatabaseService.DatabaseService.DeletePet(item);
                    }

                    await ApiDatabaseService.DatabaseService.DeleteClinic(ActiveUser.Clinic);
                    await ApiDatabaseService.DatabaseService.DeleteUser(ActiveUser.User);
                    await App.Current.MainPage.DisplayAlert("Atentie!", "Userul si clinica asociata au fost sterse cu succes!", "Ok");
                    ActiveUser.User = new User();
                    ActiveUser.Clinic = null;
                    App.Current.MainPage = new NavigationPage(new LogInView());
                }
                else
                {
                    return;
                }
            }
        }

        [RelayCommand]
        private void PageAppearing()
        {
            User = ActiveUser.User;
        }

        [RelayCommand]
        private async void ChangePicture()
        {
            var result = await App.Current.MainPage.DisplayAlert("Schimba poza", "Cum doresti sa schimbi poza?", "Fa o noua poza",
                "Alege o poza existenta");

            if (result)
                await TakeNewPicture();
            else
                await PickExistingPicture();

            await ApiDatabaseService.DatabaseService.UpdateUser(ActiveUser.User);
            await App.Current.MainPage.DisplayAlert("Succes", "Poza a fost schimbata cu succes!", "Ok");
        }

        #endregion

        #region Private Methods...

        private void InitializePage()
        {
            User = ActiveUser.User;
        }

        private async Task TakeNewPicture()
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result == null)
                return;

            var stream = await result.OpenReadAsync();
            var userImage = ImageSource.FromStream(() => stream);

            var memoryStream = new System.IO.MemoryStream();
            await stream.CopyToAsync(memoryStream);
            ActiveUser.User.Image = memoryStream.ToArray();
            MessagingCenter.Send(this, "image", ActiveUser.User.Image);
        }

        private async Task PickExistingPicture()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions()
            {
                Title = "Alege o poza:"
            });

            if (result == null)
                return;

            var stream = await result.OpenReadAsync();
            var userImage = ImageSource.FromStream(() => stream);

            var memoryStream = new System.IO.MemoryStream();
            await stream.CopyToAsync(memoryStream);
            ActiveUser.User.Image = memoryStream.ToArray();
            MessagingCenter.Send(this, "image", ActiveUser.User.Image);
        }

        #endregion

    }
}
