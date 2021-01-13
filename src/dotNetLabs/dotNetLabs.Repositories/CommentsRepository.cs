using dotNetLabs.Server.Models;
using dotNetLabs.Server.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetLabs.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {

        private readonly ApplicationDbContext _db;

        public CommentsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Comment comment)
        {
            await _db.Comments.AddAsync(comment); 
        }

        public async Task<Comment> GetByIdAsync(string commentId)
        {
            return await _db.Comments.
                            Include(c => c.CreatedByUser)
                           .Include(c => c.Replys)
                           .ThenInclude(r => r.CreatedByUser)
                           .SingleOrDefaultAsync(c => c.Id == commentId); 
        }

        public IEnumerable<Comment> GetAllForVideo(string videoId)
        {
            return _db.Comments
                           .Include(c => c.CreatedByUser)
                           .Include(c => c.Replys)
                           .ThenInclude(r => r.CreatedByUser)
                           .Where(c => c.VideoId == videoId && c.ParentComment == null); 
        }

        public void Remove(Comment comment)
        {
            if (comment.Replys != null && comment.Replys.Any())
                _db.Comments.RemoveRange(comment.Replys); 

            _db.Comments.Remove(comment); 
        }
    }


}
