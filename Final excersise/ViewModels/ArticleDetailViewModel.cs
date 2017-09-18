using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_excersise.Models;
using Library.Command;

namespace Final_excersise.ViewModels
{
    public class ArticleDetailViewModel : BaseViewModel
    {
        public ArticleDetailViewModel()
        {
            ToArticleCommand = new RelayCommand(GoToArticle);
        }

        public Article Article { get; set; }
        public RelayCommand ToArticleCommand { get; }

        private void GoToArticle(object o)
        {
            var url = o as string;
            if (url == null) return;

            var succes = Windows.System.Launcher.LaunchUriAsync(new Uri(url));
        }
    }
}
