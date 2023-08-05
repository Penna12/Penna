using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class ProductInOutDto
    {
        public ProductInOutDto()
        {
            ProductInOut = new ProductInOut();
        }
        public bool Input_fl { get; set; }
        public ProductInOut ProductInOut { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }

        //public IEnumerable<SelectListItem> BusinessGroupList { get; set; }
        //public IEnumerable<SelectListItem> StatusEnumList { get; set; }
        //public IEnumerable<SelectListItem> UnitList { get; set; }
    }
}
