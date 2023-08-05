using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class PurchaseDto
    {
        public PurchaseDto()
        {
            Purchase = new Purchase();
            PurchaseRequest = new PurchaseRequest();
        }
        public Purchase Purchase { get; set; }
        public PurchaseRequest PurchaseRequest { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
        public IEnumerable<SelectListItem> BlockList { get; set; }
        public IEnumerable<SelectListItem> SupplierList { get; set; }
        public IEnumerable<SelectListItem> BusinessGroupList { get; set; }
    }
}
