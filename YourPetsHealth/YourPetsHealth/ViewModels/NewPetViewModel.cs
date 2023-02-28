﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using YourPetsHealth.Interfaces;
using YourPetsHealth.Models;
using YourPetsHealth.Services;
using YourPetsHealth.Utility;

namespace YourPetsHealth.ViewModels
{
    public partial class NewPetViewModel : ObservableObject
    {
        public NewPetViewModel()
        {
            _navigationService = new NavigationService();
        }

        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private string _species;
        [ObservableProperty]
        private string _breed;
        [ObservableProperty]
        private string _height;
        [ObservableProperty]
        private string _weight;
        private readonly INavigationService _navigationService;

        [RelayCommand]
        private async void AddNewPet()
        {
            if (!CheckHeightAndWeight())
            {
                return;
            }

            var pet = new Pet()
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Species = Species,
                Breed = Breed,
                Height = Convert.ToDouble(Height),
                Weight = Convert.ToDouble(Weight),
                UserId = ActiveUser.User.Id
            };

            await ApiDatabaseService.DatabaseService.CreateNewPet(pet);
            await App.Current.MainPage.DisplayAlert("Succes!", "Animal adaugat cu succes", "OK");
        }

        [RelayCommand]
        private async void Cancel()
        {
            await _navigationService.PopAsync();
        }

        private bool CheckHeightAndWeight()
        {
            bool isMatch = Regex.IsMatch(Height, @"^[+-]?\d+(\.\d+)?$");

            if (!isMatch)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Inaltimea nu poate contine caractere invalide!", "OK");
                return false;
            }

            isMatch = Regex.IsMatch(Weight, @"^[+-]?\d+(\.\d+)?$");

            if (!isMatch)
            {
                App.Current.MainPage.DisplayAlert("Eroare!", "Greutatea nu poate contine caractere invalide!", "OK");
                return false;
            }
            return true;
        }
    }
}