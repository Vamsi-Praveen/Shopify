using Shopify.Core.Communication;
using Shopify.Core.Domain.Repositories;
using Shopify.Core.Domain.Services;
using Shopify.Core.DTOs;
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

        public async Task<ServiceResult> UpdateBrand(BrandDTO brand)
        {
            try
            {
                var brandInDb = await _unitOfWork.Brands.GetBrandById(brand.Id);
                if(brandInDb == null)
                {
                    return new ServiceResult(false, "Brand details not found");
                }

                brandInDb.Name = brand.Name;
                brandInDb.IsActive = brand.IsActive;
                brandInDb.LogoUrl = brand.LogoUrl;
                brandInDb.Description = brand.Description;
                brandInDb.UpdatedAt = brand.UpdatedAt;

                _unitOfWork.Brands.Update(brandInDb);
                await _unitOfWork.SaveAsync();
                return new ServiceResult(true,"Successfully Updated Brand Details");
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, "Failed to update brand details");
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

        public async Task<bool> DeleteBrand(Guid brandId)
        {
            try
            {
                var brand = await _unitOfWork.Brands.GetBrandById(brandId);
                _unitOfWork.Brands.Remove(brand);
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
