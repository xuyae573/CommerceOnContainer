using System;
using ProductAPI.Dto;

namespace ProductAPI.Repository
{
    public class ProductRepository : MongoDbRepositoryBase<ProductDto>
    {
        public ProductRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
        }
    }
}
