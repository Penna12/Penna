using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace Penna.Web.ClaimRequirements
{
    public class ProjectSelectedRequirement : IAuthorizationRequirement
    {
    }

    public class ProjectSelectedHandler : AuthorizationHandler<ProjectSelectedRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProjectSelectedRequirement requirement)
        {
            #region örnek kullanımlar
            // Örnek Kullanım 1
            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //{
            //    context.Fail();
            //}
            //else
            //{
            //    var claim = user.FindFirst("ProjectId");
            //    var projectId = claim == null ? 0 : Convert.ToInt32(claim.Value);
            //    if (projectId == 0)
            //    {
            //        context.Fail();
            //    }
            //    else
            //    {
            //        context.Succeed(requirement);
            //    }
            //}


            // Örnek Kullanım 2
            //if (SD.ProjectName == "" || SD.ProjectId == 0)
            //{
            //    context.Fail();
            //}

            // Örnek Kullanım 3

            //if (context.User.HasClaim(c => c.Type == "IsDemo") && DateTime.Now < ExpireDate(context.User, context.Resource))
            //{
            //    context.Succeed(requirement);
            //}
            #endregion

            if (!context.User.HasClaim(c => c.Type == "ProjectId"))
            {
                context.Fail();
            }
            else
            {
                var claim = context.User.FindFirst("ProjectId");
                var projectId = Convert.ToInt32(claim.Value);
                if (projectId > 0)
                {
                    context.Succeed(requirement); 
                }
                else
                {
                    context.Fail();
                }
            }

            return Task.CompletedTask;
        }
    }
}
