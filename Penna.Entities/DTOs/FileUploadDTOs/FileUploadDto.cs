using Penna.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class FileUploadDto : IDto
    {
        public List<IFormFile> file { get; set; }
        public int litId { get; set; }
        public string URL { get; set; }
    }
}
