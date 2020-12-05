using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace dotNetLabs.Server.Models.Models
{
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {
            CreatedPlaylists = new List<Playlist>();
            ModifiedPlaylists = new List<Playlist>();
        }

        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25)]
        public string LastName { get; set; }

        // Relationships 
        public virtual List<Playlist> CreatedPlaylists { get; set; }
        public virtual List<Playlist> ModifiedPlaylists { get; set; }

        public virtual List<Video> CreatedVideos { get; set; }
        public virtual List<Video> ModifiedVideos { get; set; }

        public virtual List<Comment> CreatedComments { get; set; }
        public virtual List<Comment> ModifiedComments { get; set; }
    }
}
