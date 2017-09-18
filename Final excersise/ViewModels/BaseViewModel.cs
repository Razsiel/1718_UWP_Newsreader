using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_excersise.Models;

namespace Final_excersise.ViewModels
{
    public abstract class BaseViewModel
    {
        protected BaseViewModel()
        {
            Settings = Settings.SingleInstance;
        }

        public Settings Settings { get; set; }
    }
}
