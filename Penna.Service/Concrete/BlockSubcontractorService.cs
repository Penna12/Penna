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
    public class BlockSubcontractorService : BaseService<BlockSubcontractor>, IBlockSubcontractorService
    {
        public BlockSubcontractorService(IUnitOfWork unitOfWork, IRepository<BlockSubcontractor> repository) : base(unitOfWork, repository)
        {

        }

        public async Task<BlockSubcontractor> GetWithBlockAndCurrentAccountByIdAsync(int blockSubcontractorId)
        {
            return await _unitOfWork.BlockSubcontractor.GetWithBlockAndCurrentAccountByIdAsync(blockSubcontractorId);
        }

        public IEnumerable<SelectListItem> GetBusinessGroupListForDropDown(string selectedId = null)
        {
            return EnumExtensions.GetAttributeList(typeof(BusinessGroupEnum)).Select(x => new SelectListItem { Value = Convert.ChangeType(x.Value, x.Value.GetTypeCode()).ToString(), Text = x.Text, Selected = (selectedId != null && x.Value == selectedId) });
        }
    }
}
