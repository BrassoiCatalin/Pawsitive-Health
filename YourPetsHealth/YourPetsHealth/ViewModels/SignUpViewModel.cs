using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using YourPetsHealth.Models;
using YourPetsHealth.Services;

namespace YourPetsHealth.ViewModels
{
    public partial class SignUpViewModel : ObservableObject
    {
        #region Constructors...

        public SignUpViewModel()
        {
            _navigationService = new NavigationService();
        }

        #endregion

        #region Private Fields...

        [ObservableProperty]
        private string _firstName;
        [ObservableProperty]
        private string _lastName;
        [ObservableProperty]
        private string _email;
        [ObservableProperty]
        private string _password;
        [ObservableProperty]
        private string _phoneNumber;
        [ObservableProperty]
        private string _city;
        [ObservableProperty]
        private string _street;
        [ObservableProperty]
        private int _number;

        private readonly NavigationService _navigationService;

        #endregion

        #region Commands...

        [RelayCommand]
        private async void Register()
        {
            Address address = new Address()
            {
                Id = Guid.NewGuid(),
                City = City,
                Street = Street,
                Number = Number,
            };

            User user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password,
                PhoneNumber = PhoneNumber,
                Image = null,
                Role = "Customer",
                ClinicId = Guid.Empty,
                Address = address,
            };

            /*Multe validari!!! Aici sau inainte de crearea obiectelor de mai sus!*/

            await ApiDatabaseService.DatabaseService.Register(user, address);
            await App.Current.MainPage.DisplayAlert("Succes!", "Te-ai inregistrat cu succes", "OK");
            await _navigationService.PopAsync();
        }

        [RelayCommand]
        private async void Cancel()
        {
            await _navigationService.PopAsync();
        }

        #endregion

        #region Private Methods...

        private void CheckFirstName()
        {
            if (FirstName == null)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Prenumele trebuie sa fie completat.", "OK");
                return;
            }

            if (FirstName.Length < 3 || FirstName.Length > 50)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Prenumele trebuie sa aiba minim 3 caractere sau maxim 50.", "OK");
                return;
            }

            if (!FirstName.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || a == '-'))
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Prenumele nu trebuie sa aiba caractere invalide.", "OK");
                return;
            }
        }

        private void CheckLastName()
        {
            if (LastName == null)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Numele trebuie sa fie completat.", "OK");
                return;
            }

            if (LastName.Length < 3 || LastName.Length > 50)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Numele trebuie sa aiba minim 3 caractere sau maxim 50.", "OK");
                return;
            }

            if (!LastName.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || a == '-'))
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Numele nu trebuie sa aiba caractere invalide.", "OK");
                return;
            }
        }

        #endregion

    }
}
