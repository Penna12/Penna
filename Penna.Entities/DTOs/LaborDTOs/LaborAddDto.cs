using Penna.Core.Entities;

namespace Penna.Entities.DTOs
{
    public class LaborAddDto : IDto
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public int TenantId { get; set; }
        //public IEnumerable<SelectListItem> TenantList { get; set; }
    }
}
