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
    }
}
