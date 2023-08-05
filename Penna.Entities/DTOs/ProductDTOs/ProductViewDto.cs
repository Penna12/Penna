using Penna.Core.Entities;
using Penna.Entities.Models;

namespace Penna.Entities.DTOs
{
    public class ProductViewDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public int UnitId { get; set; }
        public int Quantity { get; set; }
        public string Dimensions { get; set; }
        public string Thickness { get; set; }
        public string Species { get; set; }
        public byte BusinessGroupId { get; set; }
        public byte Status { get; set; }
        public int TenantId { get; set; }


        [System.Text.Json.Serialization.JsonIgnore]
        public Unit Unit { get; set; }
    }
}
