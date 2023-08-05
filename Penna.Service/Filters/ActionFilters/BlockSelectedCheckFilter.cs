using Microsoft.AspNetCore.Mvc.Filters;
using Penna.Core.Utilities.Constants;
using System.Threading.Tasks;

namespace Penna.Business.Filters
{
    public class BlockSelectedCheckFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (SD.BlockId == 0)
            {
                context.ModelState.AddModelError("", "Bu sayfa erişmek için bir blok seçilmiş olması gereklidir.");
                context.HttpContext.Response.Redirect("/Project/Index");
            } else
            {
                await next();
            }
        }

    }
}
