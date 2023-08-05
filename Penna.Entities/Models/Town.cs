using Penna.Core.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penna.Entities.Models
{
    public class Town : IEntity
    {
        public Town()
        {
            Projects = new Collection<Project>();
            CurrentAccounts = new Collection<CurrentAccount>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<CurrentAccount> CurrentAccounts { get; set; }
    }
}
