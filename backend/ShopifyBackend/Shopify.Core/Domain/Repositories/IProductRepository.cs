using Shopify.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.Domain.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<IEnumerable<Product>> GetAllProductsList();

        public Task<Product> GetProductById(Guid productId);

        public Task<Product> AddProduct(Product product);
    }
}
