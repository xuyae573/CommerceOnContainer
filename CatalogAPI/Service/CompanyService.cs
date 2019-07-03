using System;
using MongoDB.Bson;
using ProductAPI.Dto;
using ProductAPI.Repository;
using System.Linq;

namespace ProductAPI.Service
{
    public class CompanyService : ICompanyService
    {
        private CompanyRepository _repository;
        public CompanyService(CompanyRepository repository)
        {
            _repository = repository;
        }

        public PagedResultDto<CompanyDto> GetAll(PagingRequestDto pagingRequest)
        {
            return _repository.GetAllAsync(pagingRequest.CurrentPage, pagingRequest.PageSize);
        }

        public CompanyDto GetById(ObjectId id)
        {
           return  _repository.Get(id);
        }

        public CompanyDto GetCompanyByName(string companyName)
        {
            var list = _repository.GetAll().ToList();

            return list.FirstOrDefault(x => x.Name == companyName);
        }
    }
}
