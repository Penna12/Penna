using Penna.Entities.Models;
using System.Threading.Tasks;

namespace Penna.Data.Interfaces
{
    public interface IBlockSubcontractorRepository : IRepository<BlockSubcontractor>
    {
        Task<BlockSubcontractor> GetWithBlockAndCurrentAccountByIdAsync(int blockSubcontractorId);
    }
}
