using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopify.Core.Domain.Services;
using Shopify.Core.Entities;
using Shopify.Web.DTO;

namespace Shopify.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUserService _userService;

        public EmployeeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var newUser = new User()
            {
                Email = user.Email,
                FullName = user.FullName,
                Username = user.Username,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                DateOfBirth = user.DateOfBirth,
                PasswordHash = "$2a$12$iG3JanJJGwm.ubYl4SWpT.NQGPJGvUDCZWjO/182FmAv7P.AyBHdG"
            };

            bool isSuccess = await _userService.CreateUser(newUser);

            if (isSuccess)
            {
                TempData["ModalType"] = "Success";
                TempData["ModalTitle"] = "Employee Created!";
                TempData["ModalMessage"] = $"User {newUser.Username} has been created successfully.";
            }
            else
            {
                TempData["ModalType"] = "ERROR";
                TempData["ModalTitle"] = "Creation Failed";
                TempData["ModalMessage"] = "Could not create the user due to an unexpected error. Please check the details and try again, or contact support.";
            }

            return RedirectToAction("Add", "Employee");
        }
    }
}
