using Shopify.Core.Entities;

namespace Shopify.Web.Models
{
    public class CategoriesViewModel
    {
        public IEnumerable<Category> Categories;

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
