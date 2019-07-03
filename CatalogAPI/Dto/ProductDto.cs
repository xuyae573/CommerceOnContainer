using System;
using MongoDB.Bson.Serialization.Attributes;
using ProductAPI.Attributes;

namespace ProductAPI.Dto
{
    [MongoCollection("products")]
    public class ProductDto : EntityDto
    {
        public ProductDto()
        {
        }
        [BsonElement("asin")]
        public string Asin { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("phone_url")]
        public string PhoneUrl { get; set; }

       

    }
}
