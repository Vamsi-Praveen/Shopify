﻿using Microsoft.EntityFrameworkCore;
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
    public class CategoryRepository : GenericRepository<Category, ShopifyContext>,
        ICategoryRepository
    {
        private readonly ILogger _logger;
        public CategoryRepository(ShopifyContext context, ILogger logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            try
            {
                return await Context.Categories
                        .AsNoTracking()
                        .ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError("GetAllEmployees::Database exception: {0}", error);
                return null;
            }
        }

        public async Task<bool> CreateCategory(Category category)
        {
            try
            {
                await Context.Categories.AddAsync(category);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception error)
            {
                _logger.LogError("CreateCategory::Database exception: {0}", error);
                return false;
            }
        }

        public async Task<Category> GetCategoryById(Guid categoryId)
        {
            try
            {
                return await Context.Categories.AsNoTracking().SingleOrDefaultAsync(u => u.Id == categoryId);
            }
            catch (Exception error)
            {
                _logger.LogError("GetCategoryById::Database exception: {0}", error);
                return null;
            }
        }
    }

}
