using Shopify.Core.Entities;

namespace Shopify.Web.Models
{
    public class ProductViewModel
    {
        public IEnumerable<Brand> brands;
        
        public IEnumerable<Category> categories;
    }
}
