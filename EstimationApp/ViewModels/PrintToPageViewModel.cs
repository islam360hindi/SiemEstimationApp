using System;
using System.Threading.Tasks;
using System.Windows.Input;
using EstimationApp.Extensions;
using EstimationApp.Services;
using Xamarin.Forms;

namespace EstimationApp.ViewModels
{
    public class PrintToPageViewModel : BasePageViewModel
    {
        private string printText;

        public string PrintText { get => printText; set => SetProperty(ref printText, value); }

        public ICommand DoneCommand { get; set; }

        public PrintToPageViewModel(INavigationService navigationService, ILogService logService) : base(logService, navigationService)
        {
            DoneCommand = new Command(() => navigationService.PopAsync().FireAndForgetSafeAsync());
        }

        public override Task NavigatedToAsync<T>(T parameters)
        {
            if (parameters is string text)
            {
                PrintText = text;
            }
            return base.NavigatedToAsync(parameters);
        }
    }
}
