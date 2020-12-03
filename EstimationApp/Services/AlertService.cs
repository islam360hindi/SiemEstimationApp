using System;
using System.Threading.Tasks;

namespace EstimationApp.Services
{
    public class AlertService : IAlertService
    {
        public Task<string> ShowActionSheetAsync(string titile, string[] actions, string cancelText = null, string destructiveAction = null)
        {
            return App.Current.MainPage.DisplayActionSheet(titile, cancelText, destructiveAction, actions);
        }

        public Task<bool> ShowAlertAsync(string title, string message, string acceptButtonText, string cancelButtonText)
        {
            return App.Current.MainPage.DisplayAlert(title, message, acceptButtonText, cancelButtonText);
        }
    }
}
