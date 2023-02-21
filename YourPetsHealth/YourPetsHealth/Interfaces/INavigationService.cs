using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace YourPetsHealth.Interfaces
{
    public interface INavigationService
    {
        Task PushAsync(Page page);
        Task<Page> PopAsync();

        Task PushModalAsync(Page page);
    }
}
