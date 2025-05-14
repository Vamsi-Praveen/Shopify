using System.ComponentModel.DataAnnotations;

namespace Shopify.Web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter valid Password")]
        public string Password { get; set; }

        public string? ErrorMessage { get; set; }

        public string? ReturnUrl { get; set; }

    }
}
