using Penna.Core.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penna.Entities.Models
{
    public class UserPosition : IEntity
    {
        public UserPosition()
        {
            UserPositionAuthorities = new Collection<UserPositionAuthority>();
            BlockTeams = new Collection<BlockTeam>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public int AuthorityGroupId { get; set; }
        public virtual AuthorityGroup AuthorityGroup { get; set; }

        public virtual ICollection<UserPositionAuthority> UserPositionAuthorities { get; set; }
        public virtual ICollection<BlockTeam> BlockTeams { get; set; }
    }
}
