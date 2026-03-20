
using EcommerceApplication.Common.Settings;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRTutorialApplication.Features.Products.Commands
{
    public class UploadProductImageCommand : IRequest<Result<string>>
    {
        public int ProductId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[]? FileContent { get; set; } // Not IFormFile!

        public IFormFile File { get; set; }
    }
}
