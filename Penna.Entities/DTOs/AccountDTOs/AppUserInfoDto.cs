using System;

namespace Penna.Entities.DTOs
{
    public class AppUserInfoDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime LockoutEnd { get; set; }
        public int AccessFailedCount { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryDialCode { get; set; }
        public long? TCIdentityNo { get; set; }
        public bool Status { get; set; }
        public string PictureUrl { get; set; }
        public string PictureRealName { get; set; }
        public string PictureContentType { get; set; }
        public int? TenantId { get; set; }
        public string TenantTitle { get; set; }
        public string TenantName { get; set; }
        public string RoleName { get; set; }
    }
}
