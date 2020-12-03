using System;
using System.Threading.Tasks;
using EstimationApp.Localization;

namespace EstimationApp.Services
{
    public interface IAlertService
    {
        /// <summary>
        /// return if accept button selected or not
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="acceptButtonText"></param>
        /// <param name="cancelButtonText"></param>
        /// <returns></returns>
        Task<bool> ShowAlertAsync(string title, string message, string acceptButtonText = "OK", string cancelButtonText = "Cancel");

        Task<string> ShowActionSheetAsync(string titile, string[] actions, string cancelText = null, string destructiveAction = null);
    }
}
