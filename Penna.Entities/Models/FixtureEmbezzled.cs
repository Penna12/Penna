using Penna.Core.Entities;
using System;

namespace Penna.Entities.Models
{
    public class FixtureEmbezzled : BaseModel, IEntity
    {
        public int Id { get; set; }
        public int FixtureId { get; set; }
        public int Quantity { get; set; }
        public string AppUserId { get; set; }
        public string Description { get; set; }
        public DateTime EmbezzledDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public virtual Fixture Fixture { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
