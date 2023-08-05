using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface IProductService : IService<Product>
    {
        Task<Product> GetWithProjectByIdAsync(int productId);
        IEnumerable<SelectListItem> GetProductListForDropDown(int projectId, int? selectedId = null);
        IEnumerable<SelectListItem> GetConcreteListForDropDown(int projectId, int? selectedId = null);
        IEnumerable<SelectListItem> GetUnitListForDropDown(int? selectedId = null);
        IEnumerable<SelectListItem> GetBusinessGroupListForDropDown(string selectedId = null);
        IEnumerable<SelectListItem> GetStatusListForDropDown(string selectedId = null);
    }
}
