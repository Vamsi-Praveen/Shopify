using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Core.Communication
{
    public class ServiceResult
    {
        public ServiceResult(bool success, string message, object resource = null)
        {
            Success = success;
            Message = message;
            Resource = resource;
        }

        public bool Success { get; protected set; }

        public string Message { get; protected set; }

        public object Resource { get; protected set; }
    }
}
