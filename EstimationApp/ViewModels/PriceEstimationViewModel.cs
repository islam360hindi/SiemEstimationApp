using System;
using System.Threading.Tasks;
using System.Windows.Input;
using EstimationApp.DataAccess;
using EstimationApp.Extensions;
using EstimationApp.IoC;
using EstimationApp.Localization;
using EstimationApp.Model;
using EstimationApp.Services;
using EstimationApp.Util;
using EstimationApp.Views;
using Xamarin.Forms;

namespace EstimationApp.ViewModels
{
    public class PriceEstimationViewModel : BasePageViewModel
    {
        private readonly IPriceEstimationDataAccess _dataAccess;
        private readonly ILoginDataAccess _loginDataAccess;
        private readonly IAlertService _alertService;
        private readonly IPrinterService _printerService;
        private double? unitPrice;
        private double? weight;
        private bool isDiscountAllowed;
        private ICommand calculateCommand;
        private double? totalPrice;
        private bool hasPriceCalculated;
        private float discount = 2.0f;
        private ICommand logoutCommand;
        private ICommand printCommand;

        public double? UnitPrice { get => unitPrice; set => SetProperty(ref unitPrice, value); }
        public double? Weight { get => weight; set => SetProperty(ref weight, value); }
        public double? TotalPrice { get => totalPrice; set => SetProperty(ref totalPrice, value); }
        public float Discount { get => discount; set => SetProperty(ref discount, value); }
        public bool IsDiscountAllowed { get => isDiscountAllowed; set => SetProperty(ref isDiscountAllowed, value); }
        public bool HasPriceCalculated { get => hasPriceCalculated; set => SetProperty(ref hasPriceCalculated, value); }
        public ICommand CalculateCommand { get => calculateCommand; set => SetProperty(ref calculateCommand, value); }
        public ICommand LogoutCommand { get => logoutCommand; set => SetProperty(ref logoutCommand, value); }
        public ICommand PrintCommand { get => printCommand; set => SetProperty(ref printCommand, value); }

        public PriceEstimationViewModel(ILogService logService, INavigationService navigationService, IPriceEstimationDataAccess dataAccess, ILoginDataAccess loginDataAccess, IAlertService alertService, IPrinterService printerService) : base(logService, navigationService)
        {
            _dataAccess = dataAccess;
            _loginDataAccess = loginDataAccess;
            _alertService = alertService;
            _printerService = printerService;
        }

        public override async Task NavigatedToAsync<T>(T parameters)
        {
            if (parameters is User loggedInUser)
            {
                await UpdateDiscountAsync(loggedInUser);
                CalculateCommand = new Command(() => CalculatePriceAsync().FireAndForgetSafeAsync());
                LogoutCommand = new Command(() => OnLogoutCommandAsync().FireAndForgetSafeAsync());
                PrintCommand = new Command(() => OnPrintCommandAsync().FireAndForgetSafeAsync());
            }
        }

        private async Task OnPrintCommandAsync()
        {
            var selectedPrinterType = await _alertService.ShowActionSheetAsync(AppResources.strSelectPrinterType,
                new string[] { AppResources.strPrintToScreen, AppResources.strPrintToFile, AppResources.strPrintToPaper },
                AppResources.strCancel);
            if (!string.IsNullOrEmpty(selectedPrinterType) && !selectedPrinterType.Equals(AppResources.strCancel))
            {
                _printerService.Print(CreatePrint(), CreatePrinterType(selectedPrinterType));
            }
        }

        private string CreatePrint()
        {
            return string.Format(AppResources.strPrintText, UnitPrice, Weight, Math.Round(TotalPrice.Value));
        }

        private PrinterType CreatePrinterType(string selectedPrinterType)
        {
            if (selectedPrinterType.Equals(AppResources.strPrintToFile))
                return PrinterType.PrintToFile;
            if (selectedPrinterType.Equals(AppResources.strPrintToPaper))
                return PrinterType.PrintToPaper;
            else
                return PrinterType.PrintToScreen;
        }

        private async Task OnLogoutCommandAsync()
        {
            IsBusy = true;
            var accepted = await _alertService.ShowAlertAsync(AppResources.strAlert, AppResources.alertLogoutMessage, AppResources.strOK);
            if (accepted)
            {
                IsBusy = true;
                await _loginDataAccess.Logout();
                await NavigationService.PushToRootAsync<LoginPage>();
                IsBusy = false;
            }
        }

        private async Task CalculatePriceAsync()
        {
            if (UnitPrice.HasValue && Weight.HasValue)
            {
                IsBusy = true;
                TotalPrice = await _dataAccess.CalculatePrice(UnitPrice.Value, Weight.Value, IsDiscountAllowed ? Discount : default);
                if (TotalPrice > 0)
                {
                    HasPriceCalculated = true;
                }
                IsBusy = false;
            }
        }

        private async Task UpdateDiscountAsync(User loggedInUser)
        {
            try
            {
                IsBusy = true;
                if (loggedInUser.UserType == Enums.UserType.PrivilegedUser)
                {
                    IsDiscountAllowed = true;
                    Discount = await _dataAccess.GetDiscountPercentage(Enums.UserType.PrivilegedUser);
                }
            }
            catch (Exception ex)
            {
                LogService.WriteError(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
