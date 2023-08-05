using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface ITownService : IService<Town>
    {
        Task<Town> GetWithCityByIdAsync(int townId);
        IEnumerable<SelectListItem> GetTownListForDropDown(int cityId, int? selectedId = null);
    }
}
