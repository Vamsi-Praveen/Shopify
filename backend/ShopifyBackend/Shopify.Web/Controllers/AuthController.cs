using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Shopify.Web.Models;
using Shopify.Core.Data;
using Shopify.Core.Entities;

namespace Shopify.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel user, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var validUser = await _context.users.FirstOrDefaultAsync(u => u.Email == user.Email);


            if (validUser == null)
            {
                user.ErrorMessage = "No User Found";
                return View(user);
            }

            bool isVerifiedPassword = BCrypt.Net.BCrypt.Verify(user.Password, validUser.Password);

            if (!isVerifiedPassword)
            {
                user.ErrorMessage = "Invalid Credentials";
                return View(user);
            }

            var lastLogin = validUser.LastLogin;

            if (lastLogin == null)
            {
                lastLogin = DateTimeOffset.UtcNow;
            }

            validUser.LastLogin = DateTimeOffset.UtcNow;
            await _context.SaveChangesAsync();

            //claims
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,validUser.Name),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier,validUser.Id.ToString()),
                new Claim("LastLogin", lastLogin?.ToString() ?? ""),
                new Claim(ClaimTypes.Role, validUser.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {  
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}
