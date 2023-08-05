using Microsoft.AspNetCore.Http;
using System;

namespace Penna.Utility.Service
{
    public static class App
    {
        private static IServiceProvider services = null;

        /// <summary>
        /// Provides static access to the framework's services provider
        /// </summary>
        public static IServiceProvider Services
        {
            get { return services; }
            set
            {
                if (services != null)
                {
                    throw new Exception("Zaten bir değer ayarlanmış, sonra tekrar ayarlanamaz.");
                }
                services = value;
            }
        }

        /// <summary>
        /// Provides static access to the current HttpContext
        /// </summary>
        public static HttpContext HttpContext
        {
            get
            {
                IHttpContextAccessor httpContextAccessor = services.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
                return httpContextAccessor?.HttpContext;
            }
        }

        /// <summary>
        /// Session servis sağlayıcısına statik erişim sağlar
        /// </summary>
        public static ISession Session
        {
            get { return HttpContext.Session; }
        }


    }
}
