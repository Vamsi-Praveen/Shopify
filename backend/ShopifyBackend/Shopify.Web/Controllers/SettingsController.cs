using Microsoft.AspNetCore.Mvc;

namespace Shopify.Web.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Changepassword()
        {
            return View();
        }
    }
}
