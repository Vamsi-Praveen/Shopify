using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shopify.Core.Entities
{
    public class ProductImage
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        [StringLength(1024)]
        public string ImageUrl { get; set; }

        [StringLength(255)]
        public string? AltText { get; set; }

        public bool IsPrimary { get; set; } = false;
        public int DisplayOrder { get; set; } = 0;

        // Navigation Property
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
