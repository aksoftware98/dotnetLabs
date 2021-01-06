using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dotNetLabs.Server.Services.Utilities
{
    public interface IFilesStorageService
    {

        Task<string> SaveFileAsync(IFormFile formFile, string folderName);

        void RemoveFile(string filePath); 

    }
}
