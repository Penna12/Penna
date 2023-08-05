using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Core.Entities;
using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class CityAddDto : IDto
    {
        public string Name { get; set; }
        public int CountryId { get; set; }

        public IEnumerable<SelectListItem> CountryList { get; set; }
    }
}
