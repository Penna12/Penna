using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Core.Entities;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class BlockDto : IDto
    {
        public BlockDto()
        {
            Block = new Block();
            Apartments = new List<Apartment>();
        }

        public Block Block { get; set; }
        public IEnumerable<int> Floors { get; set; }
        public List<Apartment> Apartments { get; set; }
        public IEnumerable<SelectListItem> UnitList { get; set; }
        public IEnumerable<SelectListItem> BlockTypeEnumList { get; set; }
        public IEnumerable<SelectListItem> CostCalculationEnumList { get; set; }
    }
}
