using Penna.Core.Entities;

namespace Penna.Entities.Models
{
    public class UserPositionAuthority : IEntity
    {
        public int UserPositionId { get; set; }
        public virtual UserPosition UserPosition { get; set; }


        public int AuthorityId { get; set; }
        public virtual Authority Authority { get; set; }
    }
}
