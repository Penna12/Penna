using Penna.Entities.Models;
using System.Threading.Tasks;

namespace Penna.Data.Interfaces
{
    public interface IProjectFileRepository : IRepository<ProjectFile>
    {
        Task<ProjectFile> GetWithProjectBlockApartmentByIdAsync(int projectFileId);
    }
}
