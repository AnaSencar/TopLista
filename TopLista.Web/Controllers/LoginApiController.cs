using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TopLista.Web.Interfaces;
using TopLista.Web.ViewModels;

namespace TopLista.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginApiController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;

        public LoginApiController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        [HttpGet]
        public ActionResult<UserInfoViewModel> GetLoggedUser()
        {
            UserInfoViewModel user = new UserInfoViewModel()
            {
                Username = _loginRepository.GetLoggedUsername(User),
                IsAdmin = _loginRepository.IsAdmin(User)
            };
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> SignUserIn([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _loginRepository.LogUserIn(model.Username, model.Password);
                if (loginResult)
                {
                    return NoContent();
                }
                else
                {
                    return Unauthorized();
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> SignUserOut()
        {
            await _loginRepository.SignUserOut();
            return NoContent();
        }

    }
}