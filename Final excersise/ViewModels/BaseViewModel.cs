using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Popups;
using Final_excersise.Models;

namespace Final_excersise.ViewModels
{
    public abstract class BaseViewModel
    {
        public Settings Settings { get; set; }

        protected BaseViewModel()
        {
            Settings = Settings.SingleInstance;
        }

        protected async void OnNetworkFailure()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
             {
                 var messageDialog =
                     new MessageDialog(
                         $"Er is helaas iets mis gegaan met het ophalen van gegevens. Check je internet verbinding of probeer het later opnieuw.");
                 await messageDialog.ShowAsync();
             });
        }
    }
}
