using Microsoft.Extensions.Logging;
using Shopify.Core.Domain.Repositories;
using Shopify.Core.Domain.Services;
using Shopify.Core.DTOs;
using Shopify.Core.Entities;
using Shopify.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Shopify.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AzureBlobService _azureBlobService;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IUnitOfWork unitOfWork, AzureBlobService azureBlobService, ILogger<ProductService> logger)
        {
            _unitOfWork = unitOfWork;
            _azureBlobService = azureBlobService;
            _logger = logger;
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

        public async Task<Product> GetProductDetailsById(Guid productId)
        {
            try
            {
                return await _unitOfWork.Product.GetProductById(productId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            try
            {
                // Remove related product images and product review 
                var productReviews = await _unitOfWork.ProductReviews.GetProductReviewsByProductId(productId);
                foreach (var review in productReviews)
                {
                    _unitOfWork.ProductReviews.Remove(review);
                }


                var productImages = await _unitOfWork.ProductImages.GetProductImagesByProductId(productId);
                var res = true;
                foreach (var image in productImages)
                {
                    res = await _azureBlobService.DeleteImageAsync(image.ImageUrl);
                    if(res== false)
                    {
                        _logger.LogError($"Unable to delete Product Image from DB - {image.ImageUrl}");
                        return false;
                    }
                }
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

        public async Task<bool> UploadProductImage(ProductImageUploadDTO uploadDTO)
        {
            try
            {
                Productimage productimage = new Productimage()
                {
                    ProductId = uploadDTO.ProductId,
                    ImageUrl = uploadDTO.ImageUrl
                };

                var upload = await _unitOfWork.ProductImages.AddProductImage(productimage);
                return upload;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteProductImage(Guid imageId)
        {
            try
            {
                var imageInDb = await _unitOfWork.ProductImages.GetProductImageByImageId(imageId);

                var res = await _azureBlobService.DeleteImageAsync(imageInDb.ImageUrl);
                if(!res)
                {
                    _logger.LogError($"Unable to delete Product Image from DB - {imageInDb.ImageUrl}");
                    return false;
                }

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
