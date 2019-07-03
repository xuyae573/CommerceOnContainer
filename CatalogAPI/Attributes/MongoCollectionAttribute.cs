using System;
namespace ProductAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class MongoCollectionAttribute : Attribute
    {
        public string Name { get; set; }
        public MongoCollectionAttribute(string collectionName)
        {
            Name = collectionName;
        }
    }
}
