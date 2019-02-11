using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruiter.ViewModels
{
    public class ApiResult<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
        public bool HasError { get; set; }
    }
}