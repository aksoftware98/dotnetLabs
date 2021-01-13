using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace dotNetLabs.Shared
{
    public class CommentDetail
    {
        public string Id { get; set; }

        [Required]
        [StringLength(5000)]
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }

        public string Username { get; set; }
        public IEnumerable<CommentDetail> Replys { get; set; }

        public string VideoId { get; set; }
        public string ParentCommentId { get; set; }

    }
}
