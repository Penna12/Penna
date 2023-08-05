using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Penna.Business.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Penna.Business.Filters
{
    public class UnitNotFoundFilter : ActionFilterAttribute
    {
        private readonly IUnitService _unitService;
        public UnitNotFoundFilter(IUnitService unitService)
        {
            _unitService = unitService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();

            var unit = await _unitService.GetByIdAsync(id);
            if (unit != null)
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
