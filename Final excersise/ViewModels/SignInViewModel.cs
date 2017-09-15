using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_excersise.Services;
using Library.Model;

namespace Final_excersise.ViewModels
{
    public class SignInViewModel : BindableBase
    {
        public static SignInViewModel SingleInstance { get; } = new SignInViewModel();

        private readonly IAuthenticationService _authenticationService;

        private SignInViewModel()
        {
            _authenticationService = AuthenticationService.SingleInstance;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsSaveUserName { get; set; }

        public async Task<bool> SignInAsync()
        {
            return await _authenticationService.Login(UserName, Password);
        }
    }
}
