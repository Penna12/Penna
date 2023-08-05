using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Core.Entities;
using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class TenantAddDto : IDto
    {
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public string CountryDialCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string TaxId { get; set; }
        public string TaxOffice { get; set; }


        public IEnumerable<SelectListItem> CountryList { get; set; }
        public IEnumerable<SelectListItem> CityList { get; set; }
        public IEnumerable<SelectListItem> TownList { get; set; }
    }
}
