using Penna.Core.Entities;
using Penna.Core.Utilities.Enums;

namespace Penna.Entities.Models
{
    public class BlockSubcontractorBusinessGroup : IEntity
    {
        public int Id { get; set; }
        public int BlockSubcontractorId { get; set; }
        public BusinessGroupEnum BusinessGroupId { get; set; }

        public virtual BlockSubcontractor BlockSubcontractor { get; set; }
    }
}
