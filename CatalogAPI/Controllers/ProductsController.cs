using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ProductAPI.Dto;
using ProductAPI.Service;

namespace ProductAPI.Controllers
{

    public class ProductFilter
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string Order { get; set; }
        public string Keyword { get; set; }
    }

    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
         
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        

        [HttpPost]
        [Route("GetProducts")]
        public GenericAPIResponse Get([FromBody]ProductFilter productFilter)
        {
            var res = new GenericAPIResponse();
            res.Success = true;
            res.Result =  _productService.GetProducts(new Dto.PagingRequestDto()
            {
                CurrentPage = productFilter.CurrentPage,
                PageSize = productFilter.PageSize,
                SortingProperty = productFilter.SortColumn,
                Ordering = productFilter.Order == "ascending" ? "ASC" : "DESC",
                Filter = productFilter.Keyword
            });


            return res;
        }
        [HttpGet]
        [Route("get")]
        public GenericAPIResponse Get(string id)
        {
            var res = new GenericAPIResponse();
            res.Success = true;
            res.Result = _productService.Get(new ObjectId(id));
            return res;
        }

        [HttpPost]
        [Route("create")]
        public GenericAPIResponse Create([FromBody]ProductDto product)
        {
            var res = new GenericAPIResponse();
            res.Success = true;
            res.Result = _productService.Create(product);
            return res;
        }

        [HttpPost]
        [Route("update")]
        public GenericAPIResponse Update([FromBody]ProductDto product)
        {
            var res = new GenericAPIResponse();
            res.Success = true;
            res.Result = _productService.Update(product);
            return res;
        }

    }
}