using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopify.Core.Data;
using Shopify.Core.Domain.Repositories;
using Shopify.Core.DTOs;
using Shopify.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.Repositories
{
    public class ProductImagesRepository : GenericRepository<Productimage, ShopifyContext>,
        IProductImagesRepository
    {
        private readonly ILogger _logger;
        public ProductImagesRepository(ShopifyContext context, ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<List<Productimage>> GetProductImagesByProductId(Guid productId)
        {
            try
            {
                return await Context.Productimages.AsNoTracking().Where(u => u.ProductId == productId).ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError("GetProductImageByProductId::Database exception: {0}", error);
                return null;
            }
        }

        public async Task<IEnumerable<ProductImageLookupDTO>> GetProductUrlsByProductId(Guid productId)
        {
            try
            {
                return await Context.Productimages
                    .AsNoTracking()
                    .Where(u => u.ProductId == productId)
                    .Select(p => new ProductImageLookupDTO()
                    {
                        Id = p.Id,
                        ImageURL = p.ImageUrl
                    })
                    .ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError("GetProductImageByProductId::Database exception: {0}", error);
                return null;
            }
        }

        public async Task<bool> DeleteProductImage(Guid imageId)
        {
            try
            {
                var image = await Context.Productimages.FirstOrDefaultAsync(u => u.Id == imageId);
                if (image != null) { 
                    await Context.Productimages.Remove(image);
                }
                return true;
            }
            catch (Exception error)
            {
                _logger.LogError("GetProductImageByProductId::Database exception: {0}", error);
                return false;
            }
        }
    }
}
