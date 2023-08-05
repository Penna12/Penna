using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Business.Interfaces;
using Penna.Data.Interfaces;
using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Concrete
{
    public class TownService : BaseService<Town>, ITownService
    {
        public TownService(IUnitOfWork unitOfWork, IRepository<Town> repository) : base(unitOfWork, repository)
        {
        }

        public IEnumerable<SelectListItem> GetTownListForDropDown(int cityId, int? selectedId = null)
        {
            return _unitOfWork.Town.GetTownListForDropDown(cityId, selectedId);
        }

        public Task<Town> GetWithCityByIdAsync(int townId)
        {
            return _unitOfWork.Town.GetWithCityByIdAsync(townId);
        }
    }
}
