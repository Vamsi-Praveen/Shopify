﻿using Shopify.Core.Communication;
using Shopify.Core.DTOs;
using Shopify.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.Domain.Services
{
    public interface IBrandService
    {
       public Task<IEnumerable<Brand>> GetAllBrandsAsync();
       public Task<bool> CreateBrand(Brand brand);
       public Task<Brand> GetBrandDetailsById(Guid parsedBrandId);
       public Task<bool> DeleteBrand(Guid brandId);
       public Task<ServiceResult> UpdateBrand(BrandDTO brand);
    }
}
