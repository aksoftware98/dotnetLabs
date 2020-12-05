using dotNetLabs.Server.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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


            base.OnModelCreating(builder);
        }

    }
}
