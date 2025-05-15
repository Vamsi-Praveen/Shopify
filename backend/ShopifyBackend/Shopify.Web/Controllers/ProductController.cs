using Microsoft.AspNetCore.Mvc;

namespace Shopify.Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }
    }
}
