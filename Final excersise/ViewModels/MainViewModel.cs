using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_excersise.Models;
using Library.Collection;

namespace Final_excersise.ViewModels
{
    public class MainViewModel
    {
        public static MainViewModel SingleInstance { get; } = new MainViewModel();

        private MainViewModel()
        {
            Articles = new ObservableIncrementalLoadingCollection<Article>();
        }

        public ObservableIncrementalLoadingCollection<Article> Articles { get; set; }

    }
}
