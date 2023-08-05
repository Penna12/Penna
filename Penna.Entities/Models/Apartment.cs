using Penna.Core.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penna.Entities.Models
{
    public class Apartment : BaseModel, IEntity
    {
        public Apartment()
        {
            ProjectFiles = new Collection<ProjectFile>();
        }
        public int Id { get; set; }
        public int Floor { get; set; }
        public string SectionName { get; set; }
        public float Gross { get; set; }
        public float Net { get; set; }
        public float Gabari { get; set; }
        public int? CurrentAccountId { get; set; }
        public int BlockId { get; set; }

        public virtual Block Block { get; set; }
        public virtual CurrentAccount CurrentAccount { get; set; }
        public virtual ICollection<ProjectFile> ProjectFiles { get; set; }
    }
}
