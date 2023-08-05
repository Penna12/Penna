using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Core.Entities;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class BlockSubcontractorDto : IDto
    {
        public Block Block { get; set; }
        public IEnumerable<BlockSubcontractor> BlockSubcontractors { get; set; }
        public IEnumerable<SelectListItem> SubcontractorList { get; set; }
    }
}
