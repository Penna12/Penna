using Penna.Core.Entities;

namespace Penna.Entities.DTOs
{
    public class CountryEditDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string DialCode { get; set; }
        public bool IsActive { get; set; }
    }
}
