using Penna.Core.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penna.Entities.Models
{
    public class Country : IEntity
    {
        public Country()
        {
            Cities = new Collection<City>();
            Tenants = new Collection<Tenant>();
            Projects = new Collection<Project>();
            CurrentAccounts = new Collection<CurrentAccount>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string DialCode { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Tenant> Tenants { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<CurrentAccount> CurrentAccounts { get; set; }
    }
}
