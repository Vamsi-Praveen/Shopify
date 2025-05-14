using Shopify.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shopify.Core.Entities
{
    public class Users
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Please Enter Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        public string Name {  get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please Enter Valid EmailAddress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Phone(ErrorMessage = "Please Enter Valid PhoneNumber")]
        [StringLength(10, ErrorMessage = "Please Enter Valid PhoneNumber")]
        public string? PhoneNumber { get; set; }

        [Url]
        public string ProfilePicURL { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of birth is required.")]
        public DateOnly DateOfBirth {  get; set; }
        public bool EmailConfirmed { get; set; } = false;
        public bool PhoneNumberConfirmed { get; set; } = false;
        public bool IsActive { get; set; } = true;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset? LastLogin {  get; set; }

        public UserRole Role { get; set; } = UserRole.Customer;
    }
}
