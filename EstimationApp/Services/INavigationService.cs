using System;
using System.Threading.Tasks;
using EstimationApp.ViewModels;
using Xamarin.Forms;

namespace EstimationApp.Services
{
    public interface INavigationService
    {
        Task PushAsync<T>(object data = default) where T : Page;

        Task PushToRootAsync<T>(object data = default) where T : Page;

        Task PopAsync();
    }
}
