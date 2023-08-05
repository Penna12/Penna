using Penna.Entities.Models;
using System;

namespace Penna.Entities.DTOs
{
    public class TenantInfoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public CityAddDto City { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
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
    }
}
