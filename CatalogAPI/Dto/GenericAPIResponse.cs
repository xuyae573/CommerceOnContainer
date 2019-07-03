using System;
namespace ProductAPI.Dto
{
    public class GenericAPIResponse
    {
        public GenericAPIResponse()
        {
        }

        public bool Success { get; set; }
        public object Result { get; set; }
        public string Error { get; set; }
    }
}
