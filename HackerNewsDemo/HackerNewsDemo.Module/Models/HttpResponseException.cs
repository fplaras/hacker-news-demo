﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HackerNewsDemo.Module.Models
{
    public class HttpResponseException : Exception
    {
        public HttpStatusCode Status { get; private set; }

        public HttpResponseException(HttpStatusCode status, string msg) : base(msg)
        {
            Status = status;
        }
    }
}
