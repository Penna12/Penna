using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Core.Entities;
using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class CountryAndCitySelectListDto : IDto
    {
        public IEnumerable<SelectListItem> CountryList { get; set; }
        public IEnumerable<SelectListItem> CityList { get; set; }
    }
}
