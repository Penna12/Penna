using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Core.Entities;
using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class CountrySelectListDto : IDto
    {
        public IEnumerable<SelectListItem> CountryList { get; set; }
    }
}
