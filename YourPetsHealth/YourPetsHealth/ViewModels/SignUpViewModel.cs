using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
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
        private byte[] _image;
        [ObservableProperty]
        private int _address;
        private readonly NavigationService _navigationService;

        #endregion

        #region Commands...

        [RelayCommand]
        private async void Register()
        {
            await _navigationService.PopAsync();
        }

        #endregion
    }
}
