using System;
using MongoDB.Bson;
using ProductAPI.Dto;

namespace ProductAPI.Service
{
    public interface ICompanyService
    {
        PagedResultDto<CompanyDto> GetAll(PagingRequestDto pagingRequest);

        CompanyDto GetById(ObjectId id);

        CompanyDto GetCompanyByName(string companyName);

    }
}
