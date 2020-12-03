using System;
using System.Collections.Generic;
using EstimationApp.ViewModels;
using Xamarin.Forms;

namespace EstimationApp.Views
{
    public partial class PrintToScreenPage : ContentPage
    {
        public PrintToScreenPage(PrintToPageViewModel vm)
        {
            InitializeComponent();
            this.BindingContext = vm;
        }
    }
}
