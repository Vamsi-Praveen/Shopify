using System;
using System.Collections.Generic;

namespace Shopify.Core.Entities;

public partial class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    /// <summary>
    /// Stock Keeping Unit
    /// </summary>
    public string Sku { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public Guid CategoryId { get; set; }

    public Guid? BrandId { get; set; }

    public decimal BasePrice { get; set; }

    public decimal SellingPrice { get; set; }

    /// <summary>
    /// e.g., kg, g, piece, liter, pack
    /// </summary>
    public string UnitOfMeasure { get; set; } = null!;

    public bool? IsActive { get; set; }

    public bool? IsFeatured { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? ThumbnailImage { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Inventory? Inventory { get; set; }

    public virtual ICollection<Inventorytransaction> Inventorytransactions { get; set; } = new List<Inventorytransaction>();

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual ICollection<Productimage> Productimages { get; set; } = new List<Productimage>();

    public virtual ICollection<Productreview> Productreviews { get; set; } = new List<Productreview>();
}
