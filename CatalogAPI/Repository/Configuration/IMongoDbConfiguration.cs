using System;
namespace ProductAPI.Repository.Configuration
{
    public interface IMongoDbConfiguration
    {
          string ConnectionString { get; set; }

          string DatabaseName { get; set; }
    }
}
