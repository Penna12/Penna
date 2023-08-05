using Microsoft.AspNetCore.Mvc.Filters;
using Penna.Core.Utilities.Constants;
using System.Threading.Tasks;

namespace Penna.Business.Filters
{
    public class ProjectSelectedCheckFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (SD.ProjectId == 0 || SD.TenantId == 0)
            {
                context.ModelState.AddModelError("", "Bu sayfa erişmek için bir proje seçilmiş olması gereklidir.");
                context.HttpContext.Response.Redirect("/Project/Index");
            } else
            {
                await next();
            }
        }
    }
}
