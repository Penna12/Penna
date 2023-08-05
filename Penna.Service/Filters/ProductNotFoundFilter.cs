using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Penna.Business.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Penna.Business.Filters
{
    public class ProductNotFoundFilter : ActionFilterAttribute
    {
        private readonly IProductService _productService;

        public ProductNotFoundFilter(IProductService productService)
        {
            _productService = productService;
        }
        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();

            var product = await _productService.GetByIdAsync(id);
            if (product != null)
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
