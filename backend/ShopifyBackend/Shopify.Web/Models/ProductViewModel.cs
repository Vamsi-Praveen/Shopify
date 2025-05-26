using Shopify.Core.Entities;

namespace Shopify.Web.Models
{
    public class ProductViewModel
    {
        public IEnumerable<Brand> brands;
        
        public IEnumerable<Category> categories;

        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Sku { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? ShortDescription { get; set; }

        public Guid CategoryId { get; set; }

        public Guid? BrandId { get; set; }

        public decimal BasePrice { get; set; }

        public decimal SellingPrice { get; set; }
        public string UnitOfMeasure { get; set; } = null!;

        public bool IsFeatured { get; set; }

        public IFormFile? ThumbnailImage { get; set; }

        public string? CategoryName { get; set; }

        public string? BrandName { get; set; }

        public string? ImageUrl { get; set; }
    }
}
