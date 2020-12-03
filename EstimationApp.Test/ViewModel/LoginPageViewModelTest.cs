using System;
using System.Threading.Tasks;
using EstimationApp.DataAccess;
using EstimationApp.Localization;
using EstimationApp.ViewModels;
using EstimationApp.Views;
using Moq;
using NUnit.Framework;
using Xamarin.Forms;

namespace EstimationApp.Test.ViewModel
{
    public class LoginPageViewModelTest : ViewModelBaseTest
    {
        LoginPageViewModel _viewModel;
        Mock<ILoginDataAccess> _mockLoginDataAccess;

        public override void Init()
        {
            base.Init();
            _mockLoginDataAccess = new Mock<ILoginDataAccess>();
            _viewModel = new LoginPageViewModel(MockLogService.Object, MockNavigationService.Object, _mockLoginDataAccess.Object, MockAlertService.Object);
        }

        [Test]
        public async Task NavigatedToAsync_TestAsync()
        {
            await _viewModel.NavigatedToAsync<object>(null);
            Assert.IsNotNull(_viewModel.LoginCommand);
            Assert.IsNotNull(_viewModel.CancelCommand);
        }

        [Test]
        public async Task LoginCommand_WhenUsernameAndOrPasswordEmpty_TestAsync()
        {
            await _viewModel.NavigatedToAsync<object>(null);
            _viewModel.LoginCommand.Execute(null);
            MockAlertService.Verify(x => x.ShowAlertAsync(AppResources.strAlert, AppResources.alertUsernamePasswordRequired, It.IsAny<string>(), It.IsAny<string>()));
        }

        [Test]
        public async Task LoginCommand_WhenUnAuthorizedUserLogsIn_TestAsync()
        {
            await _viewModel.NavigatedToAsync<object>(null);
            _viewModel.Username = "unauthorizeduser";
            _viewModel.Password = "test";
            _mockLoginDataAccess.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new Model.LoginResult
                {
                    IsAuthenticated = false,
                    User = null
                }));
            _viewModel.LoginCommand.Execute(null);
            MockAlertService.Verify(x => x.ShowAlertAsync(AppResources.strAlert, AppResources.alertWrongUsernameOrPassword, It.IsAny<string>(), It.IsAny<string>()));
        }

        [Test]
        public async Task LoginCommand_WhenAuthorizedUserLogsInButNoUserDataFound_TestAsync()
        {
            await _viewModel.NavigatedToAsync<object>(null);
            _viewModel.Username = "authorizeduser";
            _viewModel.Password = "test";
            _mockLoginDataAccess.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new Model.LoginResult
                {
                    IsAuthenticated = true,
                    User = null
                }));
            _viewModel.LoginCommand.Execute(null);
            MockAlertService.Verify(x => x.ShowAlertAsync(AppResources.strAlert, AppResources.alertGeneralErrorMessage, It.IsAny<string>(), It.IsAny<string>()));
        }

        [Test]
        public async Task LoginCommand_LoginSuccess_TestAsync()
        {
            await _viewModel.NavigatedToAsync<object>(null);
            _viewModel.Username = "authorizeduser";
            _viewModel.Password = "test";
            _mockLoginDataAccess.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new Model.LoginResult
                {
                    IsAuthenticated = true,
                    User = new Model.User
                    {
                        Username = "testuser",
                        FirstName = "test",
                        LastName = "user",
                        UserType = Enums.UserType.PrivilegedUser
                    }
                }));
            _viewModel.LoginCommand.Execute(null);
            MockNavigationService.Verify(x => x.PushToRootAsync<PriceEstimationPage>(It.IsAny<object>()));
        }

        [Test]
        public async Task LoginCommand_Exception_TestAsync()
        {
            await _viewModel.NavigatedToAsync<object>(null);
            _viewModel.Username = "authorizeduser";
            _viewModel.Password = "test";
            _mockLoginDataAccess.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());
            _viewModel.LoginCommand.Execute(null);
            MockLogService.Verify(x => x.WriteError(It.IsAny<Exception>()));
        }
    }
}
