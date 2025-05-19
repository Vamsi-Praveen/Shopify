using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopify.Core.Data;
using Shopify.Core.Domain.Repositories;
using Shopify.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.Repositories
{
    public class ProductRepository : GenericRepository<Product, ShopifyContext>,
            IProductRepository
    {
        private readonly ILogger _logger;
        public ProductRepository(ShopifyContext context, ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllProductsList()
        {
            return await Context.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(Guid productId)
        {
            try
            {
                return await Context.Products.AsNoTracking().SingleOrDefaultAsync(u => u.Id == productId);
            }
            catch (Exception error)
            {
                _logger.LogError("GetProductById::Database exception: {0}", error);
                return null;
            }
        }
    }
}
