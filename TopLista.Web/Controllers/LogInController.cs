using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TopLista.Web.Controllers
{
    [AllowAnonymous]
    public class LogInController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public LogInController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var loggedUser = _userManager.GetUserName(User);
            if (loggedUser != null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
    }
}