using dotNetLabs.Server.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotNetLabs.Server.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<PlaylistVideo> PlaylistVideos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(p => p.CreatedVideos)
                .WithOne(p => p.CreatedByUser)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUser>()
                .HasMany(p => p.ModifiedVideos)
                .WithOne(p => p.ModifiedByUser)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUser>()
               .HasMany(p => p.CreatedPlaylists)
               .WithOne(p => p.CreatedByUser)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUser>()
                .HasMany(p => p.ModifiedPlaylists)
                .WithOne(p => p.ModifiedByUser)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUser>()
             .HasMany(p => p.CreatedComments)
             .WithOne(p => p.CreatedByUser)
             .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ApplicationUser>()
                .HasMany(p => p.ModifiedComments)
                .WithOne(p => p.ModifiedByUser)
                .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<ApplicationUser>().ToTable("MyUsers");

            base.OnModelCreating(builder);
        }

        private string _userId = null; 

        public async Task SaveChangesAsync(string userId)
        {
            _userId = userId;
            await SaveChangesAsync();
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is UserRecord)
                {
                    var userRecord = (UserRecord)item.Entity;

                    switch (item.State)
                    {
                        case EntityState.Detached:
                            break;
                        case EntityState.Unchanged:
                            break;
                        case EntityState.Deleted:
                            break;
                        case EntityState.Modified:
                            userRecord.ModificationDate = DateTime.UtcNow;
                            userRecord.ModifiedByUserId = _userId;
                            break;
                        case EntityState.Added:
                            userRecord.ModificationDate = DateTime.UtcNow;
                            userRecord.ModifiedByUserId = _userId;
                            userRecord.CreatationDate = DateTime.UtcNow;
                            userRecord.CreatedByUserId = _userId;
                            break;
                        default:
                            break;
                    }
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
