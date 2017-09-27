using Final_excersise.Models;

namespace Final_excersise.ViewModels
{
    public class ArticleListViewModel : BaseViewModel
    {
        public Article Article { get; set; }

        public ArticleListViewModel(Article article)
        {
            Article = article;
        }
    }
}