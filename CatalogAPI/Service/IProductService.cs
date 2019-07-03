
using System.Collections.Generic;
using MongoDB.Bson;
using ProductAPI.Dto;

namespace ProductAPI.Service
{
    public interface IProductService
    {

        PagedResultDto<ProductDto> GetProducts(PagingRequestDto pagingRequestDto);

        ProductDto Create(ProductDto productDto);

        ProductDto Update(ProductDto productDto);

        ProductDto Get(ObjectId id);
    }
}
