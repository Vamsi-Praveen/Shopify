using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopify.Core.Domain.Services;
using Shopify.Core.Entities;
using Shopify.Core.Utilities;
using Shopify.Web.DTO;
using Shopify.Web.Models;

namespace Shopify.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IBrandService brandService;
        private readonly AzureBlobService _blobService;

        public ProductController(IBrandService brandService, AzureBlobService blobService)
        {
            this.brandService = brandService;
            this._blobService = blobService;
        }

        public async Task<IActionResult> Add()
        {
            var allBrands = await brandService.GetAllBrandsAsync();
            var productView = new ProductViewModel()
            {
                brands = allBrands.Where(b => b.IsActive == true),
                categories = []
            };

            return View(productView);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            return View();
            

        }
        public IActionResult Images()
        {
            return View();
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

        public IActionResult Categories()
        {
            return View();
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
    }
}
