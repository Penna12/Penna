using Penna.DTO.Interfaces;

namespace Penna.DTO.TenantDTOs
{
    public class TenantAddDto : IDto
    {
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string TaxId { get; set; }
        public string TaxOffice { get; set; }


        //public bool IsActive { get; set; }
        //public bool IsLocked { get; set; }
        //public int CreatedBy { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public int? UpdatedBy { get; set; }
        //public DateTime? UpdatedDate { get; set; }
    }
}
