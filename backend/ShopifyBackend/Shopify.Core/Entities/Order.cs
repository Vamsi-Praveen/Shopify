using System;
using System.Collections.Generic;

namespace Shopify.Core.Entities;

public partial class Order
{
    public Guid Id { get; set; }

    /// <summary>
    /// Human-readable ID, e.g., ORD-2023-00001
    /// </summary>
    public string OrderNumber { get; set; } = null!;

    public Guid UserId { get; set; }

    public Guid BillingAddressId { get; set; }

    public Guid ShippingAddressId { get; set; }

    /// <summary>
    /// Pending, Processing, Shipped, Delivered, Cancelled
    /// </summary>
    public string OrderStatus { get; set; } = null!;

    /// <summary>
    /// Pending, Paid, Failed, Refunded
    /// </summary>
    public string PaymentStatus { get; set; } = null!;

    public decimal SubtotalAmount { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? TaxAmount { get; set; }

    public decimal? ShippingAmount { get; set; }

    public decimal TotalAmount { get; set; }

    public string? PaymentMethod { get; set; }

    public string? NotesForAdmin { get; set; }

    public string? CustomerNotes { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Address BillingAddress { get; set; } = null!;

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Address ShippingAddress { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
