using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }

        IProductRepository Product { get; }

        IProductImagesRepository ProductImages { get; }

        IProductReviewRepository ProductReviews { get; }

        Task<int> SaveAsync();

        void DisableDetectChanges();

        void EnableDetectChanges();

        int Save();
    }
}
