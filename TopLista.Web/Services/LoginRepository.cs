using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLista.Web.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TopLista.Web.Services
{
    public class LoginRepository : ILoginRepository
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public LoginRepository(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public string GetLoggedUsername(ClaimsPrincipal user)
        {
            return _userManager.GetUserName(user);
        }

        public bool IsAdmin(ClaimsPrincipal user)
        {
            return user.IsInRole("admin");
        }

        public async Task<bool> LogUserIn(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, true, false);
            return result.Succeeded;
        }

        public async Task SignUserOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
