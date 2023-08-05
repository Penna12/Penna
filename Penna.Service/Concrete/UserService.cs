using Microsoft.AspNetCore.Http;
using Penna.Business.Interfaces;
using System.Security.Claims;

namespace Penna.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public bool IsAuthenticated()
        {
            return _httpContext.HttpContext.User.Identity.IsAuthenticated;
        }
        
        public string GetUserId()
        {
            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string GetUserName()
        {
            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.Name);
        }

        public string GetUserEmail()
        {
            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.Email);
        }

        public string GetUserRole()
        {
            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.Role);
        }

        public string GetUserFullName()
        {
            return _httpContext.HttpContext.User?.FindFirstValue("UserFullName");
        }

        public string GetUserTcIdentityNo()
        {
            return _httpContext.HttpContext.User?.FindFirstValue("TCIdentityNo");
        }

        public int GetUserTenantId()
        {
            int.TryParse(_httpContext.HttpContext.User?.FindFirstValue("TenantId"), out int result);
            return result;
        }

        public int GetUserCityId()
        {
            int.TryParse(_httpContext.HttpContext.User?.FindFirstValue("UserCityId"), out int result);
            return result;
        }

        public int GetUserCountryId()
        {
            int.TryParse(_httpContext.HttpContext.User?.FindFirstValue("UserCountryId"), out int result);
            return result;
        }


    }
}
