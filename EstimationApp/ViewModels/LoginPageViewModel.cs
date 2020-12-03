using System;
using System.Threading.Tasks;
using System.Windows.Input;
using EstimationApp.DataAccess;
using EstimationApp.IoC;
using EstimationApp.Localization;
using EstimationApp.Services;
using EstimationApp.Views;
using Xamarin.Forms;

namespace EstimationApp.ViewModels
{
    public class LoginPageViewModel : BasePageViewModel
    {
        #region fields
        private readonly ILoginDataAccess _dataAccess;
        private readonly IAlertService _alertService;
        private string username;
        private string password;
        private ICommand loginCommand;
        private ICommand cancelCommand;
        #endregion

        public string Username { get => username; set => SetProperty(ref username, value); }

        public string Password { get => password; set => SetProperty(ref password, value); }

        public ICommand LoginCommand { get => loginCommand; set => SetProperty(ref loginCommand, value); }

        public ICommand CancelCommand { get => cancelCommand; set => SetProperty(ref cancelCommand, value); }

        #region Constructors
        public LoginPageViewModel(ILogService logService, INavigationService navigationService, ILoginDataAccess loginDataAccess, IAlertService alertService) : base(logService, navigationService)
        {
            _dataAccess = loginDataAccess;
            _alertService = alertService;
        }
        #endregion

        public override Task NavigatedToAsync<T>(T parameters)
        {
            LoginCommand = new Command(async () => await OnLoginCommandAsync());
            CancelCommand = new Command(() => OnCancelCommand());
            return base.NavigatedToAsync(parameters);
        }

        private void OnCancelCommand()
        {
            //Exit from the app
        }

        private async Task OnLoginCommandAsync()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await _alertService.ShowAlertAsync(AppResources.strAlert, AppResources.alertUsernamePasswordRequired);
                return;
            }
            try
            {
                IsBusy = true;
                var loginResult = await _dataAccess.Login(Username, Password);
                if (!loginResult.IsAuthenticated)
                {
                    await _alertService.ShowAlertAsync(AppResources.strAlert, AppResources.alertWrongUsernameOrPassword);
                    return;
                }
                if (loginResult?.User == null)
                {
                    await _alertService.ShowAlertAsync(AppResources.strAlert, AppResources.alertGeneralErrorMessage);
                    return;
                }
                await NavigationService.PushToRootAsync<PriceEstimationPage>(loginResult?.User);
            }
            catch (Exception ex)
            {
                await _alertService.ShowAlertAsync(AppResources.strAlert, AppResources.alertWrongUsernameOrPassword);
                LogService.WriteError(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
