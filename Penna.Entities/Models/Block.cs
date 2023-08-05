using Penna.Core.Entities;
using Penna.Core.Utilities.Enums;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penna.Entities.Models
{
    public class Block : BaseModel, IEntity
    {
        public Block()
        {
            Apartments = new Collection<Apartment>();
            ProjectFiles = new Collection<ProjectFile>();
            BlockTeams = new Collection<BlockTeam>();
            BlockSubcontractors = new Collection<BlockSubcontractor>();
            Purchases = new Collection<Purchase>();
            ConcreteRequests = new Collection<ConcreteRequest>();
        }
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
        public virtual ICollection<Apartment> Apartments { get; set; }
        public virtual ICollection<ProjectFile> ProjectFiles { get; set; }
        public virtual ICollection<BlockTeam> BlockTeams { get; set; }
        public virtual ICollection<BlockSubcontractor> BlockSubcontractors { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<ConcreteRequest> ConcreteRequests { get; set; }
    }
}
