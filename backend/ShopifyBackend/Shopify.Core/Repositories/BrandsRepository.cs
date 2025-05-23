using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopify.Core.Data;
using Shopify.Core.Domain.Repositories;
using Shopify.Core.Entities;
using Shopify.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.Repositories
{
    public class BrandsRepository : GenericRepository<Brand, ShopifyContext>,
        IBrandsRepository
    {
        private readonly ILogger _logger;
        public BrandsRepository(ShopifyContext context, ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Brand>> GetAllBrands()
        {
            try
            {
                return await Context.Brands
                        .AsNoTracking()
                        .ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError("GetAllEmployees::Database exception: {0}", error);
                return null;
            }
        }

        public async Task<Brand> GetBrandById(Guid brandId)
        {
            try
            {
                return await Context.Brands.AsNoTracking().SingleOrDefaultAsync(u => u.Id == brandId);
            }
            catch (Exception error)
            {
                _logger.LogError("GetBrandById::Database exception: {0}", error);
                return null;
            }
        }
    }
}
