using dotNetLabs.Server.Models.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dotNetLabs.Repositories
{
    public interface ICommentsRepository
    {

        Task CreateAsync(Comment comment);
        void Remove(Comment Comment);
        IEnumerable<Comment> GetAllForVideo(string videoId);
        Task<Comment> GetByIdAsync(string commentId);
    }


}
