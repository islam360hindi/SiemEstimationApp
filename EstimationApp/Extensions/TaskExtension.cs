using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EstimationApp.IoC;
using EstimationApp.Services;
using EstimationApp.ViewModels;
using Xamarin.Forms;

namespace EstimationApp.Extensions
{
    public static class TaskExtension
    {
        public static void FireAndForgetSafeAsync(this Task task)
        {
            task.ContinueWith((t) =>
            {
                if (Debugger.IsAttached)
                    Debugger.Break();
                var logService = IocUtil.Resolve<ILogService>();
                logService?.WriteError(t.Exception);
                HideLoader();
            }, TaskContinuationOptions.OnlyOnFaulted);
        }

        private static void HideLoader()
        {
            if (App.Current.MainPage is NavigationPage navigationPage && navigationPage.CurrentPage?.BindingContext is BasePageViewModel vm)
            {
                vm.IsBusy = false;
            }
        }
    }
}
