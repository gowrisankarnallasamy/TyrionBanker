using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TyrionBanker.ApiWeb.Results
{
    public class Result<T> where T : class
    {
        public T Data { get; set; }
        public bool HasError { get; set; }
        public string Exception { get; set; }
    }
}