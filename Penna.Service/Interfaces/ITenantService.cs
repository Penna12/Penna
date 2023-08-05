using Penna.Entities.Models;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface ITenantService : IService<Tenant>
    {
        Task<Tenant> GetWithProjectByTenantIdAsync(int TenantId);
        Task<Tenant> GetByIdWithCityAndCountryAsync(int id);
    }
}
