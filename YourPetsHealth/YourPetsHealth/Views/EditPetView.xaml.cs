using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YourPetsHealth.Models;
using YourPetsHealth.ViewModels;

namespace YourPetsHealth.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPetView : ContentPage
    {
        public EditPetView()
        {
            InitializeComponent();
            BindingContext = new EditPetViewModel();
        }

        public EditPetView(Pet pet)
        {
            InitializeComponent();
            BindingContext = new EditPetViewModel(pet);
        }
    }
}