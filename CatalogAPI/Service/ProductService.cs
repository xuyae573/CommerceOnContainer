using Core.Caching;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ProductAPI.Dto;
using ProductAPI.Repository;

using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductAPI.Service
{
    public class ProductService : IProductService
    {
        #region Cache Constants
        private static readonly string Product_ListSearch_Pattern = "Phones_Search_";
        #endregion
        ProductRepository _productRepository;
        public Core.Caching.IStaticCacheManager _cacheManager { get; set; }
        public ProductService(ProductRepository productRepository,
        IStaticCacheManager cacheManager)
        {
            _productRepository = productRepository;
            _cacheManager = cacheManager;
        }

        public ProductDto Create(ProductDto productDto)
        {
            _cacheManager.RemoveByPattern(Product_ListSearch_Pattern);
            return _productRepository.Insert(productDto);
        }
        public ProductDto Update(ProductDto productDto)
        {
            _cacheManager.RemoveByPattern(Product_ListSearch_Pattern);
            return _productRepository.Update(productDto);
        }

        public PagedResultDto<ProductDto> GetProducts(PagingRequestDto pagingRequestDto)
        {
            long total = 0;
            var search_Cache_Key = string.Format(Product_ListSearch_Pattern + "{0}_{1}_{2}_{3}_{4}",
                pagingRequestDto.SkipCount, pagingRequestDto.PageSize, pagingRequestDto.SortingProperty,
                pagingRequestDto.Ordering, pagingRequestDto.Filter);

            SortDefinition<ProductDto> sort = null;
            if (pagingRequestDto.Ordering == "ASC")
            {
                sort = Builders<ProductDto>.Sort.Ascending(pagingRequestDto.SortingProperty);
            }
            else
            {
                sort = Builders<ProductDto>.Sort.Descending(pagingRequestDto.SortingProperty);
            }

            var builder = Builders<ProductDto>.Filter;
            List<ProductDto> result = new  List<ProductDto>();
            if(!string.IsNullOrEmpty(pagingRequestDto.Filter))
            {
                var filter = builder.Regex("name", new BsonRegularExpression(pagingRequestDto.Filter));
                var totalRes = _productRepository.Collection.Find(filter);
                total = totalRes.ToList().Count();
                result = _cacheManager.Get<List<ProductDto>>(search_Cache_Key, () =>
                {
                  return totalRes.Sort(sort)
                    .Skip(pagingRequestDto.SkipCount)
                    .Limit(pagingRequestDto.PageSize)
                    .ToList();
                }, 100);
            }
            else
            {
                var totalRes = _productRepository.Collection.Find(x=>true);
                total = totalRes.ToList().Count();
                result = _cacheManager.Get<List<ProductDto>>(search_Cache_Key, () =>
                {
                   return  totalRes.Sort(sort)
                    .Skip(pagingRequestDto.SkipCount)
                    .Limit(pagingRequestDto.PageSize)
                    .ToList();
                }, 100);
            }
           
            //total = result.Count();

            return new PagedResultDto<ProductDto>(total, result);
        }

        public ProductDto Get(ObjectId id)
        {
           return _productRepository.Get(id);
        }
    }
}
