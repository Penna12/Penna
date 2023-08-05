using Penna.Core.Entities;

namespace Penna.Entities.DTOs
{
    public class DeleteFileDto : IDto
    {
        public int litId { get; set; }
        public string folderUrl { get; set; }
        public string fileName { get; set; }
    }
}
