using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Penna.Business.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Penna.Business.Filters
{
    public class LaborNotFoundFilter : ActionFilterAttribute
    {
        private readonly ILaborService _laborService;

        public LaborNotFoundFilter(ILaborService laborService)
        {
            _laborService = laborService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();

            var labor = await _laborService.GetByIdAsync(id);
            if (labor != null)
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
