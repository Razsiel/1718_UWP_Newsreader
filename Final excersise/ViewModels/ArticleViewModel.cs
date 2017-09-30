using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Final_excersise.Models;
using Final_excersise.Services;
using Library.Command;

namespace Final_excersise.ViewModels
{
    public class ArticleViewModel : BaseViewModel
    {
        private IArticleService _articleService;
        
        public Article Article { get; set; }
        public RelayCommand ToArticleCommand { get; }
        public RelayCommand FavoriteCommand { get; }
        public RelayCommand ShareCommand { get; }

        public ArticleViewModel(Article article)
        {
            _articleService = ArticleService.SingleInstance;

            Article = article;
            ToArticleCommand = new RelayCommand(GoToArticle);
            FavoriteCommand = new RelayCommand(OnFavorite);
            ShareCommand = new RelayCommand(OnShare);
        }

        private void GoToArticle(object o)
        {
            var url = o as string;
            if (url == null) return;

            var succes = Windows.System.Launcher.LaunchUriAsync(new Uri(url));
        }
        
        private async void OnFavorite(object obj)
        {
            var article = obj as Article;
            if (article == null) return;
            
            // Update server article
            if (!article.IsLiked)
            {
                await _articleService.FavoriteArticle((uint) article.Id, HttpMethod.Put);
            }
            else
            {
                await _articleService.FavoriteArticle((uint) article.Id, HttpMethod.Delete);
            }
            

            // Update local article from server
            var result = await _articleService.GetArticleAsync((uint) Article.Id);
            var serverArticle = result.Results.FirstOrDefault();
            if (serverArticle != null)
                Article.IsLiked = serverArticle.IsLiked;
        }
        
        private void OnShare(object obj)
        {
            var article = obj as Article;
            if (article == null) return;

            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += delegate(DataTransferManager sender, DataRequestedEventArgs args)
            {
                var request = args.Request;
                request.Data.Properties.Title = article.Title;
                request.Data.SetText($"{article.Title}\n\n{article.Summary}");
            };
            DataTransferManager.ShowShareUI();
        }
    }
}
