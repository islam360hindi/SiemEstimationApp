using System;
using System.Threading.Tasks;
using EstimationApp.Callbacks;
using EstimationApp.Services;

namespace EstimationApp.ViewModels
{
    public class BasePageViewModel : BaseViewModel, INavigationAware
    {
        private bool isBusy;

        protected ILogService LogService { get; private set; }
        protected INavigationService NavigationService { get; private set; }
        public bool IsBusy { get => isBusy; set => SetProperty(ref isBusy, value); }

        public BasePageViewModel(ILogService logService, INavigationService navigationService)
        {
            LogService = logService;
            NavigationService = navigationService;
        }

        public virtual Task NavigatedToAsync<T>(T parameters)
        {
            return Task.FromResult(0);
        }

        public virtual Task NavigatedBackAsync<T>(T parameters)
        {
            return Task.FromResult(0);
        }
    }
}
