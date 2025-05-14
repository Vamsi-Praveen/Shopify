using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shopify.Core.Entities
{
    public class Orders
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid BillingAddressId { get; set; }

        [Required]
        public Guid ShippingAddressId { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderStatus { get; set; } = "Pending";

        [Required]
        [StringLength(50)]
        public string PaymentStatus { get; set; } = "Pending";

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal SubtotalAmount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal DiscountAmount { get; set; } = 0;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TaxAmount { get; set; } = 0;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ShippingAmount { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        [StringLength(50)]
        public string? PaymentMethod { get; set; }

        public string? NotesForAdmin { get; set; }
        public string? CustomerNotes { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow; 
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

        [ForeignKey("BillingAddressId")]
        public virtual Address BillingAddress { get; set; }

        [ForeignKey("ShippingAddressId")]
        public virtual Address ShippingAddress { get; set; }
    }
}
