using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Core.Models;
using System.Collections.Generic;

namespace Penna.DTO.TenantDTOs
{
    public class TenantListDto
    {
        public Tenant Tenant { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
        public IEnumerable<SelectListItem> CityList { get; set; }
        public IEnumerable<SelectListItem> TownList { get; set; }
    }
}
