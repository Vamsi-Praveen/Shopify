using Shopify.Core.Domain.Repositories;
using Shopify.Core.Domain.Services;
using Shopify.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _unitOfWork.Category.GetAllCategories();
        }

        public async Task<bool> CreateCategory(Category category)
        {
            try
            {
                await _unitOfWork.Category.CreateCategory(category);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Category>GetCategoryDetailsById(Guid categoryId)
        {
            try
            {
                return await _unitOfWork.Category.GetCategoryById(categoryId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeleteCategory(Guid categoryId)
        {
            try
            {
                var category = await _unitOfWork.Category.GetCategoryById(categoryId);
                _unitOfWork.Category.Remove(category);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
