using dotNetLabs.Repositories;
using dotNetLabs.Server.Infrastructure;
using dotNetLabs.Server.Models.Mappers;
using dotNetLabs.Server.Models.Models;
using dotNetLabs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetLabs.Server.Services
{
    public class CommentsService : ICommentsService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IdentityOptions _identity; 

        public CommentsService(IUnitOfWork unitOfWork,
                               IdentityOptions identity)
        {
            _unitOfWork = unitOfWork;
            _identity = identity; 
        }

        public async Task<OperationResponse<CommentDetail>> CreateAsync(CommentDetail model)
        {
            // check for the parent comment if the comment is reply
            Comment parentComment = null; 
            if(!string.IsNullOrWhiteSpace(model.ParentCommentId))
            {
                parentComment = await _unitOfWork.Comments.GetByIdAsync(model.ParentCommentId);
                if (parentComment == null)
                    return new OperationResponse<CommentDetail>
                    {
                        IsSuccess = false,
                        Message = "Main comment cannot be found"
                    };
            }

            // Check for the video 
            if (string.IsNullOrWhiteSpace(model.VideoId))
                return new OperationResponse<CommentDetail> { IsSuccess = false, Message = "Video is required" };

            var video = await _unitOfWork.Videos.GetByIdAsync(model.VideoId); 
            if(video == null)
            {
                return new OperationResponse<CommentDetail>
                {
                    IsSuccess = false,
                    Message = "Video cannot be found"
                };
            }

            var newComment = new Comment
            {
                Content = model.Content,
                Likes = 0,
                ParentComment = parentComment,
                Video = video,
            };

            await _unitOfWork.Comments.CreateAsync(newComment);
            await _unitOfWork.CommitChangesAsync(_identity.UserId);
            model.Id = newComment.Id; 
            return new OperationResponse<CommentDetail>
            {
                IsSuccess = true,
                Message = "Comment submited successfully",
                Data = model
            };
        }

        public async Task<OperationResponse<CommentDetail>> EditAsync(CommentDetail model)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(model.Id);
            if (comment == null)
                return new OperationResponse<CommentDetail>
                {
                    IsSuccess = false,
                    Message = "Comment not found"
                };

            comment.Content = model.Content;

            await _unitOfWork.CommitChangesAsync(_identity.UserId);

            return new OperationResponse<CommentDetail>
            {
                IsSuccess = true,
                Data = model,
                Message = "Comment has been edited successfully!"
            };
        }

        public IEnumerable<CommentDetail> GetVideoComments(string videoId)
        {
            var comments = _unitOfWork.Comments.GetAllForVideo(videoId).ToArray();

            return comments.Select(c => c.ToCommentDetail());
        }

        public async Task<OperationResponse<CommentDetail>> RemoveAsync(string commentId)
        {
            var comment = await _unitOfWork.Comments.GetByIdAsync(commentId);
            if (comment == null)
                return new OperationResponse<CommentDetail>
                {
                    IsSuccess = false,
                    Message = "Comment not found"
                };

            _unitOfWork.Comments.Remove(comment);
            await _unitOfWork.CommitChangesAsync(_identity.UserId);

            return new OperationResponse<CommentDetail>
            {
                Data = comment.ToCommentDetail(),
                IsSuccess = true,
                Message = "Comment deleted successfully"
            };
        }
    }
}
