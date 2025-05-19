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
    public class ProductReviewRepository : GenericRepository<Productreview, ShopifyContext>,
            IProductReviewRepository
    {
        private readonly ILogger _logger;
        public ProductReviewRepository(ShopifyContext context, ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<List<Productreview>> GetProductReviewsByProductId(Guid productId)
        {
            try
            {
                return await Context.Productreviews.AsNoTracking().Where(u => u.ProductId == productId).ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError("GetProductReviewsByProductId::Database exception: {0}", error);
                return null;
            }
        }
    }
}
