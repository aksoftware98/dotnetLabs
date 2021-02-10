using dotNetLabs.Server.Models.Models;
using dotNetLabs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotNetLabs.Server.Models.Mappers
{
    public static class CommentsMapper 
    {

        public static CommentDetail ToCommentDetail(this Comment comment)
        {
            return new CommentDetail
            {
                CommentDate = comment.CreatationDate,
                Content = comment.Content,
                ParentCommentId = comment.ParentCommentId,
                Id = comment.Id,
                Username = $"{comment.CreatedByUser.FirstName} {comment.CreatedByUser.LastName}",
                VideoId = comment.VideoId,
                Replys = comment.Replys?.Select(c => c.ToCommentDetail()),
                UserId = comment.CreatedByUserId,
            };
        }
    }
}
