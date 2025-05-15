using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shopify.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }
    }
}
