using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_excersise.Models;

namespace Final_excersise.Services.Results
{
    public class ArticlesResult
    {
        public List<Article> Results { get; set; }
        public int NextId { get; set; }
    }
}
