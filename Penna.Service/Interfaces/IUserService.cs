namespace Penna.Business.Interfaces
{
    public interface IUserService
    {
        bool IsAuthenticated();
        string GetUserId();
        string GetUserName();
        string GetUserEmail();
        string GetUserRole();
        string GetUserFullName();
        string GetUserTcIdentityNo();
        int GetUserTenantId();
        int GetUserCityId();
        int GetUserCountryId();
    }
}
