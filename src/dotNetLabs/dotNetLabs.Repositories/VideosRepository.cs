using dotNetLabs.Server.Models;
using dotNetLabs.Server.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotNetLabs.Repositories
{
    public class VideosRepository : IVideosRepository
    {

        private readonly ApplicationDbContext _db;

        public VideosRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Video video)
        {
            await _db.Videos.AddAsync(video);
        }

        public IEnumerable<Video> GetAll()
        {
            return _db.Videos.Include(v => v.Tags);
        }

        public async Task<Video> GetByIdAsync(string id)
        {
            return await _db.Videos.Include(v => v.Tags).SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Video> GetByTitleAsync(string title)
        {
            return await _db.Videos.SingleOrDefaultAsync(v => v.Title == title);
        }

        public void Remove(Video video)
        {
            _db.Videos.Remove(video);
        }

        public void RemoveTags(Video video)
        {
            _db.Tags.RemoveRange(video.Tags); 
        }
    }
}
