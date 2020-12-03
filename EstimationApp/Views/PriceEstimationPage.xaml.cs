using System;
using System.Collections.Generic;
using EstimationApp.ViewModels;
using Xamarin.Forms;

namespace EstimationApp.Views
{
    public partial class PriceEstimationPage : ContentPage
    {
        public PriceEstimationPage(PriceEstimationViewModel priceEstimationViewModel)
        {
            InitializeComponent();
            this.BindingContext = priceEstimationViewModel;
        }
    }
}
