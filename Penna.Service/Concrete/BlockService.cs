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
    public class BlockService : BaseService<Block>, IBlockService
    {
        public BlockService(IUnitOfWork unitOfWork, IRepository<Block> repository) : base(unitOfWork, repository)
        {

        }

        public async Task<Block> GetWithProjectByIdAsync(int blockId)
        {
            return await _unitOfWork.Block.GetWithProjectByIdAsync(blockId);
        }

        public IEnumerable<SelectListItem> GetBlockListForDropDown(int projectId, int? selectedId = null)
        {
            return _unitOfWork.Block.GetBlockListForDropDown(projectId, selectedId);
        }

        public IEnumerable<SelectListItem> GetBlockTypeListForDropDown(string selectedId = null)
        {
            return EnumExtensions.GetAttributeList(typeof(BlockTypeEnum)).Select(x => new SelectListItem { Value = Convert.ChangeType(x.Value, x.Value.GetTypeCode()).ToString(), Text = x.Text, Selected = (selectedId != null && x.Value == selectedId) });
        }

        public IEnumerable<SelectListItem> GetCostCalculationListForDropDown(string selectedId = null)
        {
            return EnumExtensions.GetAttributeList(typeof(CostCalculationEnum)).Select(x => new SelectListItem { Value = Convert.ChangeType(x.Value, x.Value.GetTypeCode()).ToString(), Text = x.Text, Selected = (selectedId != null && x.Value == selectedId) });
        }

        public IEnumerable<SelectListItem> GetUserListForDropDown(int tenantId, string selectedId = null)
        {
            return _unitOfWork.AppUser.GetUserListForDropDown(tenantId, selectedId);
        }

        public IEnumerable<SelectListItem> GetSubcontractorListForDropDown(int tenantId, int? selectedId = null)
        {
            return _unitOfWork.CurrentAccount.GetCurrentAccountListForDropDown(tenantId, CurrentAccountTypeEnum.Subcontractor, selectedId);
        }

        public IEnumerable<SelectListItem> GetUserPositionListForDropDown(int tenantId)
        {
            return _unitOfWork.UserPosition.GetUserPositionListForDropDown(tenantId);
        }

        public async Task<AppUser> GetAppUserByIdAsync(string userId)
        {
            return await _unitOfWork.AppUser.SingleOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<CurrentAccount> GetCurrentAccountByIdAsync(int accountId)
        {
            return await _unitOfWork.CurrentAccount.GetByIdAsync(accountId);
        }

        public async Task<IEnumerable<BlockTeam>> GetBlockTeamListAsync(int blockId)
        {
            return await _unitOfWork.BlockTeam.GetBlockTeamsListAsync(blockId);
        }
    }
}
