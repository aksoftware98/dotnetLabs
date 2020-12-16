using dotNetLabs.Server.Models;
using dotNetLabs.Server.Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dotNetLabs.Repositories
{

    public interface IUnitOfWork
    {

        IUsersRepository Users { get; }
        IPlaylistsRepository Playlists { get; }

        Task CommitChangesAsync(string userId);
    }

    public class EfUnitOfWork : IUnitOfWork
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db; 

        public EfUnitOfWork(UserManager<ApplicationUser> userManager,
                            RoleManager<IdentityRole> roleManager,
                            ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db; 
        }

        private IUsersRepository _users;
        public IUsersRepository Users 
        {
            get
            {
                if (_users == null)
                    _users = new IdentityUsersRepository(_userManager, _roleManager);

                return _users; 
            }
        }

        private IPlaylistsRepository _playlists;
        public IPlaylistsRepository Playlists
        {
            get
            {
                if (_playlists == null)
                    _playlists = new PlaylistsRepository(_db);

                return _playlists;
            }
        }

        public async Task CommitChangesAsync(string userId)
        {
            await _db.SaveChangesAsync(userId);
        }
    }



}
