using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopify.Core.Communication;
using Shopify.Core.Domain.Services;
using Shopify.Core.DTOs;
using Shopify.Core.Entities;
using Shopify.Core.Utilities;
using Shopify.Web.DTO;
using Shopify.Web.Models;
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
        public IActionResult Images()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var allProducts = await productService.GetAllProductsAsync(); 
            return View(allProducts);
        }

        public async Task<IActionResult> Brands()
        {
            var allBrands = await brandService.GetAllBrandsAsync();
            var brandViewModel = new BrandViewModel()
            {
                Brands = allBrands
            };
            return View(brandViewModel);
        }

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

            var imageUrl = await _blobService.UploadImageAsync(brandDto.ThumbnailFile);

            if (imageUrl == null)
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

        [HttpGet]
        public async Task<IEnumerable<ProductLookupDto>> SearchProduct(string productName)
        {
            var products = await productService.SearchProductByName(productName);
            return products;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductImageLookupDTO>> GetProductImages(Guid productId)
        {
            var productImages = await productService.GetAllProductImages(productId);
            return productImages;
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
        public async Task<ServiceResult> DeleteProductImage(Guid imageId)
        {
            var result = await productService.DeleteProductImage(imageId);
            if (result)
            {
                return new ServiceResult(true, "Product Image Deleted Successfully");
            }
            return new ServiceResult(false, "Failed to Delete Image");
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

            var imageUrl = await _blobService.UploadImageAsync(category.ImageFile);

            if (imageUrl == null)
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
    }
}
