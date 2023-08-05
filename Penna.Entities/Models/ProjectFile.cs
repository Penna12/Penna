using Penna.Core.Entities;
using Penna.Core.Utilities.Enums;

namespace Penna.Entities.Models
{
    public class ProjectFile : BaseModel, IEntity
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public FileTypeEnum FileTypeId { get; set; }
        public string Message { get; set; }
        public int ProjectId { get; set; }
        public int BlockId { get; set; }
        public int ApartmentId { get; set; }

        public virtual Project Project { get; set; }
        public virtual Block Block { get; set; }
        public virtual Apartment Apartment { get; set; }
    }
}
