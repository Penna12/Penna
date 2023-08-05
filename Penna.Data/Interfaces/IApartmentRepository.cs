using Penna.Entities.Models;
using System.Threading.Tasks;

namespace Penna.Data.Interfaces
{
    public interface IApartmentRepository : IRepository<Apartment>
    {
        Task<Apartment> GetWithBlockAndCurrentAccountByIdAsync(int apartmentId);
    }
}
