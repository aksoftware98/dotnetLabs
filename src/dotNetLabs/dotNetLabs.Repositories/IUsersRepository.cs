using dotNetLabs.Server.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dotNetLabs.Repositories
{
    public interface IUsersRepository
    {

        Task<ApplicationUser> GetUserByIdAsync(string id);

        Task<ApplicationUser> GetUserByEmailAsync(string email);

        Task CreateUserAsync(ApplicationUser user, string password, string role);

        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

        Task<string> GetUserRoleAsync(ApplicationUser user);
    }

}
