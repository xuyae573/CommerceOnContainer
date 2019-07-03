using System;
namespace ProductAPI.Repository.Configuration
{
    public class MongoDbConfiguration : IMongoDbConfiguration
    {
        public MongoDbConfiguration()
        {

        }
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
