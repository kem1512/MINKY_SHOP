using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Models
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int ToltalPages { get; set; }

        public int TotalRecords { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
            Message = null;
            Status = true;
            Errors = null;
        }
    }
}
