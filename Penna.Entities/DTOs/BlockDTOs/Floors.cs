using Penna.Core.Entities;

namespace Penna.Entities.DTOs
{
    public class Floors :IDto
    {
        public int BlockId { get; set; }
        public int FlourNo { get; set; }
        public string FlourName { get; set; }
    }
}
