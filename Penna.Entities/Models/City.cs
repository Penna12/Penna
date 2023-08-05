using Penna.Core.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penna.Entities.Models
{
    public class City : IEntity
    {
        public City()
        {
            Towns = new Collection<Town>();
            Tenants = new Collection<Tenant>();
            Projects = new Collection<Project>();
            CurrentAccounts = new Collection<CurrentAccount>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }


        public virtual Country Country { get; set; }
        public virtual ICollection<Town> Towns { get; set; }
        public virtual ICollection<Tenant> Tenants { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<CurrentAccount> CurrentAccounts { get; set; }
    }
}
