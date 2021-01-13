using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace dotNetLabs.Shared
{
    public class VideoDetail
    {

        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(5000)]
        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string VideoUrl { get; set; }

        public string ThumpUrl { get; set; } // From server to client 
        public IFormFile ThumpFile { get; set; } // Submit video from client to server 
        public int Views { get; set; }
        public int Likes { get; set; }
        public DateTime PublishingDate { get; set; }
        public Category Category { get; set; }
        public VideoPrivacy Privacy { get; set; }

        //public virtual List<PlaylistVideo> PlaylistVideos { get; set; }
        //public virtual List<Comment> Comments { get; set; }
        public virtual List<string> Tags { get; set; }

        public IEnumerable<CommentDetail> Comments { get; set; }
    }
}
