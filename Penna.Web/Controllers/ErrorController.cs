using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Penna.Web.Models;

namespace Penna.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch(statusCode)
            {
                case 404: 
                    ViewBag.ErrorMessage = "";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;
                    break;
            }
            return View("NotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ErrorViewModel errorViewModel = new ErrorViewModel() {
                PageTitle = "Hata oluştu",
                Email = "admin@admin.com",
                StatusCode = 500,
                ExceptionPath = exceptionDetails.Path,
                ExceptionMessage = exceptionDetails.Error.Message,
                Stacktrace = exceptionDetails.Error.StackTrace
            };
            
            return View("Error", errorViewModel);
        }
    }
}
