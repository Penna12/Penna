using Penna.Business.Interfaces;
using Penna.Data.Interfaces;
using Penna.Data.UnitOfWork;
using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Concrete
{
    public class ProductInOutService : BaseService<ProductInOut>, IProductInOutService
    {
        public ProductInOutService(IUnitOfWork unitOfWork, IRepository<ProductInOut> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<List<ProductInOut>> GetAllByProductIdAsync(int productId)
        {
            return (List<ProductInOut>)await _unitOfWork.ProductInOut.Where(p => p.ProductId == productId);
        }

        public async Task<ProductInOut> GetWithProductByIdAsync(int id)
        {
            return await _unitOfWork.ProductInOut.SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}
