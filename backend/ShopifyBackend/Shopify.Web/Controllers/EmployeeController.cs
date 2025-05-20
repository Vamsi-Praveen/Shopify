using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopify.Core.Domain.Services;
using Shopify.Core.Entities;
using Shopify.Core.Enums;
using Shopify.Web.DTO;
using Shopify.Web.Models;

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
                Status = UserStatusEnum.NEW.ToString(),
                DateOfBirth = user.DateOfBirth,
                PasswordHash = "$2a$12$V2FPj/Zbv8IPfQhUTiet.uX1oGRJ48Z2HMY94.lJRdrZ9qikA9vYC"
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


        public IActionResult Reset()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Reset(ResetEmployeePassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isUserExists = await _userService.GetUserByEmail(model.Email);

            if (isUserExists == null)
            {
                TempData["ModalType"] = "Error";
                TempData["ModalTitle"] = "No User Found";
                TempData["ModalMessage"] = $"User with given email address is not found.";
                return View(model);
            }

            var resetPassword = await _userService.ResetNewUserPassword(model.Email, "$2a$12$V2FPj/Zbv8IPfQhUTiet.uX1oGRJ48Z2HMY94.lJRdrZ9qikA9vYC",UserStatusEnum.NEW);

            if (!resetPassword.Success)
            {
                TempData["ModalType"] = "Error";
                TempData["ModalTitle"] = "Failed to Reset Password";
                TempData["ModalMessage"] = $"{resetPassword.Message}";
            }
            else
            {
                TempData["ModalType"] = "Success";
                TempData["ModalTitle"] = "Password Reset Successfull";
                TempData["ModalMessage"] = "You can now login with default user and reset the password.";
            }

            return RedirectToAction("Reset", "Employee");


        }
    }
}
