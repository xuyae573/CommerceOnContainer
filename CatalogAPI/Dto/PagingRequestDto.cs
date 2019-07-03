using System;
namespace ProductAPI.Dto
{
    public class PagingRequestDto
    {
        public PagingRequestDto()
        {
        }

        public int CurrentPage { get; set; }

        public int SkipCount {
            get
            {
                return (this.CurrentPage - 1) * this.PageSize;
            }
        }

        public int PageSize { get; set; }
        /// <summary>
        /// Json Format
        /// </summary>
        /// <value>The filter.</value>
        public string Filter { get; set; }

        /// <summary>
        /// Sorting information.
        /// Should include sorting field and optionally a direction (ASC or DESC)
        /// Can contain more than one field separated by comma (,).
        /// </summary>
        /// <example>
        /// Examples:
        /// "Name"
        public string SortingProperty { get; set; }

        public string Ordering { get; set; }
    }
}
