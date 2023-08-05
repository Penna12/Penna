using Microsoft.AspNetCore.Identity;
using Penna.Entities.Models;
using System.Security.Principal;

namespace Penna.Web.Extensions
{
    public static class IdentityExtensionMethods
    {
        public static string FullName(this IIdentity identity, UserManager<AppUser> userManager)
        {
            if (identity.IsAuthenticated)
            {
                AppUser user = userManager.FindByNameAsync(identity.Name).Result;
                if (user != null)
                {
                    return user.FullName;
                }
            }
            
            return "Noname";
        }

    }
}
