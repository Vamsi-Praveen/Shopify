using Shopify.Core.Entities;
using Shopify.Web.DTO;

namespace Shopify.Web.Models
{
    public class BrandViewModel
    {
        public IEnumerable<Brand> Brands;

        public Guid Id { get; set; }
        
        public string Name { get; set; } = null!;

        public IFormFile? ThumbnailFile { get; set; }
        public string? ThumbnailFileUrl { get; set; }

        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
