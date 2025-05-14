using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopify.Core.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Sku { get; set; }

        [Required]
        public string Description { get; set; }

        [StringLength(500)]
        public string? ShortDescription { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        public Guid? BrandId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BasePrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal SellingPrice { get; set; }

        [Required]
        [StringLength(50)]
        public string UnitOfMeasure { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; } = false;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    }
}
