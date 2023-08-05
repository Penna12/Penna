using Penna.Core.Entities;
using Penna.Core.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penna.Entities.Models
{
    public class Project : BaseModel, IEntity
    {
        public Project()
        {
            Blocks = new Collection<Block>();
            ProjectFiles = new Collection<ProjectFile>();
            CurrentAccountDetails = new Collection<CurrentAccountDetail>();
            Fixtures = new Collection<Fixture>();
            Products = new Collection<Product>();
            PurchaseRequests = new Collection<PurchaseRequest>();
            Purchases = new Collection<Purchase>();
        }
        public int Id { get; set; } // ProjectId
        public string ProjectName { get; set; }
        public int TenantId { get; set; }
        public int CurrentAccountId { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int TownId { get; set; }
        public int Pafta { get; set; }
        public int Ada { get; set; }
        public int Parsel { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime LicenseDate { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime CommitmentDate { get; set; }
        public int BlockCount { get; set; }
        public int TotalApartmentCount { get; set; }
        public int TotalCommercialCount { get; set; }
        public string BuildingInspection { get; set; }

        public string ArchitecturalWorks { get; set; }
        public string StaticWorks { get; set; }
        public string MechanicalWorks { get; set; }
        public string ElectricalWorks { get; set; }
        public string LandScapeWorks { get; set; }
        
        public ProjectStatusEnum Status { get; set; }
        public bool IsLocked { get; set; }

        public double CommitmentAmount { get; set; } // TaahhutTutari: commitment amount
        public float DownPaymentRate { get; set; } // PesinatOrani: down payment rate
        public byte InstallmentCount { get; set; } // TaksitSayisi: number of installments


        public virtual Tenant Tenant { get; set; }
        public virtual CurrentAccount CurrentAccount { get; set; }
        public virtual Country Country { get; set; }
        public virtual City City { get; set; }
        public virtual Town Town { get; set; }

        public virtual ICollection<Block> Blocks { get; set; }
        public virtual ICollection<ProjectFile> ProjectFiles { get; set; }
        public virtual ICollection<CurrentAccountDetail> CurrentAccountDetails { get; set; }
        public virtual ICollection<Fixture> Fixtures { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<PurchaseRequest> PurchaseRequests { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
