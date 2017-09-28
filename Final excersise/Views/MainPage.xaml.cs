using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Final_excersise.ViewModels;

namespace Final_excersise.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = this;
            //NavigationCacheMode = NavigationCacheMode.Required; // Enable to allow frame.GoBack() to return to the correct point in this page, unfortunately breaks dynamic binding page styling (ex. login/register btn)...
        }

        private MainViewModel VM => MainViewModel.SingleInstance;

        private void ArticlesListView_OnItemClick(object sender, ItemClickEventArgs e)
        {
            VM.ArticleClickCommand.Execute(e.ClickedItem);
        }

        private void HamburgerButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MainSplitView.IsPaneOpen = false;
            base.OnNavigatedTo(e);
        }
    }
}
