﻿using Shopify.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.Domain.Repositories
{
    public interface IBrandsRepository : IGenericRepository<Brand>
    {
        public Task<IEnumerable<Brand>> GetAllBrands();
        public Task<Brand> GetBrandById(Guid brandId);
    }
}
