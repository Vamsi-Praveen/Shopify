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
    public class BrandService:IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _unitOfWork.Brands.GetAllAsync();
        }

        public async Task<bool> CreateBrand(Brand brand)
        {
            try
            {
                await _unitOfWork.Brands.AddAsync(brand);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Brand> GetBrandDetailsById(Guid brandId)
        {
            try
            {
                return await _unitOfWork.Brands.GetBrandById(brandId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
