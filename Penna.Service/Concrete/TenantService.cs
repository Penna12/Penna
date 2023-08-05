using Penna.Business.Interfaces;
using Penna.Data.Interfaces;
using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using System.Threading.Tasks;

namespace Penna.Business.Concrete
{
    public class TenantService : BaseService<Tenant>, ITenantService
    {
        public TenantService(IUnitOfWork unitOfWork, IRepository<Tenant> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<Tenant> GetWithProjectByTenantIdAsync(int tenantId)
        {
            return await _unitOfWork.Tenant.GetWithProjectByIdAsync(tenantId);
        }
        
        public async Task<Tenant> GetByIdWithCityAndCountryAsync(int id)
        {
            return await _unitOfWork.Tenant.SingleOrDefaultAsync(x => x.Id == id, includeProperties: "City,Country");
        }
    }
}
