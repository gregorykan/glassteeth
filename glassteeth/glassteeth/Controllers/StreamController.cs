using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using glassteeth.Models;

namespace glassteeth.Controllers
{
    public class StreamController : ApiController
    {
        public string GetMyStream(string input)
        {
            PusherStream stream = new PusherStream();
            return "hello";
        }
    }
}
