using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Core.Entities;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class PurchaseRequestDto : IDto
    {
        public PurchaseRequestDto()
        {
            PurchaseRequest = new PurchaseRequest();
        }
        public PurchaseRequest PurchaseRequest { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
    }
}
