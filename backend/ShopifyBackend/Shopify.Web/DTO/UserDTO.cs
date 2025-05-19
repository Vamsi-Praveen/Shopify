using System.ComponentModel.DataAnnotations;

namespace Shopify.Web.DTO
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter valid email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Employee Name is required")]
        public string FullName { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string Role { get; set; } = null!;


    }
}
