using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace dotNetLabs.Server.Models.Models
{
    public class Comment : UserRecord
    {

        [Required]
        [StringLength(5000)]
        public string Content { get; set; }

        public int Likes { get; set; }

        public virtual List<Comment> Replys { get; set; }

        public virtual Comment ParentComment { get; set; }

        [ForeignKey(nameof(ParentComment))]
        public string ParentCommentId { get; set; }

        public virtual Video Video { get; set; }

        [ForeignKey(nameof(Video))]
        public string VideoId { get; set; }

    }
}
