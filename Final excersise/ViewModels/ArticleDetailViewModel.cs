using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_excersise.Models;
using Final_excersise.Services;
using Library.Command;

namespace Final_excersise.ViewModels
{
    public class ArticleDetailViewModel : BaseViewModel
    {
        private IArticleService _articleService;
        
        public Article Article { get; set; }
        public RelayCommand ToArticleCommand { get; }
        public RelayCommand FavoriteCommand { get; }

        public ArticleDetailViewModel()
        {
            _articleService = ArticleService.SingleInstance;

            ToArticleCommand = new RelayCommand(GoToArticle);
            FavoriteCommand = new RelayCommand(OnFavorite);
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
            await _articleService.PutArticle((uint)article.Id);
        }
    }
}
