using Windows.UI.Xaml.Controls;
using Final_excersise.ViewModels;

namespace Final_excersise.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = MainViewModel.SingleInstance;
        }

        private void ArticlesListView_OnItemClick(object sender, ItemClickEventArgs e)
        {
            (DataContext as MainViewModel)?.ArticleClickCommand.Execute(e.ClickedItem);
        }
    }
}
