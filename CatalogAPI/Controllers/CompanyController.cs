using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ProductAPI.Dto;
using ProductAPI.Repository;
using ProductAPI.Service;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        [Route("{currentPage}/{pageSize}")]
        public GenericAPIResponse Get(int currentPage,int pageSize)
        {
            var res = new GenericAPIResponse();
            res.Success = true;
            res.Result = _companyService.GetAll(
                new PagingRequestDto
                {
                    CurrentPage = currentPage,
                    PageSize = pageSize
                });
            return res;
        }

        [HttpGet]
        [Route("GetByName/{companyName}")]
        public GenericAPIResponse GetByCompanyName(string companyName)
        {
            var res = new GenericAPIResponse();
            res.Success = true;
            res.Result = _companyService.GetCompanyByName(companyName);
            return res;
        }

       [HttpGet]
        [Route("GetById/{id}")]
        public GenericAPIResponse GetById(string id)
        {
            var res = new GenericAPIResponse();
            res.Success = true;
            res.Result = _companyService.GetById(new ObjectId(id));
            return res;
       }
    }
}
