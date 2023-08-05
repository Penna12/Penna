using Penna.Core.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penna.Entities.Models
{
    public class Fixture : BaseModel, IEntity
    {
        public Fixture()
        {
            FixtureEmbezzleds = new Collection<FixtureEmbezzled>();
        }
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int ProjectId { get; set; }

        public Project Project { get; set; }
        public virtual ICollection<FixtureEmbezzled> FixtureEmbezzleds { get; set; }
    }
}
