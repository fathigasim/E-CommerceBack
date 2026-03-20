using MediaRTutorialApplication.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceInfrastructure.Services
{
    // Implementation in Infrastructure layer
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _basePath;
        private readonly string _baseUrl;
        private readonly ILogger<LocalFileStorageService> _logger;

        public LocalFileStorageService(
            IConfiguration configuration,
            ILogger<LocalFileStorageService> logger, IWebHostEnvironment env)
        {
            //_basePath =configuration["FileStorage:Path"] ?? "/uploads/products";
            _basePath = Path.Combine(env.WebRootPath, "uploads", "products");
            _baseUrl = configuration["FileStorage:BaseUrl"] ?? "/uploads/products";
            _logger = logger;

            if (!Directory.Exists(_basePath))
                Directory.CreateDirectory(_basePath);
        }
        public async Task<string> UploadFileAsync(
       Stream fileStream, string fileName, string contentType,
       CancellationToken cancellationToken = default)
        {
            // Generate unique file name to avoid collisions
            var extension = Path.GetExtension(fileName);
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(_basePath, uniqueFileName);

            using var output = new FileStream(filePath, FileMode.Create);
            await fileStream.CopyToAsync(output, cancellationToken);

            return $"{uniqueFileName}";//$"{_baseUrl}/{uniqueFileName}";
        }

        public Task DeleteFileAsync(string fileUrl,
      CancellationToken cancellationToken = default)
        {

            var fileName = Path.GetFileName(fileUrl);
            if (string.IsNullOrEmpty(fileName))
            {
                return Task.CompletedTask; 
            }
            var filePath = Path.Combine(_basePath, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);

            return Task.CompletedTask;
        }

        public async Task<string> UploadAsync(
            byte[] fileContent,
            string fileName,
            string contentType
            )
        {
            var extension = contentType switch
            {
                "image/jpg" => ".jpg",
                "image/jpeg" => ".jpg",
                "image/png" => ".png",
                "image/gif" => ".gif",
                "image/webp" => ".webp",
                _ => ".bin"
            };

             fileName = $"{Guid.NewGuid()}-{extension}";
            var filePath = Path.Combine(_basePath, fileName);

            await File.WriteAllBytesAsync(filePath, fileContent);

            _logger.LogInformation("File uploaded: {FileName}", fileName);

            // Return URL or path
            return fileName;
        }

        public async Task<byte[]> DownloadAsync(string fileName)
        {
            var filePath = Path.Combine(_basePath, fileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {fileName}");

            return await File.ReadAllBytesAsync(filePath);
        }

        public async Task<bool> DeleteAsync(string fileName)
        {
            var filePath = Path.Combine(_basePath, fileName);

            if (!File.Exists(filePath))
                return false;

            await Task.Run(() => File.Delete(filePath));
            return true;
        }
    }
}
