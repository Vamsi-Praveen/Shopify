using System.ComponentModel.DataAnnotations;

namespace Shopify.Web.Models
{
    public class ResetEmployeePassword
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
