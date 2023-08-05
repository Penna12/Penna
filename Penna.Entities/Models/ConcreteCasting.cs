using Penna.Core.Entities;
using System;

namespace Penna.Entities.Models
{
    public class ConcreteCasting : BaseModel, IEntity
    {
        public int Id { get; set; }
        public int ConcreteRequestId { get; set; }
        public string CarPlate { get; set; }
        public DateTime CastingDate { get; set; }
        public int Quantity { get; set; }
        public string WaybilNumber { get; set; }

        public virtual ConcreteRequest ConcreteRequest { get; set; }

    }
}
