using System;
using System.Collections.Generic;

namespace ProductAPI.Dto
{
    public class PagedResultDto<T>
    {
        public List<T> Items { get; set; }
        public long Total { get; set; }
        public PagedResultDto(long total, List<T> items)
        {
            this.Items = items;
            this.Total = total;
        }

    }
}
