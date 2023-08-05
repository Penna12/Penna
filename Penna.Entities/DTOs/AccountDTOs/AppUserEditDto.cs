using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Core.Entities;
using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class AppUserEditDto : IDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public string CountryDialCode { get; set; }
        public long? TCIdentityNo { get; set; }
        public bool Status { get; set; }
        public int? TenantId { get; set; }
        public string PictureUrl { get; set; }
        public string PictureRealName { get; set; }
        public string PictureContentType { get; set; }
        public IFormFile NewImage { get; set; }

        public IEnumerable<SelectListItem> CityList { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
    }
}
