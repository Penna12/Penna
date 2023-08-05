using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using Penna.Business.Interfaces;
using System.Threading.Tasks;
using Penna.Data.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Penna.Entities.DTOs;

namespace Penna.Business.Concrete
{
    public class ApartmentService : BaseService<Apartment>, IApartmentService
    {
        public ApartmentService(IUnitOfWork unitOfWork, IRepository<Apartment> repository) : base(unitOfWork, repository)
        {

        }

        public async Task<Apartment> GetWithBlockAndCurrentAccountByIdAsync(int apartmentId)
        {
            return await _unitOfWork.Apartment.GetWithBlockAndCurrentAccountByIdAsync(apartmentId);
        }

        public async Task<IEnumerable<int>> GetBlockFloorsAsync(int blockId)
        {
            var liste = await _unitOfWork.Apartment.Where(a => a.BlockId == blockId);
            return liste.OrderBy(x => x.Floor).Select(x => x.Floor).Distinct();
        }
    }
}
