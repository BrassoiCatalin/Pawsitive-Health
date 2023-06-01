using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;
using YourPetsHealth.Utility;

namespace YourPetsHealth.ViewModels
{
    public partial class HeaderContentViewModel : ObservableObject
    {
        #region Constructors...

        public HeaderContentViewModel()
        {
            UserFullName = ActiveUser.User.LastName + " " + ActiveUser.User.FirstName;
            UserEmail = ActiveUser.User.Email;

            if (ActiveUser.User.Image != null)
            {
                var stream = new MemoryStream(ActiveUser.User.Image);
                if(stream.CanRead)
                {
                    ProfileImage = ImageSource.FromStream(() => stream);
                }
            }
            else
            {
                ProfileImage = ImageSource.FromFile("user.png");
            }

            MessagingCenter.Subscribe<ProfileViewModel, byte[]>(this, "image", (sender, arguments) =>
            {
                var stream = new MemoryStream(arguments);
                ProfileImage = ImageSource.FromStream(() => stream);
            });
        }

        #endregion

        #region Private Fields...

        [ObservableProperty]
        private string _userFullName;
        [ObservableProperty]
        private string _userEmail;
        [ObservableProperty]
        private ImageSource _profileImage;

        #endregion
    }
}
