using System;
using System.Collections.Generic;

namespace Shopify.Core.Entities;

public partial class Inventory
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public int QuantityOnHand { get; set; }

    /// <summary>
    /// In carts or processing orders
    /// </summary>
    public int ReservedQuantity { get; set; }

    /// <summary>
    /// Low stock threshold
    /// </summary>
    public int? ReorderLevel { get; set; }

    public DateTime? LastStockUpdatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;
}
