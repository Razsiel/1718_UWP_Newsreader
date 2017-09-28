using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_excersise.Services;
using Library.Collection;

namespace Final_excersise.ViewModels
{
    public class FavoritesViewModel : BaseViewModel
    {
        private IArticleService _articleService;
        private bool _loaded;

        public ObservableIncrementalLoadingCollection<ArticleViewModel> Articles { get; set; }

        public FavoritesViewModel()
        {
            _articleService = ArticleService.SingleInstance;

            Articles = new ObservableIncrementalLoadingCollection<ArticleViewModel>();
            Articles.LoadMoreItemsEvent += ArticlesOnLoadMoreItemsEvent;
        }

        private List<ArticleViewModel> ArticlesOnLoadMoreItemsEvent(uint count)
        {
            if (_loaded) return null;

            var list = new List<ArticleViewModel>();
            var result = _articleService.GetLikedArticles().Result;
            foreach (var article in result.Results)
            {
                list.Add(new ArticleViewModel(article));
            }
            _loaded = true;
            return list;
        }
    }
}
