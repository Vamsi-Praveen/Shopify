using Shopify.Core.Entities;

namespace Shopify.Web.DTO
{
    public class BrandDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public IFormFile? LogoUrl { get; set; }

        public string? Description { get; set; }
    }
}
