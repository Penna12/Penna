using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Core.Entities;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class ProjectDto : IDto
    {
        public ProjectDto()
        {
            Project = new Project();
        }
        public Project Project { get; set; }
        public IEnumerable<SelectListItem> CurrentAccountList { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
        public IEnumerable<SelectListItem> CityList { get; set; }
        public IEnumerable<SelectListItem> TownList { get; set; }
    }
}
