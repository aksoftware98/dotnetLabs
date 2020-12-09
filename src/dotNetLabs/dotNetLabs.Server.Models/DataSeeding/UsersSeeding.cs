using dotNetLabs.Server.Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dotNetLabs.Server.Models.DataSeeding
{
    public class UsersSeeding
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersSeeding(UserManager<ApplicationUser> userManager,
                            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager; 
        }

        public async Task SeedData()
        {
            if (await _roleManager.FindByNameAsync("Admin") != null)
                return;

            // Create role 
            var adminRole = new IdentityRole { Name = "Admin" };
            await _roleManager.CreateAsync(adminRole);

            var userRole = new IdentityRole { Name = "User" };
            await _roleManager.CreateAsync(userRole);

            // Create user 
            var admin = new ApplicationUser
            {
                Email = "test@dotnetlabs.com",
                UserName = "test@dotnetlabs.com",
                FirstName = "John",
                LastName = "Smith"
            };
            await _userManager.CreateAsync(admin, "Test.123");

            await _userManager.AddToRoleAsync(admin, "Admin"); 
        }

    }
}
