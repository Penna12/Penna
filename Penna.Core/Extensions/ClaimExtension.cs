using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Penna.Core.Extensions
{
    public static class ClaimExtension
    {
        public static void AddOrUpdateClaim(this IPrincipal currentPrincipal, string key, string value)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
                return;

            // check for existing claim and remove it
            var existingClaim = identity.FindFirst(key);
            if (existingClaim != null)
                identity.RemoveClaim(existingClaim);

            // add new claim
            identity.AddClaim(new Claim(key, value));
        }

        public static string GetClaimValue(this IPrincipal currentPrincipal, string key)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
                return null;

            var claim = identity.Claims.FirstOrDefault(c => c.Type == key);
            return claim?.Value;
        }

        public static void RemoveClaimValue(this IPrincipal currentPrincipal, string key)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            if (identity != null)
            {
                //var claim = identity.Claims.FirstOrDefault(c => c.Type == key);
                // check for existing claim and remove it
                var existingClaim = identity.FindFirst(key);
                if (existingClaim != null)
                    identity.RemoveClaim(existingClaim);
            }
        }

        public static void SetProjectClaim(this IPrincipal currentPrincipal, string projectId, string projectName, string tenantName)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
                return;

            // check for existing ("ProjectId") claim and remove it
            var existingClaim = identity.FindFirst("ProjectId");
            if (existingClaim != null)
                identity.RemoveClaim(existingClaim);
            // add new ("ProjectId")claim
            identity.AddClaim(new Claim("ProjectId", projectId));

            // check for existing ("ProjectName") claim and remove it
            existingClaim = identity.FindFirst("ProjectName");
            if (existingClaim != null)
                identity.RemoveClaim(existingClaim);
            // add new ("ProjectName")claim
            identity.AddClaim(new Claim("ProjectName", projectId));

            // check for existing ("TenantName") claim and remove it
            existingClaim = identity.FindFirst("TenantName");
            if (existingClaim != null)
                identity.RemoveClaim(existingClaim);
            // add new ("TenantName")claim
            identity.AddClaim(new Claim("TenantName", projectId));

        }

        public static bool ProjectSelected(this IPrincipal currentPrincipal)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
                return false;

            var claim = identity.Claims.FirstOrDefault(c => c.Type == "ProjectId");
            var projectId = claim == null ? 0 : Convert.ToInt32(claim.Value);
            return projectId > 0;
        }

    }
}
