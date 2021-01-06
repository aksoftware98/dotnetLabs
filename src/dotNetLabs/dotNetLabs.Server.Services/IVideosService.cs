using dotNetLabs.Repositories;
using dotNetLabs.Server.Infrastructure;
using dotNetLabs.Server.Models.Models;
using dotNetLabs.Server.Services.Utilities;
using dotNetLabs.Shared;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetLabs.Server.Services
{
    public interface IVideosService
    {
        Task<OperationResponse<VideoDetail>> CreateAsync(VideoDetail model);
        Task<OperationResponse<VideoDetail>> UpdateAsync(VideoDetail model);
        Task<OperationResponse<VideoDetail>> RemoveAsync(string id);
        CollectionResponse<VideoDetail> GetAllVideos(int pageNumber = 1, int pageSize = 10);
    }

    public class VideosService : IVideosService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IdentityOptions _identity;
        private readonly IFilesStorageService _storage;
        
        public VideosService(IUnitOfWork unitOfWork,
                             IdentityOptions identity,
                             IFilesStorageService storage)
        {
            _unitOfWork = unitOfWork;
            _identity = identity;
            _storage = storage;
        }

        public async Task<OperationResponse<VideoDetail>> CreateAsync(VideoDetail model)
        {
            // Validation 
            var similarVideo = await _unitOfWork.Videos.GetByTitleAsync(model.Title);
            if (similarVideo != null)
                return new OperationResponse<VideoDetail>
                {
                    IsSuccess = false,
                    Message = "Failed to create the video, another one has the same title"
                };

            if (model.ThumpFile == null)
                return new OperationResponse<VideoDetail>
                {
                    IsSuccess = false,
                    Message = "Thump image is required"
                };

            string thumpUrl = string.Empty; 
            try
            {
                thumpUrl = await _storage.SaveFileAsync(model.ThumpFile, "Uploads"); 
            }
            catch (BadImageFormatException)
            {
                return new OperationResponse<VideoDetail>
                {
                    IsSuccess = false,
                    Message = "Please upload a valid image file"
                };
            }

            var video = new Video
            {
                Category = model.Category,
                Description = model.Description,
                Likes = 0,
                Views = 0,
                Privacy = model.Privacy,
                ThumpUrl = thumpUrl,
                Title = model.Title,
                VideoUrl = model.VideoUrl,
                PublishingDate = model.PublishingDate, 
                Tags = model.Tags?.Select(t => new Tag
                {
                    Name = t,
                }).ToList(),
            };

            await _unitOfWork.Videos.CreateAsync(video);
            await _unitOfWork.CommitChangesAsync(_identity.UserId);

            model.Id = video.Id;
            model.ThumpFile = null;
            // TODO: Get the URL of the running API and replace the hardcoded one 
            model.ThumpUrl = $"https://localhost:44377/{thumpUrl}";

            return new OperationResponse<VideoDetail>
            {
                IsSuccess = true,
                Message = "Video has been created successfully!",
                Data = model
            }; 
        }

        public CollectionResponse<VideoDetail> GetAllVideos(int pageNumber = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse<VideoDetail>> RemoveAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse<VideoDetail>> UpdateAsync(VideoDetail model)
        {
            throw new NotImplementedException();
        }
    }
}
