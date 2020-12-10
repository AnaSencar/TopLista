using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TopLista.Web.Interfaces
{
    public interface ILoginRepository
    {
        Task<bool> LogUserIn(string username, string password);
        string GetLoggedUsername(ClaimsPrincipal user);
        Task SignUserOut();
        bool IsAdmin(ClaimsPrincipal user);
    }
}
