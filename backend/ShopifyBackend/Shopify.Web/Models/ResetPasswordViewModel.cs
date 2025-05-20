using System.ComponentModel.DataAnnotations;

namespace Shopify.Web.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
