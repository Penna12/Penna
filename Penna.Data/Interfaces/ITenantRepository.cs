using Penna.Entities.Models;
using System.Threading.Tasks;

namespace Penna.Data.Interfaces
{
    public interface ITenantRepository:IRepository<Tenant>
    {
        Task<Tenant> GetWithProjectByIdAsync(int tenantId);
    }
}
