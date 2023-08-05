using Penna.Core.Entities;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class ProjectViewDto : IDto
    {
        public Project Project { get; set; }
        public IEnumerable<Block> Blocks { get; set; }
        public CurrentAccount CurrentAccount { get; set; }
        public IEnumerable<CurrentAccountDetail> CurrentAccountDetail { get; set; }
        public List<FileModel> FileListModel { get; set; }
        public string URL { get; set; }
    }
}
