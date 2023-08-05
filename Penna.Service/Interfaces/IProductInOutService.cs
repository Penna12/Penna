using Penna.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface IProductInOutService :IService<ProductInOut>
    {
        Task<ProductInOut> GetWithProductByIdAsync(int id);
        Task<List<ProductInOut>> GetAllByProductIdAsync(int productId);
    }
}
