using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Core.Entities;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class ProductDto : IDto
    {
        public ProductDto()
        {
            Product = new Product();
        }
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> BusinessGroupList { get; set; }
        public IEnumerable<SelectListItem> StatusEnumList { get; set; }
        public IEnumerable<SelectListItem> UnitList { get; set; }
    }
}
