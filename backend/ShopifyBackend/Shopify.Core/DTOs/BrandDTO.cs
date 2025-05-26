using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.DTOs
{
    public class BrandDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? LogoUrl { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
