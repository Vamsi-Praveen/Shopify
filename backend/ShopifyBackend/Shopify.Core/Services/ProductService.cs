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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateProduct(Product product)
        {
            try
            {
                await _unitOfWork.Product.AddProduct(product);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                _unitOfWork.Product.Update(product);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            try
            {
                // Remove related product images and prodcut review 
                var productReviews = await _unitOfWork.ProductReviews.GetProductReviewsByProductId(productId);
                foreach (var review in productReviews)
                {
                    _unitOfWork.ProductReviews.Remove(review);
                }


                var productImages = await _unitOfWork.ProductImages.GetProductImagesByProductId(productId);
                foreach (var review in productImages)
                {
                    _unitOfWork.ProductImages.Remove(review);
                }


                var product = await _unitOfWork.Product.GetProductById(productId);
                _unitOfWork.Product.Remove(product);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {                
                var allProducts = await _unitOfWork.Product.GetAllProductsDetails();
                return allProducts;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ProductLookupDto>> SearchProductByName(string name)
        {
            try
            {
                var resultProducts = await _unitOfWork.Product.SearchProductsByNameAsync(name);
                return resultProducts;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<IEnumerable<ProductImageLookupDTO>> GetAllProductImages(Guid productId)
        {
            try
            {
                var resultProducts = await _unitOfWork.ProductImages.GetProductUrlsByProductId(productId);
                return resultProducts;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeleteProductImage(Guid imageId)
        {
            try
            {
                var result = await _unitOfWork.ProductImages.DeleteProductImage(imageId);
                await _unitOfWork.SaveAsync();
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
