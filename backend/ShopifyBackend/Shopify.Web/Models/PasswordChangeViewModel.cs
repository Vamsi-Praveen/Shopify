namespace Shopify.Web.Models
{
    public class PasswordChangeViewModel
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
