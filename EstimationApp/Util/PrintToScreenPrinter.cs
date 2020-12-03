using System;
using System.Threading.Tasks;
using EstimationApp.IoC;
using EstimationApp.Services;
using EstimationApp.Views;

namespace EstimationApp.Util
{
    public class PrintToScreenPrinter : IPrinter
    {
        public async Task Print(string text)
        {
            var navigationService = IocUtil.Resolve<INavigationService>();
            await navigationService.PushAsync<PrintToScreenPage>(text);
        }
    }
}
