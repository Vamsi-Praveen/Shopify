using Microsoft.Extensions.Logging;
using Shopify.Core.Data;
using Shopify.Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private ILogger<UnitOfWork> _logger;
        private readonly ShopifyContext _shopifyContext;

        private IUserRepository _user;
        private IProductRepository _product;
        private IProductImagesRepository _productImages;
        private IProductReviewRepository _productReviews;
        private IBrandsRepository _brands;
        private ICategoryRepository _category;

        public UnitOfWork(ShopifyContext shopifyContext, ILogger<UnitOfWork> Logger)
        {
            _shopifyContext = shopifyContext;
            _logger = Logger;
        }

        public IUserRepository User
        {
            get { return _user = _user ?? new UserRepository(_shopifyContext, _logger); }
        }

        public IProductRepository Product
        {
            get { return _product = _product ?? new ProductRepository(_shopifyContext, _logger); }
        }

        public IProductImagesRepository ProductImages
        {
            get { return _productImages = _productImages ?? new ProductImagesRepository(_shopifyContext, _logger); }
        }

        public IProductReviewRepository ProductReviews
        {
            get { return _productReviews = _productReviews ?? new ProductReviewRepository(_shopifyContext, _logger); }
        }

        public IBrandsRepository Brands
        {
            get { return _brands = _brands ?? new BrandsRepository(_shopifyContext, _logger); }
        }

        public ICategoryRepository Category
        {
            get { return _category = _category ?? new CategoryRepository(_shopifyContext, _logger); }
        }

        public async Task<int> SaveAsync()
        {
            return await _shopifyContext.SaveChangesAsync();
        }

        public void DisableDetectChanges()
        {
            _shopifyContext.ChangeTracker.AutoDetectChangesEnabled = false;
            return;
        }

        public void EnableDetectChanges()
        {
            _shopifyContext.ChangeTracker.AutoDetectChangesEnabled = true;
            return;
        }
        public int Save()
        {
            return _shopifyContext.SaveChanges();
        }

    }
}
