using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface IFixtureService : IService<Fixture>
    {
        Task<Fixture> GetWithProjectByIdAsync(int fixtureId);
        IEnumerable<SelectListItem> GetFixtureListForDropDown(int projectId, int? selectedId = null);
    }
}
