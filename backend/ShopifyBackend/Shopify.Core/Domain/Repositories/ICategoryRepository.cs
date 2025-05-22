using Shopify.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.Domain.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public Task<IEnumerable<Category>> GetAllCategories();

        public Task<bool> CreateCategory (Category category);
    }
}
