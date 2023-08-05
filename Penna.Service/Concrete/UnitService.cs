using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using System.Collections.Generic;
using Penna.Data.Interfaces;

namespace Penna.Business.Concrete
{
    public class UnitService : BaseService<Unit>, IUnitService
    {
        public UnitService(IUnitOfWork unitOfWork, IRepository<Unit> repository) : base(unitOfWork, repository)
        {

        }

        public IEnumerable<SelectListItem> GetUnitListForDropDown(int? selectedId = null)
        {
            return _unitOfWork.Unit.GetUnitListForDropDown(selectedId);
        }
    }
}
