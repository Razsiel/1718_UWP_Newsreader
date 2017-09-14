using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Final_excersise.Models;
using Final_excersise.Services;
using Final_excersise.Views;
using Library.Collection;
using Library.Command;

namespace Final_excersise.ViewModels
{
    public class MainViewModel
    {
        public static MainViewModel SingleInstance { get; } = new MainViewModel();

        private int _nextId = -1;
        private readonly IArticleService _articleService;

        private MainViewModel()
        {
            _articleService = ArticleService.SingleInstance;
            Articles = new ObservableIncrementalLoadingCollection<Article>();
            Articles.LoadMoreItemsEvent += ArticlesOnLoadMoreItemsEvent;

            ArticleClickCommand = new RelayCommand(OnArticleClick);
        }

        public ObservableIncrementalLoadingCollection<Article> Articles { get; set; }
        public Settings Settings { get; set; }

        public RelayCommand ArticleClickCommand { get; }

        private List<Article> ArticlesOnLoadMoreItemsEvent(uint count)
        {
            var list = new List<Article>();
            // On initial load get the first 20 articles
            if (_nextId < 0)
            {
                var articlesResult = _articleService.GetArticles().Result;
                _nextId = articlesResult.NextId;
                foreach (var article in articlesResult.Results)
                {
                    list.Add(article);
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    var articleResult = _articleService.GetArticleAsync((uint)_nextId).Result;
                    _nextId = articleResult.NextId;

                    var article = articleResult.Results.FirstOrDefault();
                    if (article != null)
                    {
                        list.Add(article);
                    }
                    
                }
            }
            
            return list;
        }

        public void OnArticleClick(object o)
        {
            var article = o as Article;
            if (article == null) return;

            ((Frame) Window.Current.Content).Navigate(typeof(ArticleDetailPage), article);
        }
    }
}
