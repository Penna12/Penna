using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Core.Entities;
using System;
using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class TenantEditDto : IDto
    {
        public int Id { get; set; }
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


        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public IEnumerable<SelectListItem> CountryList { get; set; }
        public IEnumerable<SelectListItem> CityList { get; set; }
        public IEnumerable<SelectListItem> TownList { get; set; }
    }
}
