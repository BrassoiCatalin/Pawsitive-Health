using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using YourPetsHealth.Interfaces;
using YourPetsHealth.Models;
using YourPetsHealth.Services;
using YourPetsHealth.Utility;
using YourPetsHealth.Views;

namespace YourPetsHealth.ViewModels
{
    public partial class PetsViewModel : ObservableObject
    {
        #region Constructors...

        public PetsViewModel()
        {
            _navigationService = new NavigationService();

            IsTopTextVisible = true;
            PetsVisible = false;
            Pets = new List<Pet>();
            InitializePage();
        }

        #endregion

        #region Properties...

        [ObservableProperty]
        private bool _isTopTextVisible;
        [ObservableProperty]
        private bool _petsVisible;
        [ObservableProperty]
        private List<Pet> _pets;
        private readonly INavigationService _navigationService;
        [ObservableProperty]
        private Pet _selectedPet;

        #endregion

        #region Commands...

        [RelayCommand]
        private async void PageAppearing(object obj)
        {
            Pets = await ApiDatabaseService.DatabaseService.GetAllPetsByUserId(ActiveUser.User.Id);

            if (Pets.Count == 0)
            {
                return;
            }
            IsTopTextVisible = false;
            PetsVisible = true;
        }


        [RelayCommand]
        private async void AddNewPet()
        {
            await _navigationService.PushAsync(new NewPetView());
        }

        [RelayCommand]
        private async void Back()
        {
            await _navigationService.PopAsync();
        }

        [RelayCommand]
        private async void Delete()
        {
            if (SelectedPet == null)
            {
                await App.Current.MainPage.DisplayAlert("Atentie",
               "Intai trebuie sa selectezi un animal.", "OK");
                return;
            }

            var answer = await App.Current.MainPage.DisplayAlert("Confirma",
                "Esti sigur ca vrei sa stergi acest animal?", "Da", "Nu");

            if (!answer)
            {
                return;
            }
            else
            {
                await ApiDatabaseService.DatabaseService.DeletePet(SelectedPet);
                SelectedPet = null;
            }

            Pets = await ApiDatabaseService.DatabaseService.GetAllPetsByUserId(ActiveUser.User.Id);
        }

        [RelayCommand]
        private async void EditPet()
        {
            if (SelectedPet == null)
            {
                await App.Current.MainPage.DisplayAlert("Atentie",
               "Intai trebuie sa selectezi un animal.", "OK");
                return;
            }

            await _navigationService.PushAsync(new EditPetView(SelectedPet));
        }

        #endregion

        #region Private Methods...

        private async void InitializePage()
        {
            Pets = await ApiDatabaseService.DatabaseService.GetAllPetsByUserId(ActiveUser.User.Id);
        }

        #endregion

    }
}
