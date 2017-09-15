using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Final_excersise.Models;
using Final_excersise.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Final_excersise.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ArticleDetailPage : Page
    {
        public ArticleDetailPage()
        {
            this.InitializeComponent();
            DataContext = this;
        }

        private ArticleDetailViewModel VM = new ArticleDetailViewModel();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var article = e.Parameter as Article;
            if (article == null) return;

            VM = new ArticleDetailViewModel
            {
                Article = article
            };
        }

        private void RelatedUris_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var url = e.ClickedItem as string;
            if (url == null) return;

            var viewModel = DataContext as ArticleDetailViewModel;
            if (viewModel == null) return;

            viewModel.ToArticleCommand.Execute(url);
        }
    }
}
