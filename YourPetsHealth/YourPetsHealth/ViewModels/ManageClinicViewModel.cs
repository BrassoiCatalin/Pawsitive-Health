using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using YourPetsHealth.Interfaces;
using YourPetsHealth.Services;
using YourPetsHealth.Views;

namespace YourPetsHealth.ViewModels
{
    public partial class ManageClinicViewModel : ObservableObject
    {
        #region Constructors...

        public ManageClinicViewModel()
        {
            _navigationService = new NavigationService();
        }

        #endregion


        #region Private Fields...

        [ObservableProperty]
        private bool _areTopTextAndButtonVisible;
        private readonly INavigationService _navigationService;

        #endregion

        #region Commands...

        [RelayCommand]
        private async void AddNewClinic()
        {
            await _navigationService.PushAsync(new NewClinicView());
        }

        #endregion
    }
}
