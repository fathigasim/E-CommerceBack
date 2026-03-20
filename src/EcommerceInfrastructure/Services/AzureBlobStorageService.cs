using MediaRTutorialApplication.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceInfrastructure.Services
{
    // Infrastructure/Services/AzureBlobStorageService.cs
    public class AzureBlobStorageService 
        //: IFileStorageService
    {
        //private readonly BlobContainerClient _containerClient;

        //public AzureBlobStorageService(IConfiguration config)
        //{
        //    var connectionString = config["AzureStorage:ConnectionString"];
        //    var containerName = config["AzureStorage:ContainerName"] ?? "product-images";
        //    _containerClient = new BlobContainerClient(connectionString, containerName);
        //    _containerClient.CreateIfNotExists();
        //}

        //public async Task<string> UploadFileAsync(
        //    Stream fileStream, string fileName, string contentType,
        //    CancellationToken cancellationToken = default)
        //{
        //    var blobName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        //    var blobClient = _containerClient.GetBlobClient(blobName);

        //    await blobClient.UploadAsync(fileStream, new BlobHttpHeaders
        //    {
        //        ContentType = contentType
        //    }, cancellationToken: cancellationToken);

        //    return blobClient.Uri.ToString();
        //}

        //public async Task DeleteFileAsync(string fileUrl,
        //    CancellationToken cancellationToken = default)
        //{
        //    var blobName = Path.GetFileName(new Uri(fileUrl).LocalPath);
        //    var blobClient = _containerClient.GetBlobClient(blobName);
        //    await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
        //}
    }
}
