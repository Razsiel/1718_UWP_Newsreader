using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.UI.Xaml.Controls;
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

        private ArticleViewModel VM;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var article = e.Parameter as Article;
            if (article == null) return;

            VM = new ArticleViewModel(article);
        }

        private void RelatedUris_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var url = e.ClickedItem as string;
            if (url == null) return;

            var viewModel = DataContext as ArticleViewModel;
            if (viewModel == null) return;

            viewModel.ToArticleCommand.Execute(url);
        }
    }
}
