using dotNetLabs.Repositories;
using dotNetLabs.Server.Infrastructure;
using dotNetLabs.Server.Models.Models;
using dotNetLabs.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dotNetLabs.Server.Services
{
    public interface IPlaylistService
    {

        Task<OperationResponse<PlaylistDetail>> CreateAsync(PlaylistDetail model);
        Task<OperationResponse<PlaylistDetail>> UpdateAsync(PlaylistDetail model);
        Task<OperationResponse<PlaylistDetail>> RemoveAsync(string id);
        //Task<OperationResponse<PlaylistDetail>> (PlaylistDetail model);

    }

    public class PlaylistsService : IPlaylistService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IdentityOptions _identity; 

        public PlaylistsService(IUnitOfWork unitOfWork,
                                IdentityOptions identity)
        {
            _unitOfWork = unitOfWork;
            _identity = identity;
        }

        public async Task<OperationResponse<PlaylistDetail>> CreateAsync(PlaylistDetail model)
        {
            
            var playlist = new Playlist
            {
                Name = model.Name,
                Description = model.Description
            };

            await _unitOfWork.Playlists.CreateAsync(playlist);
            await _unitOfWork.CommitChangesAsync(_identity.UserId);

            model.Id = playlist.Id;

            return new OperationResponse<PlaylistDetail>
            {
                IsSuccess = true,
                Message = "Playlist created successfully!",
                Data = model
            };
        }

        public Task<OperationResponse<PlaylistDetail>> RemoveAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse<PlaylistDetail>> UpdateAsync(PlaylistDetail model)
        {
            throw new NotImplementedException();
        }
    }
}
