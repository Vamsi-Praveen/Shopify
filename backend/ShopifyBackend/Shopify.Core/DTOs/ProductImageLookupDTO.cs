﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.DTOs
{
    public class ProductImageLookupDTO
    {
        public Guid Id { get; set; }
        public string ImageURL { get; set; } = null!;
    }

}
