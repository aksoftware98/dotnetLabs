using dotNetLabs.Server.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dotNetLabs.Repositories
{
    public interface IPlaylistsRepository
    {

        Task CreateAsync(Playlist playlist);
        void Remove(Playlist playlist);
        IEnumerable<Playlist> GetAll();
        IEnumerable<Video> GetAllVideosInPlaylist(string id);
        void RemoveVideoFromPlaylist(PlaylistVideo playlistVideo);
        Task AddVideoToPlaylistAsync(PlaylistVideo playlistVideo);
        Task<Playlist> GetByIdAsync(string id);

    }
}
