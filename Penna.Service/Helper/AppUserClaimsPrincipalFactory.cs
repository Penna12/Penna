using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Penna.Entities.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Penna.Business.Helper
{
    public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, IdentityRole>
    {
        public AppUserClaimsPrincipalFactory(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("UserFullName", user.FullName ?? ""));
            identity.AddClaim(new Claim("TCIdentityNo", user.TCIdentityNo.GetValueOrDefault().ToString() ?? "0"));
            identity.AddClaim(new Claim("UserCityId", user.CityId.GetValueOrDefault().ToString() ?? "0"));
            identity.AddClaim(new Claim("UserCountryId", user.CountryId.GetValueOrDefault().ToString() ?? "0"));
            identity.AddClaim(new Claim("TenantId", user.TenantId.GetValueOrDefault().ToString() ?? "0"));
            identity.AddClaim(new Claim("PictureUrl", user.PictureUrl ?? ""));
            identity.AddClaim(new Claim("CurrentAccountId", user.CurrentAccountId.GetValueOrDefault().ToString() ?? "0"));
            //identity.AddClaim(new Claim("ProjectId", "0"));
            //identity.AddClaim(new Claim("BlockId", "0"));
            return identity;
        }
    }
}
