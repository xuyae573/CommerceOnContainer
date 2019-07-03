using System;
using MongoDB.Driver;

namespace ProductAPI.Repository
{
    public interface IMongoDatabaseProvider
    {
        IMongoDatabase Database { get; }
    }
}
