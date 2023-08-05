using Penna.Core.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penna.Entities.Models
{
    public class Tenant : BaseModel, IEntity
    {
        public Tenant()
        {
            Projects = new Collection<Project>();
            Labors = new Collection<Labor>();
            CurrentAccounts = new Collection<CurrentAccount>();
            Authorities = new Collection<Authority>();
            AuthorityGroups = new Collection<AuthorityGroup>();
            UserPositions = new Collection<UserPosition>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public string CountryDialCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string TaxId { get; set; }
        public string TaxOffice { get; set; }


        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }

        public virtual Country Country { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Labor> Labors { get; set; }
        public virtual ICollection<CurrentAccount> CurrentAccounts { get; set; }
        public virtual ICollection<Authority> Authorities { get; set; }
        public virtual ICollection<AuthorityGroup> AuthorityGroups { get; set; }
        public virtual ICollection<UserPosition> UserPositions { get; set; }
    }
}
