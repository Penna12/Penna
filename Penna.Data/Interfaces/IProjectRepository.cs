using Penna.Entities.Models;
using System.Threading.Tasks;

namespace Penna.Data.Interfaces
{
    public interface IProjectRepository:IRepository<Project>
    {
        Task<Project> GetWithTenantByIdAsync(int projectId);
    }
}
