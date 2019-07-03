using System;
using System.Linq;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ProductAPI.Attributes;
using ProductAPI.Dto;

namespace ProductAPI.Repository
{
    public class MongoDbRepositoryBase<TEntity> : MongoDbRepositoryBase<TEntity, ObjectId> where TEntity : EntityDto
    {
        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
        }
 
    }


    public class MongoDbRepositoryBase<TEntity, TPrimaryKey> where TEntity : EntityDto
    {
        public virtual IMongoDatabase Database
        {
            get
            {
                return _databaseProvider.Database;
            }
        }

        public string CollectionName
        {
            get
            {
                return GetMongoAliasAttributeValue();
            }
        }

        private string GetMongoAliasAttributeValue()
        {
            var attrs = typeof(TEntity).GetCustomAttributes();
            foreach (var item in attrs)
            {
                if(item is MongoCollectionAttribute)
                {
                    return ((MongoCollectionAttribute)item).Name;
                }
            }

            return typeof(TEntity).Name;
        }

        public virtual IMongoCollection<TEntity> Collection => _databaseProvider.Database.GetCollection<TEntity>(CollectionName);

        private readonly IMongoDatabaseProvider _databaseProvider;

        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }


        public IQueryable<TEntity> GetAll()
        {
            return Collection.AsQueryable();
        }

        public TEntity Get(TPrimaryKey id)
        {

            var query = Builders<TEntity>.Filter.Eq("_id", id);

            var entity = Collection.Find(query).FirstOrDefault();

            if (entity == null)
            {
                throw new Exception("There is no such an entity with given primary key. Entity type: " + typeof(TEntity).FullName + ", primary key: " + id);
            }
            return entity;
        }

        public TEntity Insert(TEntity entity)
        {
            Collection.InsertOne(entity);
            return entity;
        }


        public TEntity Update(TEntity entity)
        {
            var result = Collection.ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity, new UpdateOptions
            {
                IsUpsert = true
            });

            return entity;
        }

        public void Delete(TPrimaryKey id)
        {
            Collection.DeleteOneAsync(x => x.Id.Equals(id));
        }
        public void Delete(TEntity entity)
        {
            Collection.DeleteOneAsync(x => x.Id.Equals(entity.Id));
        }
    }


}
