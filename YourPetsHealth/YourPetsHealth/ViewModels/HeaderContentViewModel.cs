using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using YourPetsHealth.Utility;

namespace YourPetsHealth.ViewModels
{
    public partial class HeaderContentViewModel : ObservableObject
    {
        #region Constructors...

        public HeaderContentViewModel()
        {
            //UserFullName = ActiveUser.User.LastName + " " + ActiveUser.User.FirstName;
            //UserEmail = ActiveUser.User.Email;

            UserFullName = "Brassoi Catalin";
            UserEmail = "dupaSaSchimbiAici@neaparat.pls";
        }

        #endregion

        #region Private Fields...

        [ObservableProperty]
        private string _userFullName;
        [ObservableProperty]
        private string _userEmail;

        #endregion
    }
}
