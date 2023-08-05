using Microsoft.AspNetCore.Mvc.Filters;
using Penna.Core.Utilities.Constants;

namespace Penna.Business.Filters
{
    public class ProjectUnselectedFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var c = context.RouteData.Values["controller"];
            var a = context.RouteData.Values["action"];

            SD.ProjectId = 0;
            SD.ProjectName = string.Empty;
            SD.BlockId = 0;
            SD.BlockName = string.Empty;
            SD.CurAccountId = 0;
            SD.CurAccountName = string.Empty;
        }
    }
}
