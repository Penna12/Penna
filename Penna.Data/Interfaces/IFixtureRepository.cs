using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Data.Interfaces
{
    public interface IFixtureRepository : IRepository<Fixture>
    {
        IEnumerable<SelectListItem> GetFixtureListForDropDown(int projectId, int? selectedId = null);
    }
}
