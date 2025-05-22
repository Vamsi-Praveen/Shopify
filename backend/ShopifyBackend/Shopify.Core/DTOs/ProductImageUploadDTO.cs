using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.DTOs
{
    public class ProductImageUploadDTO
    {
        public Guid ProductId { get; set; }
        public IFormFile Image { get; set; }

        public string? ImageUrl { get; set; }
    }
}
