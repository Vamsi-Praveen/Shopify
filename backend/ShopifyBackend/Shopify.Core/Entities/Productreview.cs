using System;
using System.Collections.Generic;

namespace Shopify.Core.Entities;

public partial class Productreview
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public Guid UserId { get; set; }

    /// <summary>
    /// 1 to 5
    /// </summary>
    public int Rating { get; set; }

    public string? Comment { get; set; }

    public bool? IsApproved { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
