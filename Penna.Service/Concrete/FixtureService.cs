using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Business.Interfaces;
using Penna.Data.Interfaces;
using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Concrete
{
    public class FixtureService : BaseService<Fixture>, IFixtureService
    {
        public FixtureService(IUnitOfWork unitOfWork, IRepository<Fixture> repository) : base(unitOfWork, repository)
        {
        }

        public IEnumerable<SelectListItem> GetFixtureListForDropDown(int projectId, int? selectedId = null)
        {
            return _unitOfWork.Fixture.GetFixtureListForDropDown(projectId, selectedId);
        }

        public async Task<Fixture> GetWithProjectByIdAsync(int fixtureId)
        {
            return await _unitOfWork.Fixture.SingleOrDefaultAsync(f => f.Id == fixtureId, includeProperties:"Project");
        }
    }
}
