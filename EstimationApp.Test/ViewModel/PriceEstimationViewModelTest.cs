using System;
using EstimationApp.DataAccess;
using EstimationApp.Enums;
using EstimationApp.Model;
using EstimationApp.Services;
using EstimationApp.Util;
using EstimationApp.ViewModels;
using EstimationApp.Views;
using Moq;
using NUnit.Framework;
using Xamarin.Forms;

namespace EstimationApp.Test.ViewModel
{
    public class PriceEstimationViewModelTest : ViewModelBaseTest
    {
        PriceEstimationViewModel _viewModel;
        Mock<ILoginDataAccess> _mockLoginDataAccess;
        Mock<IPriceEstimationDataAccess> _mockPriceEstimationDataAccess;
        Mock<IPrinterService> _mockPrinterService;

        public override void Init()
        {
            base.Init();
            _mockLoginDataAccess = new Mock<ILoginDataAccess>();
            _mockPriceEstimationDataAccess = new Mock<IPriceEstimationDataAccess>();
            _mockPrinterService = new Mock<IPrinterService>();
            _viewModel = new PriceEstimationViewModel(MockLogService.Object, MockNavigationService.Object, _mockPriceEstimationDataAccess.Object, _mockLoginDataAccess.Object, MockAlertService.Object, _mockPrinterService.Object);
        }

        [Test]
        public async System.Threading.Tasks.Task NavigatedToAsync_TestAsync()
        {
            await _viewModel.NavigatedToAsync<User>(new User() { UserType = Enums.UserType.PrivilegedUser });
            Assert.IsNotNull(_viewModel.LogoutCommand);
            Assert.IsNotNull(_viewModel.CalculateCommand);
            Assert.IsNotNull(_viewModel.PrintCommand);
        }

        [Test]
        public async System.Threading.Tasks.Task CalculateCommand_TestAsync()
        {
            _viewModel.UnitPrice = 5000;
            _viewModel.Weight = 10;
            await _viewModel.NavigatedToAsync<User>(CreateMockUser());
            _mockPriceEstimationDataAccess.Setup(x => x.CalculatePrice(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<float>()))
                .ReturnsAsync(50000);
            _viewModel.CalculateCommand.Execute(default);
            Assert.IsTrue(_viewModel.HasPriceCalculated);
        }

        [Test]
        public async System.Threading.Tasks.Task LogoutCommand_TestAsync()
        {
            await _viewModel.NavigatedToAsync<User>(CreateMockUser());
            MockAlertService.Setup(x => x.ShowAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);
            _viewModel.LogoutCommand.Execute(default);
            _mockLoginDataAccess.Verify(x => x.Logout());
            MockNavigationService.Verify(x => x.PushToRootAsync<LoginPage>(It.IsAny<object>()));
        }

        [TestCase(PrinterType.PrintToFile)]
        [TestCase(PrinterType.PrintToPaper)]
        [TestCase(PrinterType.PrintToScreen)]
        public async System.Threading.Tasks.Task PrintCommand_TestAsync(PrinterType printerType)
        {
            _viewModel.UnitPrice = 5000;
            _viewModel.Weight = 10;
            await _viewModel.NavigatedToAsync<User>(CreateMockUser(UserType.PrivilegedUser));
            _viewModel.CalculateCommand.Execute(default);
            MockAlertService.Setup(x => x.ShowActionSheetAsync(It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(printerType.ToString());
            _viewModel.PrintCommand.Execute(default);
            _mockPrinterService.Verify(x => x.Print(It.IsAny<string>(), It.IsAny<PrinterType>()));
        }

        private User CreateMockUser(UserType privilegedUser = UserType.RegularUser)
        {
            return new User
            {
                FirstName = "Anas",
                LastName = "Alam",
                Username = "anasmzn@gmail.com",
                UserType = Enums.UserType.RegularUser
            };
        }
    }
}
