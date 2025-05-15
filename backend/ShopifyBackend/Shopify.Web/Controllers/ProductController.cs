using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shopify.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Images()
        {
            return View();
        }
    }
}
