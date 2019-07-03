using System;
using MongoDB.Driver;
using ProductAPI.Repository.Configuration;

namespace ProductAPI.Repository
{
    public class MongoDatabaseProvider : IMongoDatabaseProvider
    {
        IMongoDbConfiguration _configuration;
        public MongoDatabaseProvider(IMongoDbConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IMongoDatabase Database => new MongoClient(_configuration.ConnectionString)
                                .GetDatabase(_configuration.DatabaseName);
    }
}
