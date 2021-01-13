using dotNetLabs.Shared;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dotNetLabs.Server.Services
{
    public interface ICommentsService
    {

        Task<OperationResponse<CommentDetail>> CreateAsync(CommentDetail model);
        Task<OperationResponse<CommentDetail>> EditAsync(CommentDetail model);
        Task<OperationResponse<CommentDetail>> RemoveAsync(string commentId);
        IEnumerable<CommentDetail> GetVideoComments(string videoId); 

    }
}
