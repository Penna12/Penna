using Penna.Core.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penna.Entities.Models
{
    public class BlockSubcontractor : BaseModel, IEntity
    {
        public BlockSubcontractor()
        {
            BlockSubcontractorBusinessGroups = new Collection<BlockSubcontractorBusinessGroup>();
        }

        public int Id { get; set; }
        public int CurrentAccountId { get; set; }
        public string CompanyRepresentative { get; set; }
        public string Phone { get; set; }
        public int BlockId { get; set; }

        public virtual Block Block { get; set; }
        public virtual CurrentAccount CurrentAccount { get; set; }
        public virtual ICollection<BlockSubcontractorBusinessGroup> BlockSubcontractorBusinessGroups { get; set; }
    }
}
