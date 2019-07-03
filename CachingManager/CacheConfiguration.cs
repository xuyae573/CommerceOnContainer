using System;
namespace Core.Caching
{
    public class CacheConfiguration
    {
        public string RedisCachingConnectionString { get; set; }
        public bool EnableRedis { get; set; }
        public CacheConfiguration()
        {
        }
    }
}
