using Penna.Entities.DTOs;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface IApartmentService : IService<Apartment>
    {
        Task<Apartment> GetWithBlockAndCurrentAccountByIdAsync(int apartmentId);
        Task<IEnumerable<int>> GetBlockFloorsAsync(int blockId);
    }
}
