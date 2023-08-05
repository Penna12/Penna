using Microsoft.AspNetCore.Http;

namespace Penna.Core.Utilities.Constants
{
    public static class SD
    {
        public static HttpContext HttpContext { get; set; }

        public static string ROLE_SuperAdmin = "SuperAdmin";
        public static string ROLE_Admin = "Admin";
        public static string ROLE_ProjectManager = "ProjectManager";
        public static string ROLE_Taseron = "Taseron";
        public static string ROLE_Supplier = "Tedarikçi";
        public static string ROLE_Customer = "Customer";

        public static string RootPath = string.Empty;
        public static string ProfileImagePath = "/assets/img/users/";

        public static string usp_GetUserInfoByTenantAndRole = "GetUserInfoByTenantAndRole";

        public static int ProjectId = 0;
        public static string ProjectName = string.Empty;
        public static int TenantId = 0;
        public static string TenantName = string.Empty;
        public static int CurAccountId = 0;
        public static string CurAccountName = string.Empty;

        public static int BlockId = 0;
        public static string BlockName = string.Empty;

        public static string SESSION_KEY_USER = "AppUser";
        public static string SESSION_KEY_TENANT = "Tenant";
        public static string SESSION_KEY_TENANT_ID = "TenantId";
        public static string SESSION_KEY_TENANT_NAME = "TenantName";
        //public static string SESSION_KEY_PROJECT = "Project";
        public static string SESSION_KEY_PROJECT_ID = "ProjectId";
        public static string SESSION_KEY_PROJECT_NAME = "ProjectName";
        //public static string SESSION_KEY_BLOCK = "Block";
        public static string SESSION_KEY_BLOCK_ID = "BlockId";
        public static string SESSION_KEY_BLOCK_NAME = "BlockName";
        //public static string SESSION_KEY_CURRENT_ACCOUNT = "CurAccount";
    }
}
