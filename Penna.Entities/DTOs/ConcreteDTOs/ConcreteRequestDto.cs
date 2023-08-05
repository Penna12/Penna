using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Core.Entities;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class ConcreteRequestDto : IDto
    {
        public ConcreteRequestDto()
        {
            ConcreteRequest = new ConcreteRequest();
        }
        public ConcreteRequest ConcreteRequest { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
        public IEnumerable<SelectListItem> BlockList { get; set; }
    }
}
