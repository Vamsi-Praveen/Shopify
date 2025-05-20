﻿using System;
using System.Collections.Generic;

namespace Shopify.Core.Entities;

public partial class Brand
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? LogoUrl { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
