using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Essentials;
using YourPetsHealth.Interfaces;
using YourPetsHealth.Models;
using YourPetsHealth.Services;

namespace YourPetsHealth.ViewModels
{
    public partial class EditPetViewModel : ObservableObject
    {
        public EditPetViewModel()
        {
            _navigationService = new NavigationService();
        }

        public EditPetViewModel(Pet pet)
        {
            _navigationService = new NavigationService();
            _localPet = pet;
        }

        [ObservableProperty]
        private string _petName;
        [ObservableProperty]
        private string _petSpecies;
        [ObservableProperty]
        private string _petBreed;
        [ObservableProperty]
        private string _petHeight;
        [ObservableProperty]
        private string _petWeight;
        private Pet _localPet;
        private readonly INavigationService _navigationService;

        [RelayCommand]
        private async void Confirm()
        {
            if (!CheckHeightAndWeight())
            {
                return;
            }

            if (string.IsNullOrEmpty(PetName) || string.IsNullOrEmpty(PetSpecies) ||
                string.IsNullOrEmpty(PetBreed) || string.IsNullOrEmpty(PetHeight) ||
                string.IsNullOrEmpty(PetWeight))
            {
                var response = await App.Current.MainPage.DisplayAlert("Atentie",
                "Campurile lasate goale vor ramane la valorile vechi. Doresti sa continui?",
                "Da", "Nu");
                if (!response)
                {
                    return;
                }
            }

            var pet = new Pet()
            {
                Id = _localPet.Id,
                Name = PetName == null ? _localPet.Name : PetName,
                Species = PetSpecies == null ? _localPet.Species : PetSpecies,
                Breed = PetBreed == null ? _localPet.Breed : PetBreed,
                Height = PetHeight == null ? _localPet.Height : Convert.ToDouble(PetHeight),
                Weight = PetWeight == null ? _localPet.Weight : Convert.ToDouble(PetWeight),
                UserId = _localPet.UserId
            };

            await ApiDatabaseService.DatabaseService.UpdatePet(pet);
            await App.Current.MainPage.DisplayAlert("Succes!", "Datele au fost actualizate!", "OK");
            await _navigationService.PopAsync();
        }

        [RelayCommand]
        private async void Cancel()
        {
            await _navigationService.PopAsync();
        }

        private bool CheckHeightAndWeight()
        {
            if (string.IsNullOrWhiteSpace(PetHeight))
            {
                return true;
            }

            bool isMatch = Regex.IsMatch(PetHeight, @"^[+-]?\d+(\.\d+)?$");

            if (!isMatch)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Inaltimea nu poate contine caractere invalide!", "OK");
                return false;
            }

            if (string.IsNullOrWhiteSpace(PetWeight))
            {
                return true;
            }

            isMatch = Regex.IsMatch(PetWeight, @"^[+-]?\d+(\.\d+)?$");

            if (!isMatch)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Greutatea nu poate contine caractere invalide!", "OK");
                return false;
            }
            return true;
        }
    }
}
