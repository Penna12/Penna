using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Penna.Web.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var routeValues = context.RouteData.Values;

            var c = routeValues["controller"];
            var a = routeValues["action"];

            if (!User.Identity.IsAuthenticated)
            {
                routeValues["controller"] = "Account";
                routeValues["action"] = "Login";
                Response.Redirect("~/Account/Login");
            }
        }
    }
}
