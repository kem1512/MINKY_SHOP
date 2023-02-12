using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Models
{
    public class Response<T>
    {
        public Response()
        {

        }

        public Response(T data)
        {
            Status = true;
            Message = null!;
            Errors = null!;
            Data = data;
        }

        public T Data { get; set; } = default(T)!;

        public bool Status { get; set; }

        public string[] Errors { get; set; } = null!;

        public string Message { get; set; } = null!;
    }
}
