using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Final_excersise.Models;
using Final_excersise.Services.Results;
using Newtonsoft.Json;

namespace Final_excersise.Services
{
    public class ArticleService : ServiceBase, IArticleService
    {
        public static ArticleService SingleInstance { get; } = new ArticleService();

        private ArticleService()
        {
        }

        protected override string ServiceUri => "api/articles";

        public async Task<ArticlesResult> GetArticles()
        {
            var uri = ServiceUri;
            var result = await base.GetJsonResultAsync<ArticlesResult>(uri);
            return result;
        }

        public async Task<ArticlesResult> GetArticleAsync(uint id)
        {
            var uri = $"{ServiceUri}/{id}";
            var result = await base.GetJsonResultAsync<ArticlesResult>(uri);
            return result;
        }
    }

    public interface IArticleService
    {
        Task<ArticlesResult> GetArticles();
        Task<ArticlesResult> GetArticleAsync(uint id);
    }
}
