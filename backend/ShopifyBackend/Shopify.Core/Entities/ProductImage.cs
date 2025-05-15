using System;
using System.Collections.Generic;

namespace Shopify.Core.Entities;

public partial class Productimage
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public string? AltText { get; set; }

    public bool? IsPrimary { get; set; }

    public int? DisplayOrder { get; set; }

    public virtual Product Product { get; set; } = null!;
}
