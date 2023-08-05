using Penna.Core.Entities;

namespace Penna.Entities.Models
{
    public class Labor : BaseModel, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public int TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}
