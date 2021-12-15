using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_Server.Helpers
{
    public class Response<T>
    {
        public Response(T data) => Data = data;
        public Response(string error) => Error = error;
        public T Data { private set; get; }
        public string Error { private set; get; }
        public bool Ok => Error is not null;
    }
}
