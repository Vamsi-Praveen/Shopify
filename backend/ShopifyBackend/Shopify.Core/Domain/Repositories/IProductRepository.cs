using Shopify.Core.DTOs;
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

        public Task<IEnumerable<Product>> GetAllProductsDetails();

        public Task<Product> GetProductById(Guid productId);

        public Task<Product> AddProduct(Product product);

        public Task<IEnumerable<ProductLookupDto>> SearchProductsByNameAsync(string searchTerm);
    }
}
