using System;
using EstimationApp.ViewModels;
using NUnit.Framework;

namespace EstimationApp.Test.ViewModel
{
    public class PrintToPageViewModelTest : ViewModelBaseTest
    {
        PrintToPageViewModel _viewModel;

        public override void Init()
        {
            base.Init();
            _viewModel = new PrintToPageViewModel(MockNavigationService.Object, MockLogService.Object);
        }

        [Test]
        public async System.Threading.Tasks.Task NavigatedToAsync_TestAsync()
        {
            string mockPrintText = "This is mock print test to be displayed on to the screen";
            await _viewModel.NavigatedToAsync(mockPrintText);
            Assert.AreEqual(mockPrintText, _viewModel.PrintText);
        }

        [Test]
        public void DoneCommand_Test()
        {
            _viewModel.DoneCommand.Execute(default);
            MockNavigationService.Verify(x => x.PopAsync());
        }
    }
}
