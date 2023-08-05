using Microsoft.AspNetCore.Http;
using Penna.Core.Extensions;
using System;   

namespace Penna.Utility.Service
{

    public static class SessionManager
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
        public static HttpContext HttpContext
        {
            get
            {
                IHttpContextAccessor httpContextAccessor = services.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
                return httpContextAccessor?.HttpContext;
            }
        }
        public static ISession Session
        {
            get { return HttpContext.Session; }
        }

        public static void SaveObject(string key, string value)
        {
            Session.SetObject(key, value);
        }

        public static T GetObject<T>(string key) where T : class
        {
            return Session.GetObject<T>(key);
        }
    }
}
