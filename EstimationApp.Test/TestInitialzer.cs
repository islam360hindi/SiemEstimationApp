using EstimationApp.IoC;
using NUnit.Framework;

namespace EstimationApp.Test
{
    [SetUpFixture]
    public class TestInitialzer
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
            IocUtil.BuildIocContainer();
        }
    }
}
