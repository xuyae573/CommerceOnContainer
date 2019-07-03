using System;

namespace IdentityServerAPI.Models
{
    public class GenericAPIResponse
    {
        public bool Success { get; set; }
        public object Result { get; set; }
        public string Error { get; set; }
    }
}
