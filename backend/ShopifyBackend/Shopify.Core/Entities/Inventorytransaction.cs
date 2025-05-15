using System;
using System.Collections.Generic;

namespace Shopify.Core.Entities;

public partial class Inventorytransaction
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    /// <summary>
    /// e.g., StockIn, Sale, Return, AdjustmentOut
    /// </summary>
    public string TransactionType { get; set; } = null!;

    public int QuantityChanged { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? Notes { get; set; }

    /// <summary>
    /// e.g., OrderId, PurchaseOrderId
    /// </summary>
    public string? ReferenceId { get; set; }

    public string? BatchNumber { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public virtual Product Product { get; set; } = null!;
}
