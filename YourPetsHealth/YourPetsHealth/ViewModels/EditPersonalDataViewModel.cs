using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using YourPetsHealth.Interfaces;
using YourPetsHealth.Models;
using YourPetsHealth.Services;
using YourPetsHealth.Utility;

namespace YourPetsHealth.ViewModels
{
    public partial class EditPersonalDataViewModel : ObservableObject
    {
        public EditPersonalDataViewModel()
        {
            _navigationService = new NavigationService();
        }

        [ObservableProperty]
        private string _firstName;
        [ObservableProperty]
        private string _lastName;
        [ObservableProperty]
        private string _email;
        [ObservableProperty]
        private string _phoneNumber;
        [ObservableProperty]
        private string _city;
        [ObservableProperty]
        private string _street;
        [ObservableProperty]
        private string _number;
        private readonly INavigationService _navigationService;

        [RelayCommand]
        private async void Confirm()
        {
            if (!CheckFirstName() || !CheckLastName() || !CheckEmail() ||
                !CheckPhoneNumber() || !CheckCity() || !CheckStreet() ||
                !CheckNumber())
            {
                return;
            }

            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || 
                string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(PhoneNumber))
            {
                var response = await App.Current.MainPage.DisplayAlert("Atentie",
                "Campurile lasate goale vor ramane la valorile vechi. Doresti sa continui?",
                "Da", "Nu");
                if (!response)
                {
                    return;
                }
            }

            int nonEmptyFieldsCount = 0;

            if (!string.IsNullOrEmpty(City))
                nonEmptyFieldsCount++;
            if (!string.IsNullOrEmpty(Street))
                nonEmptyFieldsCount++;
            if (!string.IsNullOrEmpty(Number))
                nonEmptyFieldsCount++;

            if (nonEmptyFieldsCount == 0 || nonEmptyFieldsCount == 3)
            {
                
            }

            else
            {
                await App.Current.MainPage.DisplayAlert("Eroare!", "Pentru a putea schimba adresa, trebuie schimbate toate trei intrarile urmatoare: Oras, Strada, Numarul.", "OK");
                return;
            }

            var address = new Address()
            {
                Id = ActiveUser.User.Address.Id,
                City = City == null ? ActiveUser.User.Address.City : City,
                Street = Street == null ? ActiveUser.User.Address.Street : Street,
                Number = Number == null ? ActiveUser.User.Address.Number : Number
            };

            var user = new User()
            {
                Id = ActiveUser.User.Id,
                FirstName = FirstName == null ? ActiveUser.User.FirstName : FirstName,
                LastName = LastName == null ? ActiveUser.User.LastName : LastName,
                Email = Email == null ? ActiveUser.User.Email : Email,
                Password = ActiveUser.User.Password,
                PhoneNumber = PhoneNumber == null ? ActiveUser.User.PhoneNumber : PhoneNumber,
                Image = ActiveUser.User.Image,
                Role = ActiveUser.User.Role,
                ClinicId = ActiveUser.User.ClinicId,
                Address = address
            };

            await ApiDatabaseService.DatabaseService.Register(user, address);
            await App.Current.MainPage.DisplayAlert("Succes!", "Datele au fost actualizate!", "OK");
            ActiveUser.User = user;
            await _navigationService.PopAsync();
        }

        [RelayCommand]
        private async void Cancel()
        {
            await _navigationService.PopAsync();
        }

        private bool CheckFirstName()
        {
            if(string.IsNullOrWhiteSpace(FirstName))
            {
                return true;
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
            if (string.IsNullOrWhiteSpace(LastName))
            {
                return true;
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
            if (string.IsNullOrWhiteSpace(Email))
            {
                return true;
            }

            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w)+)+)$");

            if (regex.Match(Email) == Match.Empty || Email.Length < 4 || Email.Length > 50)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Emailul este invalid.", "OK");
                return false;
            }
            return true;
        }

        private bool CheckPhoneNumber()
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                return true;
            }

            if (PhoneNumber.Length != 10 || !Regex.IsMatch(PhoneNumber, "^[0-9]+$"))
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Numar de telefon invalid", "OK");
                return false;
            }
            return true;
        }

        private bool CheckCity()
        {
            if (string.IsNullOrWhiteSpace(City))
            {
                return true;
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
            if (string.IsNullOrWhiteSpace(Street))
            {
                return true;
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
            if (string.IsNullOrWhiteSpace(Number))
            {
                return true;
            }

            if (!Regex.IsMatch(Number, "^[0-9]+$"))
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Numarul nu trebuie sa aiba caractere invalide.", "OK");
                return false;
            }
            return true;
        }
    }
}
