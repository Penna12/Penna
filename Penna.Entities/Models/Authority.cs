using Penna.Core.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penna.Entities.Models
{
    public class Authority : IEntity
    {
        public Authority()
        {
            UserPositionAuthorities = new Collection<UserPositionAuthority>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public virtual ICollection<UserPositionAuthority> UserPositionAuthorities { get; set; }
    }
}
