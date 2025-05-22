using Shopify.Core.DTOs;
using Shopify.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.Domain.Repositories
{
    public interface IProductImagesRepository : IGenericRepository<Productimage>
    {
        public Task<List<Productimage>> GetProductImagesByProductId(Guid productId);
        public Task<IEnumerable<ProductImageLookupDTO>> GetProductUrlsByProductId(Guid productId);
        public Task<Productimage> GetProductImageByImageId(Guid imageId);
        public Task<bool> DeleteProductImage(Guid imageId);
        public Task<bool> AddProductImage(Productimage productimage);
    }
}
