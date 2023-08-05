using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface IFixtureEmbezzledService : IService<FixtureEmbezzled>
    {
        Task<FixtureEmbezzled> GetWithFixtureByIdAsync(int id);
        Task<List<FixtureEmbezzled>> GetAllByFixtureIdAsync(int fixtureId);
        Task<List<FixtureEmbezzled>> GetAllByUserIdAsync(string userId);
    }
}
