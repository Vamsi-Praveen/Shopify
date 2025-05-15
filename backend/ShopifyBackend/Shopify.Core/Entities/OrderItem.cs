using System;
using System.Collections.Generic;

namespace Shopify.Core.Entities;

public partial class Orderitem
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    /// <summary>
    /// Product name at time of order
    /// </summary>
    public string ProductNameSnapshot { get; set; } = null!;

    public string SkuSnapshot { get; set; } = null!;

    public int Quantity { get; set; }

    /// <summary>
    /// Price per unit at time of order
    /// </summary>
    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
