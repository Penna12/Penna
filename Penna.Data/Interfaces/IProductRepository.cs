using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Data.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetWithProjectByIdAsync(int productId);
        IEnumerable<SelectListItem> GetProductListForDropDown(int projectId, int? selectedId = null);
        IEnumerable<SelectListItem> GetConcreteListForDropDown(int projectId, int? selectedId = null);
    }
}
