using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Credentials;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Final_excersise.ViewModels;
using Library.Command;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Final_excersise.Controls
{
    public enum SignInResult
    {
        SignInOK,
        SignInFail,
        SignInCancel,
        Nothing
    }

    public sealed partial class SignInContentDialog : ContentDialog
    {
        public SignInResult Result { get; private set; }
        
        private SignInViewModel VM => SignInViewModel.SingleInstance;

        public SignInContentDialog()
        {
            this.InitializeComponent();
            this.Opened += OnOpened;
            this.Closing += OnClosing;
            DataContext = this;
        }

        void OnOpened(ContentDialog sender, ContentDialogOpenedEventArgs args)
        {
            this.Result = SignInResult.Nothing;

            //TODO: If the user has logged in before, get it and populate the user name field.
        }

        private void OnClosing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            
        }

        private async void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // Ensure the user name and password fields aren't empty. If a required field
            // is empty, set args.Cancel = true to keep the dialog open.
            if (string.IsNullOrEmpty(VM.UserName))
            {
                args.Cancel = true;
                ErrorTextBlock.Text = "User name is required.";
            }
            else if (string.IsNullOrEmpty(VM.Password))
            {
                args.Cancel = true;
                ErrorTextBlock.Text = "Password is required.";
            }

            // If you're performing async operations in the button click handler,
            // get a deferral before you await the operation. Then, complete the
            // deferral when the async operation is complete.
            
            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            if (await VM.SignInAsync())
            {
                this.Result = SignInResult.SignInOK;
            }
            else
            {
                this.Result = SignInResult.SignInFail;
            }
            deferral.Complete();
        }

        private void OnCloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // User clicked Cancel, ESC, or the system back button.
            this.Result = SignInResult.SignInCancel;
        }

        // Handle the button clicks from the flyouts.
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.GetAttachedFlyout(this).Hide();
        }

        private void DiscardButton_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.GetAttachedFlyout(this).Hide();
        }

        // When the flyout closes, hide the sign in dialog, too.
        private void Flyout_Closed(object sender, object e)
        {
            this.Hide();
        }
    }
}
