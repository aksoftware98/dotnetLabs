using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dotNetLabs.Repositories
{

    public interface IUnitOfWork
    {

        IUsersRepository Users { get; }
        IPlaylistsRepository Playlists { get; }
        IVideosRepository Videos { get; }
        Task CommitChangesAsync(string userId);
    }



}
