using Penna.Core.Entities;

namespace Penna.Entities.Models
{
    public class BlockTeam : BaseModel, IEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
        
        public string Phone { get; set; }

        public int UserPositionId { get; set; }
        public virtual UserPosition UserPosition { get; set; }
        
        public int BlockId { get; set; }
        public virtual Block Block { get; set; }
    }
}
