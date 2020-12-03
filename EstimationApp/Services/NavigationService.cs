using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using EstimationApp.Callbacks;
using EstimationApp.IoC;
using EstimationApp.ViewModels;
using Xamarin.Forms;

namespace EstimationApp.Services
{
    public class NavigationService : INavigationService
    {
        private readonly ILogService _logService;

        public NavigationService(ILogService logService)
        {
            _logService = logService;
        }

        public async Task PopAsync()
        {
            try
            {
                if (Application.Current.MainPage is NavigationPage navigationPage)
                {
                    await navigationPage.PopAsync();
                }
            }
            catch (Exception ex)
            {
                _logService.WriteError(ex);
            }
        }

        public async Task PushAsync<T>(object data = null) where T : Page
        {
            try
            {
                if (Application.Current.MainPage is NavigationPage navigationPage)
                {
                    var page = IocUtil.Resolve<T>();
                    await navigationPage.PushAsync(page, true);
                    if (page.BindingContext is INavigationAware basePageViewModel)
                    {
                        await basePageViewModel.NavigatedToAsync(data);
                    }
                }
            }
            catch (Exception ex)
            {
                _logService.WriteError(ex);
            }
        }

        public async Task PushToRootAsync<T>(object data = default) where T : Page
        {
            try
            {
                if (Application.Current.MainPage is NavigationPage navigationPage)
                {
                    var page = IocUtil.Resolve<T>();
                    navigationPage.Navigation.InsertPageBefore(page, navigationPage.Navigation.NavigationStack[0]);
                    await navigationPage.Navigation.PopToRootAsync(false);
                    if (page.BindingContext is INavigationAware basePageViewModel)
                    {
                        await basePageViewModel.NavigatedToAsync(data);
                    }
                }
            }
            catch (Exception ex)
            {
                _logService.WriteError(ex);
            }
        }
    }
}
