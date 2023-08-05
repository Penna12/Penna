using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Penna.Data.Interfaces;
using Penna.Core.Extensions;
using Penna.Core.Utilities.Enums;
using System.Linq;
using System;

namespace Penna.Business.Concrete
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, IRepository<Product> repository) : base(unitOfWork, repository)
        {

        }

        public IEnumerable<SelectListItem> GetProductListForDropDown(int projectId, int? selectedId = null)
        {
            return _unitOfWork.Product.GetProductListForDropDown(projectId, selectedId);
        }
        public IEnumerable<SelectListItem> GetConcreteListForDropDown(int projectId, int? selectedId = null)
        {
            return _unitOfWork.Product.GetConcreteListForDropDown(projectId, selectedId);
        }
        public async Task<Product> GetWithProjectByIdAsync(int productId)
        {
            return await _unitOfWork.Product.SingleOrDefaultAsync(p => p.Id == productId, includeProperties:"Project"); //GetWithProjectByIdAsync(productId);
        }

        public IEnumerable<SelectListItem> GetUnitListForDropDown(int? selectedId = null)
        {
            return _unitOfWork.Unit.GetUnitListForDropDown(selectedId);
        }

        public IEnumerable<SelectListItem> GetBusinessGroupListForDropDown(string selectedId = null)
        {
            return EnumExtensions.GetAttributeList(typeof(BusinessGroupEnum)).Select(x => new SelectListItem { Value = Convert.ChangeType(x.Value, x.Value.GetTypeCode()).ToString(), Text = x.Text, Selected = (selectedId != null && x.Value == selectedId) });
        }

        public IEnumerable<SelectListItem> GetStatusListForDropDown(string selectedId = null)
        {
            return EnumExtensions.GetAttributeList(typeof(StatusEnum)).Select(x => new SelectListItem { Value = Convert.ChangeType(x.Value, x.Value.GetTypeCode()).ToString(), Text = x.Text, Selected = (selectedId != null && x.Value == selectedId) });
        }
    }
}
