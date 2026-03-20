using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRTutorialApplication.Interfaces
{
    // Interface in Application layer
    public interface IFileStorageService
    {
        Task<string> UploadAsync(byte[] fileContent, string fileName, string contentType);
        Task<byte[]> DownloadAsync(string fileName);
        Task<bool> DeleteAsync(string fileName);

        Task<string> UploadFileAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default);

        Task DeleteFileAsync(
            string fileUrl,
            CancellationToken cancellationToken = default);
    }

    
}
