using System;
using EstimationApp.Callbacks;
using EstimationApp.Extensions;
using EstimationApp.IoC;
using EstimationApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace EstimationApp
{
    public partial class App : Application
    {
        public static bool UseMocks { get; internal set; }

        public App()
        {
            InitializeComponent();

            IoC.IocUtil.BuildIocContainer();
            var loginPage = IocUtil.Resolve<LoginPage>();
            MainPage = new NavigationPage(loginPage);
            if (loginPage.BindingContext is INavigationAware navigationAware)
                navigationAware.NavigatedToAsync<object>().FireAndForgetSafeAsync();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
