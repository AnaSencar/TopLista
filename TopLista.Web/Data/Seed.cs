using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopLista.Web.Data
{
    public class Seed
    {
        public static async Task CreateAdmin(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var roleExists = await roleManager.RoleExistsAsync("admin");
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var existingUser = await userManager.FindByEmailAsync("sencar.ana@gmail.com");
            if (existingUser == null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin",
                    Email = "sencar.ana@gmail.com",
                    EmailConfirmed = true,
                };
                var createdUser = await userManager.CreateAsync(user, "Pass123");
                if (createdUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "admin");
                }
            }
        }
    }
}
