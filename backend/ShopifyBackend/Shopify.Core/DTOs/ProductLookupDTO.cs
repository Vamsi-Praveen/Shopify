using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.DTOs
{
    public class ProductLookupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }

}
