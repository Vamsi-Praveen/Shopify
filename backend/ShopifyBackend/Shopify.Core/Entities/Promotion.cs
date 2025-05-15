using System;
using System.Collections.Generic;

namespace Shopify.Core.Entities;

public partial class Promotion
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    /// <summary>
    /// Percentage, FixedAmount
    /// </summary>
    public string DiscountType { get; set; } = null!;

    public decimal DiscountValue { get; set; }

    public decimal? MinOrderAmount { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool? IsActive { get; set; }

    public int? MaxUses { get; set; }

    public int? UsageCount { get; set; }
}
