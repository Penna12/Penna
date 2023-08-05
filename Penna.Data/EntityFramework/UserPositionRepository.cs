﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Data.EntityFramework.Contexts;
using Penna.Data.Interfaces;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Penna.Data.EntityFramework
{
    public class UserPositionRepository : Repository<UserPosition>, IUserPositionRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public UserPositionRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<SelectListItem> GetUserPositionListForDropDown(int tenantId, int? selectedId = null)
        {
            return appDbContext.UserPositions.Where(x => x.TenantId == tenantId).Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = selectedId.HasValue ? (int)selectedId == x.Id : false
            });
        }
    }
}
