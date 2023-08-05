using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Penna.Business.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Penna.Business.Filters
{
    public class TenantNotFoundFilter : ActionFilterAttribute
    {
        private readonly ITenantService _tenantService;

        public TenantNotFoundFilter(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();

            var tenant = await _tenantService.GetByIdAsync(id);
            if (tenant != null)
            {
                await next();
            }
            else
            {
                context.ModelState.AddModelError("", $"ID'si {id} olan kayıt veritabanında bulunamadı.");
                context.Result = new ViewResult();
            }
        }
    }
}
