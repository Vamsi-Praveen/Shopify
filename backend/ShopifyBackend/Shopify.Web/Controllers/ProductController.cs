using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopify.Core.Communication;
using Shopify.Core.Domain.Services;
using Shopify.Core.DTOs;
using Shopify.Core.Entities;
using Shopify.Core.Utilities;
using Shopify.Web.DTO;
using Shopify.Web.Models;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Shopify.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IBrandService brandService;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly AzureBlobService _blobService;

        public ProductController(IBrandService brandService, AzureBlobService blobService,ICategoryService categoryService, IProductService productService)
        {
            this.brandService = brandService;
            this._blobService = blobService;
            this.categoryService = categoryService;
            this.productService = productService;
        }
        
        // Product
        public async Task<IActionResult> Add()
        {
            var allBrands = await brandService.GetAllBrandsAsync();
            var allCategories = await categoryService.GetAllCategoriesAsync();
            var productView = new ProductViewModel()
            {
                brands = allBrands.Where(b => b.IsActive == true),
                categories = allCategories.Where(b => b.IsActive == true)
            };

            return View(productView);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel product)
        {
            var allBrands = await brandService.GetAllBrandsAsync();
            var allCategories = await categoryService.GetAllCategoriesAsync();
            product.brands = allBrands.Where(b=>b.IsActive == true);
            product.categories = allCategories.Where(b => b.IsActive == true);
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var imageUrl = await _blobService.UploadImageAsync(product.ThumbnailImage);

            if (imageUrl == null)
            {
                TempData["ModalType"] = "ERROR";
                TempData["ModalTitle"] = "Creation Failed";
                TempData["ModalMessage"] = "Could add image to our DB.";
                return RedirectToAction("Brands", "Product");
            }


            var newProduct = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                ShortDescription = product.ShortDescription,
                IsFeatured = product.IsFeatured,
                ThumbnailImage = imageUrl,
                Sku = product.Sku,
                SellingPrice = product.SellingPrice,
                BasePrice = product.BasePrice,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                UnitOfMeasure = product.UnitOfMeasure
            };

            bool isSuccess = await productService.CreateProduct(newProduct);

            if (isSuccess)
            {
                TempData["ModalType"] = "Success";
                TempData["ModalTitle"] = "Product Created!";
                TempData["ModalMessage"] = $"Brand {product.Name} has been created successfully.";
            }
            else
            {
                TempData["ModalType"] = "ERROR";
                TempData["ModalTitle"] = "Creation Failed";
                TempData["ModalMessage"] = "Could not create the user due to an unexpected error. Please check the details and try again, or contact support.";
            }

            return RedirectToAction("Add", "Product");


        }
       
        public async Task<IActionResult> List()
        {
            var allProducts = await productService.GetAllProductsAsync(); 
            return View(allProducts);
        }

        [HttpGet]
        public async Task<IEnumerable<ProductLookupDto>> SearchProduct(string productName)
        {
            var products = await productService.SearchProductByName(productName);
            return products;
        }

        public async Task<IActionResult> ProductDetails(Guid id)
        {
            var allBrands = await brandService.GetAllBrandsAsync();
            var allCategories = await categoryService.GetAllCategoriesAsync();
            var product = await productService.GetProductDetailsById(id);
            var productView = new ProductViewModel()
            {
                Id = id,
                brands = allBrands.Where(b => b.IsActive == true),
                categories = allCategories.Where(b => b.IsActive == true),
                Name = product.Name,
                Description = product.Description,
                Sku = product.Sku,
                IsFeatured = (bool)product.IsFeatured,
                ShortDescription = product.ShortDescription,
                BasePrice = product.BasePrice,
                SellingPrice = product.SellingPrice,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                UnitOfMeasure = product.UnitOfMeasure,
            };

            return View(productView);
        }

        [HttpPost]
        public async Task<ServiceResult> DeleteProductDetails(Guid productId)
        {
            var result = await productService.DeleteProduct(productId);
            if (result == false)
            {
                return new ServiceResult(false, "Failed to Delete Product Details");
            }
            return new ServiceResult(true, "Product details deleted successfully");
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var allBrands = await brandService.GetAllBrandsAsync();
            var allCategories = await categoryService.GetAllCategoriesAsync();
            var product = await productService.GetProductDetailsById(id);
            var productView = new ProductViewModel()
            {
                Id = id,
                brands = allBrands.Where(b => b.IsActive == true),
                categories = allCategories.Where(b => b.IsActive == true),
                Name = product.Name,
                Description = product.Description,
                Sku = product.Sku,
                IsFeatured = (bool)product.IsFeatured,
                ShortDescription = product.ShortDescription,
                BasePrice = product.BasePrice,
                SellingPrice = product.SellingPrice,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                UnitOfMeasure = product.UnitOfMeasure,
            };

            return View(productView);
        }


        // Product Images
        public IActionResult Images()
        {
            return View();
        }

        [HttpGet]
        public async Task<ServiceResult> GetProductImages(string productId)
        {
            if (!Guid.TryParse(productId, out Guid parsedproductId))
            {
                return new ServiceResult(false, "Invalid GUID format");
            }
            var productImages = await productService.GetAllProductImages(parsedproductId);
            if(productImages == null)
            {
                return new ServiceResult(false, "No Product Images found");
            }
            return new ServiceResult(true, "Product Images found", productImages);
        }

        [HttpPost]
        public async Task<ServiceResult> UploadProductImage(ProductImageUploadDTO product)
        {
            var imageUrl = await _blobService.UploadImageAsync(product.Image);
            if (imageUrl == null)
            {
                return new ServiceResult(false, "Failed to Upload Image in DB");
            }

            product.ImageUrl = imageUrl;

            var result = await productService.UploadProductImage(product);

            if (result)
            {
                return new ServiceResult(true, "Product Image Uploaded Successfully");
            }
            return new ServiceResult(false, "Failed to Upload Image");
        }

        [HttpGet]
        public async Task<ServiceResult> DeleteProductImage(string imageId)
        {
            if (!Guid.TryParse(imageId, out Guid parsedImageId))
            {
                return new ServiceResult(false, "Invalid GUID format");
            }
            var result = await productService.DeleteProductImage(parsedImageId);
            if (result)
            {
                return new ServiceResult(true, "Product Image Deleted Successfully");
            }
            return new ServiceResult(false, "Failed to Delete Image");
        }


        // Brands
        public async Task<IActionResult> Brands()
        {
            var allBrands = await brandService.GetAllBrandsAsync();
            var brandViewModel = new BrandViewModel()
            {
                Brands = allBrands
            };
            return View(brandViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Brands(BrandViewModel brandDto)
        {
            if (!ModelState.IsValid)
            {
                var brands = await brandService.GetAllBrandsAsync();
                TempData["OpenModal"] = true;
                return View(new BrandViewModel
                {
                    Brands = brands
                });
            }

            string imageUrl = null;
            if (brandDto.ThumbnailFile != null)
            {
                imageUrl = await _blobService.UploadImageAsync(brandDto.ThumbnailFile);
            }

            if (brandDto.ThumbnailFile != null && imageUrl == null)
            {
                TempData["ModalType"] = "ERROR";
                TempData["ModalTitle"] = "Creation Failed";
                TempData["ModalMessage"] = "Could add image to our DB.";
                return RedirectToAction("Brands", "Product");
            }

            Brand brand = new Brand()
            {
                Id = Guid.NewGuid(),
                Name = brandDto.Name,
                Description = brandDto.Description,
                LogoUrl = imageUrl
            };

            bool isSuccess = await brandService.CreateBrand(brand);

            if (isSuccess)
            {
                TempData["ModalType"] = "Success";
                TempData["ModalTitle"] = "Brand Created!";
                TempData["ModalMessage"] = $"Brand {brand.Name} has been created successfully.";
            }
            else
            {
                TempData["ModalType"] = "ERROR";
                TempData["ModalTitle"] = "Creation Failed";
                TempData["ModalMessage"] = "Could not create the user due to an unexpected error. Please check the details and try again, or contact support.";
            }

            return RedirectToAction("Brands", "Product");
        }

        [HttpPost]
        public async Task<ServiceResult> UpdateBrandDetails(BrandViewModel brandDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(false, "Invalid Modal states");
            }

            string imageUrl = null;
            if (brandDto.ThumbnailFile != null)
            {
                imageUrl = await _blobService.UploadImageAsync(brandDto.ThumbnailFile);
            }

            if (brandDto.ThumbnailFile != null && imageUrl == null)
            {
                return new ServiceResult(false, "Failed to add Image in DB");
            }

            BrandDTO brand = new BrandDTO()
            {
                Id = brandDto.Id,
                Name = brandDto.Name,
                Description = brandDto.Description,
                LogoUrl = imageUrl,
                IsActive = brandDto.IsActive
            };
            brand.UpdatedAt = DateTime.Now;

            var result = await brandService.UpdateBrand(brand);

            return result;
        }

        public async Task<IActionResult> BrandDetails(Guid id)
        {
            var brand = await brandService.GetBrandDetailsById(id);
            var brandView = new BrandViewModel()
            {
                Name = brand.Name,
                Description = brand.Description,
                ThumbnailFileUrl = brand.LogoUrl,
            };

            return View(brandView);
        }

        [HttpPost]
        public async Task<ServiceResult> DeleteBrandDetails(Guid brandId)
        {
            var result = await brandService.DeleteBrand(brandId);
            if (result == false)
            {
                return new ServiceResult(false, "Failed to Delete Brand Details");
            }
            return new ServiceResult(true, "Brand details deleted successfully");
        }

        //[HttpPost]
        //public async Task<IActionResult> UpdateBrands(BrandViewModel brandDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var brands = await brandService.GetAllBrandsAsync();
        //        TempData["OpenModal"] = true;
        //        return View(new BrandViewModel
        //        {
        //            Brands = brands
        //        });
        //    }

        //    string imageUrl = null;
        //    if (brandDto.ThumbnailFile != null)
        //    {
        //        imageUrl = await _blobService.UploadImageAsync(brandDto.ThumbnailFile);
        //    }

        //    if (brandDto.ThumbnailFile != null && imageUrl == null)
        //    {
        //        TempData["ModalType"] = "ERROR";
        //        TempData["ModalTitle"] = "Creation Failed";
        //        TempData["ModalMessage"] = "Could add image to our DB.";
        //        return RedirectToAction("Brands", "Product");
        //    }

        //    Brand brand = new Brand()
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = brandDto.Name,
        //        Description = brandDto.Description,
        //        LogoUrl = imageUrl
        //    };

        //    bool isSuccess = await brandService.CreateBrand(brand);

        //    if (isSuccess)
        //    {
        //        TempData["ModalType"] = "Success";
        //        TempData["ModalTitle"] = "Brand Created!";
        //        TempData["ModalMessage"] = $"Brand {brand.Name} has been created successfully.";
        //    }
        //    else
        //    {
        //        TempData["ModalType"] = "ERROR";
        //        TempData["ModalTitle"] = "Creation Failed";
        //        TempData["ModalMessage"] = "Could not create the user due to an unexpected error. Please check the details and try again, or contact support.";
        //    }

        //    return RedirectToAction("Brands", "Product");
        //}


        // Categories
        public async Task<IActionResult> Categories()
        {
            var allCategories = await categoryService.GetAllCategoriesAsync();
            var category = new CategoriesViewModel()
            {
                Categories = allCategories
            };
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Categories(CategoriesViewModel category)
        {
            if (!ModelState.IsValid)
            {
                var categories = await categoryService.GetAllCategoriesAsync();
                TempData["OpenModal"] = true;
                return View(new CategoriesViewModel
                {
                    Categories= categories
                });
            }

            string imageUrl = null;
            if (category.ImageFile != null)
            {
                imageUrl = await _blobService.UploadImageAsync(category.ImageFile);
            }

            if (category.ImageFile != null && imageUrl == null)
            {
                TempData["ModalType"] = "ERROR";
                TempData["ModalTitle"] = "Creation Failed";
                TempData["ModalMessage"] = "Could add image to our DB.";
                return RedirectToAction("Categories", "Product");
            }

            Category c = new Category()
            {
                Id = Guid.NewGuid(),
                Name = category.Name,
                ImageUrl = imageUrl,
                Description = category.Description,
            };

            bool isSuccess = await categoryService.CreateCategory(c);

            if (isSuccess)
            {
                TempData["ModalType"] = "Success";
                TempData["ModalTitle"] = "Category Created!";
                TempData["ModalMessage"] = $"Category {category.Name} has been created successfully.";
            }
            else
            {
                TempData["ModalType"] = "ERROR";
                TempData["ModalTitle"] = "Creation Failed";
                TempData["ModalMessage"] = "Could not create the user due to an unexpected error. Please check the details and try again, or contact support.";
            }

            return RedirectToAction("Categories", "Product");
        }

        public async Task<IActionResult> CategoryDetails(Guid id)
        {
            var category = await categoryService.GetCategoryDetailsById(id);
            var categoryView = new CategoriesViewModel()
            {
                Name = category.Name,
                Description = category.Description,
                ImageFileUrl = category.ImageUrl,
            };

            return View(categoryView);
        }
        [HttpPost]
        public async Task<ServiceResult> DeleteCategoryDetails(Guid categoryId)
        {
            var result = await categoryService.DeleteCategory(categoryId);
            if (result == false)
            {
                return new ServiceResult(false, "Failed to Delete Category Details");
            }
            return new ServiceResult(true, "Category details deleted successfully");
        }

    }
}
