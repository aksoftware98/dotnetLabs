using dotNetLabs.Server.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dotNetLabs.Repositories
{
    public interface IVideosRepository
    {

        Task CreateAsync(Video video);
        void Remove(Video video);
        IEnumerable<Video> GetAll();
        Task<Video> GetByTitleAsync(string name); 
        Task<Video> GetByIdAsync(string id);
        void RemoveTags(Video video);

    }
}
