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
    public partial class TabbedClinicDetailsView : TabbedPage
    {
        public TabbedClinicDetailsView()
        {
            InitializeComponent();
            BindingContext = new TabbedClinicDetailsViewModel();
        }

        public TabbedClinicDetailsView(Clinic clinic)
        {
            InitializeComponent();
            BindingContext = new TabbedClinicDetailsViewModel(clinic);
        }
    }
}