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
    public partial class NewAppointmentView : ContentPage
    {
        public NewAppointmentView()
        {
            InitializeComponent();
            BindingContext = new NewAppointmentViewModel();
        }
        public NewAppointmentView(Clinic clinic, List<Procedure> procedures)
        {
            InitializeComponent();
            BindingContext = new NewAppointmentViewModel(clinic, procedures);
        }
    }
}