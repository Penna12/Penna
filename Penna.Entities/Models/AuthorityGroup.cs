using Penna.Core.Entities;
using System.Collections.Generic;

namespace Penna.Entities.Models
{
    public class AuthorityGroup : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public virtual ICollection<UserPosition> UserPositions { get; set; }
    }
}
