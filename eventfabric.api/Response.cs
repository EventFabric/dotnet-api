using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace eventfabric.api
{
    public class Response
    {
        private readonly String _statusDescription;
        private readonly int _statusCode;
        private readonly CookieCollection _cookies;
        private readonly String _body;

        public Response(String statusDescription, int statusCode, String body, CookieCollection cookies)
        {
            _statusDescription = statusDescription;
            _statusCode = statusCode;
            _cookies = cookies;
            _body = body;
        }

        public Response(string statusDescription, int statusCode)
            : this(statusDescription, statusCode, string.Empty, new CookieCollection())
        {
            
        }

        public string Body
        {
            get { return _body; }
        }

        public int StatusCode
        {
            get { return _statusCode; }
        }

        public string StatusDescription
        {
            get { return _statusDescription; }
        }

        public CookieCollection Cookies
        {
            get { return _cookies; }
        }
    }
}
