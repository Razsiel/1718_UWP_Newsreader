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

        public ObservableIncrementalLoadingCollection<ArticleListViewModel> Articles { get; set; }
        public RelayCommand ArticleClickCommand { get; }
        public RelayCommand LogInCommand { get; set; }
        public RelayCommand LogOutCommand { get; set; }
        public RelayCommand RegisterCommand { get; set; }

        private MainViewModel()
        {
            _articleService = ArticleService.SingleInstance;
            _authenticationService = AuthenticationService.SingleInstance;

            Articles = new ObservableIncrementalLoadingCollection<ArticleListViewModel>();
            Articles.LoadMoreItemsEvent += ArticlesOnLoadMoreItemsEvent;

            ArticleClickCommand = new RelayCommand(OnArticleClick);
            LogInCommand = new RelayCommand(OnLogin);
            LogOutCommand = new RelayCommand(OnLogoutAsync);
            RegisterCommand = new RelayCommand(OnRegister);
        }

        private List<ArticleListViewModel> ArticlesOnLoadMoreItemsEvent(uint count)
        {
            var list = new List<ArticleListViewModel>();
            // On initial load get the first 20 articles
            if (_nextId < 0)
            {
                var articlesResult = _articleService.GetArticles().Result;
                _nextId = articlesResult.NextId;
                foreach (var article in articlesResult.Results)
                {
                    list.Add(new ArticleListViewModel(article));
                }
            }
            else
            {
                for (int i = 0; i < 20; i++) //replace '20' with 'count' to allow load-by-demand
                {
                    var articleResult = _articleService.GetArticleAsync((uint)_nextId).Result;
                    var article = articleResult.Results.FirstOrDefault(); // collection will always contain 1 element with this api call. Hence the 'FirstOrDefault' call.
                    if (article != null)
                    {
                        list.Add(new ArticleListViewModel(article));
                        //Prep the method for next time the api call is done.
                        _nextId = articleResult.NextId;
                    }
                }
            }
            
            return list;
        }

        public void OnArticleClick(object o)
        {
            // Cast the clicked object to the viewmodel underlying it
            var articleListViewModel = o as ArticleListViewModel;
            if (articleListViewModel?.Article == null) return;

            // Navigate to the detail page of the clicked article
            ((Frame) Window.Current.Content).Navigate(typeof(ArticleDetailPage), articleListViewModel.Article);
        }
        
        private async void OnLogin(object obj)
        {
            // show login dialog (handles the logging in and backend calls)
            var dialog = new SignInContentDialog();
            await dialog.ShowAsync();

            // If login was succesful -> show welcome message and refresh the page
            if (dialog.Result == SignInResult.SignInOK)
            {
                var message = new MessageDialog($"Welcome {Settings.Username}!");
                await message.ShowAsync();
                // Reload loaded articles before navigating
                _nextId = -1;
                Articles.Clear();
                await Articles.LoadMoreItemsAsync(0);
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
