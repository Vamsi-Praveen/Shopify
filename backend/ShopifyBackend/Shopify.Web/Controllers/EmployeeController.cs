using Microsoft.AspNetCore.Mvc;

namespace Shopify.Web.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }
    }
}
