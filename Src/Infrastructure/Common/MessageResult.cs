using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Common
{

    public partial class MessageResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Redirect { get; set; }
        public ResultStatusCode StatusCode { get; set; }
        public SerializableError Errors { get; set; }

        public MessageResult(bool isSuccess, ResultStatusCode statusCode, string message = null, string redirect = null)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Redirect = redirect;
            Message = message ?? statusCode.ToString();

            Errors = new SerializableError();

        }
    }

    public partial class MessageResult<T> : MessageResult  
    {
        public T Data { get; set; }

        public MessageResult
            (bool isSuccess, ResultStatusCode statusCode, T data, string message = null, string redirect = null)
            : base(isSuccess, statusCode, message, redirect)
        {
            Data = data;
        }


    }
}
