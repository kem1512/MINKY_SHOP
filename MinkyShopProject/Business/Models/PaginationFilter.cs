using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Models
{
    public class PaginationFilter
    {
        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public Uri FirstPage { get; set; }

        public Uri LastPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }

        public Uri NextPage { get; set; }

        public Uri PreviousPage { get; set; }
    }
}
