using Shopify.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.Domain.Services
{
    public interface ICategoryService
    {
       public Task<IEnumerable<Category>> GetAllCategoriesAsync();

       public Task<bool> CreateCategory(Category category);
    }
}
