using Penna.Core.Entities;
using Penna.Core.Utilities.Enums;
using Penna.Entities.Models;

namespace Penna.Entities.DTOs
{
    public class BlockViewDto :IDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public BlockTypeEnum TypeId { get; set; }
        public byte FloorCount { get; set; }
        public byte BasementCount { get; set; }
        public int ApartmentCount { get; set; }
        public CostCalculationEnum BlockCostCalculation { get; set; }
        public CostCalculationEnum ApartmentCostCalculation { get; set; }
        public CostCalculationEnum PublicAreaCostCalculation { get; set; }

        public virtual Project Project { get; set; }
    }
}
