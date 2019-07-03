using System;
using System.Collections.Generic;

using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Linq;
using ProductAPI.Dto;

namespace ProductAPI.Repository
{
    public class CompanyRepository : MongoDbRepositoryBase<CompanyDto>
    {
        public CompanyRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
        }

        public PagedResultDto<CompanyDto> GetAllAsync(int currentPage, int pageSize = 10)
        {
            /*var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("ca2");
            var collection = database.GetCollection<CompanyDto>("companies");*/

            var list = GetAll().ToList();
            var total = list.Count;
            list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResultDto<CompanyDto>(total, list);
        }


    }


}
