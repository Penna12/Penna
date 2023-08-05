using Penna.Core.Entities;

namespace Penna.Entities.DTOs
{
    public class DeleteFolderDto : IDto
    {
        public int litId { get; set; }
        public string folderUrl { get; set; }
        public string folderName { get; set; }
    }
}
