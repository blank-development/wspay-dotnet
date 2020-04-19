using System;
using System.Net;

namespace WSPay.Net
{
    public class WSPayException : Exception
    {
        public WSPayException(string message)
            : base(message)
        {
        }

        public WSPayException(HttpStatusCode httpStatusCode, string message)
            : base(message)
        {
            this.HttpStatusCode = httpStatusCode;
        }

        public HttpStatusCode HttpStatusCode { get; set; }
    }
}