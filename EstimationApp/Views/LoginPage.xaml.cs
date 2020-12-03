using System;
using System.Collections.Generic;
using EstimationApp.ViewModels;
using Xamarin.Forms;

namespace EstimationApp.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(LoginPageViewModel loginPageViewModel)
        {
            InitializeComponent();
            this.BindingContext = loginPageViewModel;
        }

        void Entry_Completed(System.Object sender, System.EventArgs e)
        {
            passwordView.Focus();
        }
    }
}
