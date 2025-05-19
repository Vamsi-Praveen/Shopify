using Shopify.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.Domain.Repositories
{
    public interface IProductReviewRepository : IGenericRepository<Productreview>
    {
        public Task<List<Productreview>> GetProductReviewsByProductId(Guid productId);
    }
}
