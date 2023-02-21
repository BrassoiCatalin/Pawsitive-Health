using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YourPetsHealth.ViewModels;

namespace YourPetsHealth.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInView : ContentPage
    {
        public LogInView()
        {
            InitializeComponent();
            BindingContext = new LogInViewModel();
        }
    }
}