using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Penna.Entities.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            BlockTeams = new Collection<BlockTeam>();
            FixtureEmbezzleds = new Collection<FixtureEmbezzled>();
        }

        public string FullName { get; set; }
        public string Address { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public string CountryDialCode { get; set; }
        public long? TCIdentityNo { get; set; }
        public bool Status { get; set; }
        public int? TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public string PictureUrl { get; set; }
        public string PictureRealName { get; set; }
        public string PictureContentType { get; set; }
        public int? CurrentAccountId { get; set; }
        public CurrentAccount CurrentAccount { get; set; }

        public virtual ICollection<BlockTeam> BlockTeams { get; set; }
        public virtual ICollection<FixtureEmbezzled> FixtureEmbezzleds { get; set; }
    }
}
