using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetLabs.Server.Services.Utilities
{
    public class LocalFilesStorageService : IFilesStorageService
    {
        public void RemoveFile(string filePath)
        {
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);

            File.Delete(fullPath); 
        }

        public async Task<string> SaveFileAsync(IFormFile formFile, string folderName)
        {
            string fileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

            string extension = Path.GetExtension(formFile.FileName);
            var allowedExtensions = new[] { ".jpg", ".png", ".bmp" };

            if (!allowedExtensions.Contains(extension))
                throw new BadImageFormatException();

            var newFileName = $"{Guid.NewGuid()}{extension}";

            using (var fileStream = new FileStream(Path.Combine(fileDirectory, newFileName), FileMode.Create, FileAccess.Write))
            {
                await formFile.CopyToAsync(fileStream);
                return $"{folderName}/{newFileName}"; 
            }

        }
    }
}
