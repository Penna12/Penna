using Penna.Core.Extensions;
using Penna.Entities.Models;
using Penna.Utility.Service;

namespace Penna.Web.Utilities
{
    public static class AppActiveUser
    {
        public static AppUser User
        {
            get
            {
                var activeUser = App.Session.GetObject<AppUser>("activeUser");
                if (activeUser != null)
                {
                    return activeUser;
                }
                return null;
            }
        }
        
        public static string FullName
        {
            get
            {
                var activeUser = App.Session.GetObject<AppUser>("activeUser");
                if (activeUser != null)
                {
                    return $"{activeUser.FullName}";
                }
                return "Aktif user bulunamadı!";
            }
        }

        public static string Email
        {
            get
            {
                var activeUser = App.Session.GetObject<AppUser>("activeUser");
                if (activeUser != null)
                {
                    return $"{activeUser.Email}";
                }
                return "Aktif user bulunamadı!";
            }
        }

        public static bool Status
        {
            get
            {
                var activeUser = App.Session.GetObject<AppUser>("activeUser");
                return (activeUser != null) ? activeUser.Status : false;
            }
        }

        public static string UserID
        {
            get
            {
                var activeUser = App.Session.GetObject<AppUser>("activeUser");
                return (activeUser != null) ? activeUser.Id : "";
            }
        }

        public static int TenantID
        {
            get
            {
                var activeUser = App.Session.GetObject<AppUser>("activeUser");
                return (activeUser != null) ? (int)activeUser.TenantId : 0;
            }

        }

        public static string TenantName
        {
            get
            {
                var activeUser = App.Session.GetObject<AppUser>("activeUser");
                return (activeUser != null) ? activeUser.FullName : "";
            }

        }
    }
}
