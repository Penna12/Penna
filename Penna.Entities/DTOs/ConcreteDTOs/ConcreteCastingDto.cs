using Penna.Core.Entities;
using Penna.Entities.Models;

namespace Penna.Entities.DTOs
{
    public class ConcreteCastingDto : IDto
    {
        public ConcreteCasting ConcreteCasting { get; set; }
        public ConcreteRequest ConcreteRequest { get; set; }
        public int TotalCasting { get; set; } = 0;
    }
}
