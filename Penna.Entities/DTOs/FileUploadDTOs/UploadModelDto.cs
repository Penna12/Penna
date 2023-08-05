using Microsoft.AspNetCore.Http;
using Penna.Core.Entities;

namespace Penna.Entities.DTOs
{
    public class UploadModelDto : IDto
    {
        public string URL { get; set; }
        public string RealUrl { get; set; }
        public string Message { get; set; }
        public IFormFile Mimari { get; set; }
        public IFormFile Statik { get; set; }
        public IFormFile Mekanik { get; set; }
        public IFormFile Elektrik { get; set; }
        public IFormFile Peyzaj { get; set; }
    }
}
