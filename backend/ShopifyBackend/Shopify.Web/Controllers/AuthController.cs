using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Shopify.Web.Models;
using Shopify.Core.Data;
using Shopify.Core.Entities;
using Shopify.Core.Domain.Services;
using Shopify.Core.Enums;

namespace Shopify.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;


        public AuthController(IUserService userService)
        {
            _userService = userService;
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
            var validUser = await _userService.VerifyUserLogin(user.Email,user.Password);


            if (!validUser.Success)
            {
                user.ErrorMessage = validUser.Message;
                return View(user);
            }


            var userData = await _userService.GetUserByEmail(user.Email);

            if(userData.Status == UserStatusEnum.NEW.ToString())
            {
                return RedirectToAction("ResetPassword");
            }

            if(userData.Status == UserStatusEnum.SUSPENDED.ToString())
            {
                user.ErrorMessage = "User Login Suspended! Contact Admin";
                return View(user);
            }
            //claims
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,userData.FullName),
                new Claim(ClaimTypes.NameIdentifier,userData.Id.ToString()),
                new Claim("LastLogin", userData.LastLogin.ToString()),
                new Claim(ClaimTypes.Role, userData.Role.ToString())
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


        public IActionResult ResetPassword()
        {
            return View(new ResetPasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                model.ErrorMessage = "Password's must be same";
                return View(model);
            }

            return View(model);
        }
    }
}
