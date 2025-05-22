using Shopify.Core.DTOs;
using Shopify.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.Domain.Services
{
    public interface IProductService
    {
        public Task<bool> CreateProduct(Product product);
        public Task<bool> UpdateProduct(Product product);
        public Task<bool> DeleteProduct(Guid productId);

        public Task<IEnumerable<Product>> GetAllProductsAsync();

        public Task<IEnumerable<ProductLookupDto>> SearchProductByName(string name);
        public Task<IEnumerable<ProductImageLookupDTO>> GetAllProductImages(Guid productId);
    }
}
