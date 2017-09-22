using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Final_excersise.Controls;
using Final_excersise.Models;
using Final_excersise.Services;
using Final_excersise.Views;
using Library.Collection;
using Library.Command;

namespace Final_excersise.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public static MainViewModel SingleInstance { get; } = new MainViewModel();

        private int _nextId = -1;
        private readonly IArticleService _articleService;
        private readonly IAuthenticationService _authenticationService;

        private MainViewModel()
        {
            _articleService = ArticleService.SingleInstance;
            _authenticationService = AuthenticationService.SingleInstance;

            Articles = new ObservableIncrementalLoadingCollection<Article>();
            Articles.LoadMoreItemsEvent += ArticlesOnLoadMoreItemsEvent;

            ArticleClickCommand = new RelayCommand(OnArticleClick);

            LogInCommand = new RelayCommand(OnLogin);
            LogOutCommand = new RelayCommand(OnLogoutAsync);
            RegisterCommand = new RelayCommand(OnRegister);
        }

        public ObservableIncrementalLoadingCollection<Article> Articles { get; set; }

        public RelayCommand ArticleClickCommand { get; }
        public RelayCommand LogInCommand { get; set; }
        public RelayCommand LogOutCommand { get; set; }
        public RelayCommand RegisterCommand { get; set; }

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
        
        private async void OnLogin(object obj)
        {
            // show dialog here
            var dialog = new SignInContentDialog();
            await dialog.ShowAsync();

            if (dialog.Result == SignInResult.SignInOK)
            {
                var message = new MessageDialog($"Welcome {Settings.Username}!");
                await message.ShowAsync();
                ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
            }
        }

        private async void OnLogoutAsync(object o)
        {
            var dialog = new MessageDialog("Are you sure you want to log out?");
            // Yes button
            dialog.Commands.Add(new UICommand("Yes", command =>
            {
                _authenticationService.LogOut();
                ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
            }));
            dialog.DefaultCommandIndex = 0;

            // No button
            dialog.Commands.Add(new UICommand("No", command =>
            {
                // do nothing
            }));
            dialog.CancelCommandIndex = (uint) (dialog.Commands.Count - 1);

            await dialog.ShowAsync();
        }

        private void OnRegister(object obj)
        {
            // show dialog here
            //_authenticationService.Register();
        }
    }
}
