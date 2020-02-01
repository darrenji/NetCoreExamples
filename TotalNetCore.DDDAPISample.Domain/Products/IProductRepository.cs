using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TotalNetCore.DDDAPISample.Domain.Products
{
    public interface IProductRepository
    {
        Task<List<Product>> GetByIdsAsync(List<ProductId> ids);

        Task<List<Product>> GetAllAsync();
    }
}
