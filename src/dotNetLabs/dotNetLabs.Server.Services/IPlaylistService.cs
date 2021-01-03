using dotNetLabs.Repositories;
using dotNetLabs.Server.Infrastructure;
using dotNetLabs.Server.Models.Models;
using dotNetLabs.Server.Models.Mappers;
using dotNetLabs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetLabs.Server.Services
{
    public interface IPlaylistService
    {

        Task<OperationResponse<PlaylistDetail>> CreateAsync(PlaylistDetail model);
        Task<OperationResponse<PlaylistDetail>> UpdateAsync(PlaylistDetail model);
        Task<OperationResponse<PlaylistDetail>> RemoveAsync(string id);
        CollectionResponse<PlaylistDetail> GetAllPlaylists(int pageNumber = 1, int pageSize = 10); 
      
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

        public CollectionResponse<PlaylistDetail> GetAllPlaylists(int pageNumber = 1, int pageSize = 10)
        {
            // Validation 
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 5)
                pageSize = 5;

            if (pageSize > 50)
                pageSize = 50; 

            var playlists = _unitOfWork.Playlists.GetAll();
            int playlistsCount = playlists.Count();

            var playlistsInPage = playlists
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .Select(p => p.ToPlaylistDetail());

            int pagesCount = playlistsCount / pageSize;
            if ((playlistsCount % pageSize) != 0)
                pagesCount++; 

            return new CollectionResponse<PlaylistDetail>
            {
                IsSuccess = true,
                Message = "Playlists retrieved successfully!",
                Records = playlistsInPage.ToArray(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                PagesCount = pagesCount
            };
        }

        public async Task<OperationResponse<PlaylistDetail>> RemoveAsync(string id)
        {
            var playlist = await _unitOfWork.Playlists.GetByIdAsync(id);

            if (playlist == null)
                return new OperationResponse<PlaylistDetail>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Playlist not found"
                };

            _unitOfWork.Playlists.Remove(playlist);

            await _unitOfWork.CommitChangesAsync(_identity.UserId);

            return new OperationResponse<PlaylistDetail>
            {
                IsSuccess = true,
                Message = "Playlist has been deleted successfully!",
                Data = playlist.ToPlaylistDetail(), 
            };
        }

        public async Task<OperationResponse<PlaylistDetail>> UpdateAsync(PlaylistDetail model)
        {
            var playlist = await _unitOfWork.Playlists.GetByIdAsync(model.Id);

            if (playlist == null)
                return new OperationResponse<PlaylistDetail>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Playlist not found"
                };

            playlist.Name = model.Name;
            playlist.Description = model.Description;

            await _unitOfWork.CommitChangesAsync(_identity.UserId);

            return new OperationResponse<PlaylistDetail>
            {
                IsSuccess = true,
                Message = "Playlist has been updated successfully!",
                Data = model
            }; 
        }
    }
}
