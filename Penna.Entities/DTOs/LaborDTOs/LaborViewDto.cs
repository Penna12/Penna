using Penna.Core.Entities;
using Penna.Entities.Models;

namespace Penna.Entities.DTOs
{
    public class LaborViewDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }
}
