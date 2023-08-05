using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Penna.Core.Utilities.Constants;
using Penna.Entities.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Penna.Business.Helper
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser>
    {
        public MyUserClaimsPrincipalFactory(
            UserManager<AppUser> userManager,
            IOptions<IdentityOptions> optionsAccessor)
                : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("ProjectId", SD.ProjectId.ToString()));
            return identity;
        }
    }
}
