using Penna.Core.Entities;
using Penna.Core.Utilities.Enums;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penna.Entities.Models
{
    public class CurrentAccount : BaseModel, IEntity
    {
        public CurrentAccount()
        {
            Apartments = new Collection<Apartment>();
            BlockSubcontractors = new Collection<BlockSubcontractor>();
            CurrentAccountDetails = new Collection<CurrentAccountDetail>();
            Projects = new Collection<Project>();
            Purchases = new Collection<Purchase>();
            PurchaseTenders = new Collection<PurchaseTender>();
            WorkPlans = new Collection<WorkPlan>();
        }
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string AuthorizedPerson { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int TownId { get; set; }
       public string TaxId { get; set; }
        public string TaxOffice { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string BankName { get; set; }
        public string IBAN { get; set; }
        public BusinessGroupEnum? BusinessGroupId { get; set; }
        public CurrentAccountTypeEnum AccountTypeId { get; set; } // Taşeron, Üretici, Malik, Proje Sahibi, vs.
        public CompanyStatusEnum CompanyStatusId { get; set; }
        public int TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual Country Country { get; set; }
        public virtual City City { get; set; }
        public virtual Town Town { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }
        public virtual ICollection<BlockSubcontractor> BlockSubcontractors { get; set; }
        public virtual ICollection<CurrentAccountDetail> CurrentAccountDetails { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<PurchaseTender> PurchaseTenders { get; set; }
        public virtual ICollection<WorkPlan> WorkPlans { get; set; }
    }
}
