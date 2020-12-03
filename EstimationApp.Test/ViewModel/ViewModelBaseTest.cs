using System;
using EstimationApp.Services;
using Moq;
using NUnit.Framework;

namespace EstimationApp.Test.ViewModel
{
    public class ViewModelBaseTest
    {
        protected Mock<INavigationService> MockNavigationService { get; set; }
        protected Mock<ILogService> MockLogService { get; set; }
        protected Mock<IAlertService> MockAlertService { get; set; }

        [SetUp]
        public virtual void Init()
        {
            MockNavigationService = new Mock<INavigationService>();
            MockLogService = new Mock<ILogService>();
            MockAlertService = new Mock<IAlertService>();
        }

    }
}
