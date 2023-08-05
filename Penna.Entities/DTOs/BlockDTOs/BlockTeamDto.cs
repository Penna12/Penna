using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Core.Entities;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class BlockTeamDto : IDto
    {
        public Block Block { get; set; }
        public IEnumerable<BlockTeam> BlockTeams{ get; set; }
        public IEnumerable<SelectListItem> UserList { get; set; }
        public IEnumerable<SelectListItem> UserPositionList { get; set; }
    }
}
