using System;
using System.Collections.Generic;

namespace Shopify.Core.Entities;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public decimal Amount { get; set; }

    public string? PaymentGatewayTransactionId { get; set; }

    /// <summary>
    /// e.g., Success, Failed, Pending
    /// </summary>
    public string Status { get; set; } = null!;

    public string? PaymentMethodUsed { get; set; }

    /// <summary>
    /// Full response from gateway for debugging
    /// </summary>
    public string? PaymentGatewayResponse { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual Order Order { get; set; } = null!;
}
