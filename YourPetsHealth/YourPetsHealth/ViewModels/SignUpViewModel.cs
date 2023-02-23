using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using YourPetsHealth.Models;
using YourPetsHealth.Services;
using static Xamarin.Essentials.Permissions;

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
        private string _confirmPassword;
        [ObservableProperty]
        private string _phoneNumber;
        [ObservableProperty]
        private string _city;
        [ObservableProperty]
        private string _street;
        [ObservableProperty]
        private string _number;

        private readonly NavigationService _navigationService;

        #endregion

        #region Commands...

        [RelayCommand]
        private async void Register()
        {
            if (!CheckFirstName() || !CheckLastName() || !CheckEmail() ||
                !CheckAndConfirmPassword() || !CheckPhoneNumber()
                || !CheckCity() || !CheckStreet() || !CheckNumber())
            {
                return;
            }

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

        private bool CheckFirstName()
        {
            if (FirstName == null)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Prenumele trebuie sa fie completat.", "OK");
                return false;
            }

            if (FirstName.Length < 3 || FirstName.Length > 50)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Prenumele trebuie sa aiba minim 3 caractere sau maxim 50.", "OK");
                return false;
            }

            if (!FirstName.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || a == '-'))
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Prenumele nu trebuie sa aiba caractere invalide.", "OK");
                return false;
            }
            return true;
        }

        private bool CheckLastName()
        {
            if (LastName == null)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Numele trebuie sa fie completat.", "OK");
                return false;
            }

            if (LastName.Length < 3 || LastName.Length > 50)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Numele trebuie sa aiba minim 3 caractere sau maxim 50.", "OK");
                return false;
            }

            if (!LastName.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || a == '-'))
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Numele nu trebuie sa aiba caractere invalide.", "OK");
                return false;
            }
            return true;
        }

        private bool CheckEmail()
        {
            if (Email == null)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Emailul este invalid.", "OK");
                return false;
            }

            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w)+)+)$");

            if (regex.Match(Email) == Match.Empty || Email.Length < 4 || Email.Length > 50)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Emailul este invalid.", "OK");
                return false;
            }
            return true;
        }

        private bool CheckAndConfirmPassword()
        {
            if (Password == null || Password.Length < 8 || Password.Length > 50)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Parola trebuie sa contina cel putin 8 caractere si maxim 50.", "OK");
                return false;
            }

            var regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?=!@$%^&*-]).{8,}$");

            if (regex.Match(Password) == Match.Empty)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Parola trebuie sa contina cel putin 8 caractere, sa contina o litera mica, o litera mare, o cifra si un caracter special.", "OK");
                return false;
            }

            if (!Password.Equals(ConfirmPassword))
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Parolele nu sunt identice.", "OK");
                return false;
            }
            return true;
        }

        private bool CheckPhoneNumber()
        {
            if (PhoneNumber == null || PhoneNumber.Length != 10 || !Regex.IsMatch(PhoneNumber, "^[0-9]+$"))
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Numar de telefon invalid", "OK");
                return false;
            }
            return true;
        }

        private bool CheckCity()
        {
            if (City == null)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Orasul trebuie sa fie completat.", "OK");
                return false;
            }

            if (!City.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || a == '-'))
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Orasul nu trebuie sa aiba caractere invalide.", "OK");
                return false;
            }
            return true;
        }

        private bool CheckStreet()
        {
            if (Street == null)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Strada trebuie sa fie completata.", "OK");
                return false;
            }

            if (!Street.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || a == '-' || a == '.' || char.IsNumber(a)))
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Strada nu trebuie sa aiba caractere invalide.", "OK");
                return false;
            }
            return true;
        }

        private bool CheckNumber()
        {
            if (Number == null)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Numarul trebuie sa fie completat.", "OK");
                return false;
            }

            if (!Regex.IsMatch(Number, "^[0-9]+$"))
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Numarul nu trebuie sa aiba caractere invalide.", "OK");
                return false;
            }
            return true;
        }

        #endregion
    }
}
