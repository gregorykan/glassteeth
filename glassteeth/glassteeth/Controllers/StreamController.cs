using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using glassteeth.Models;
using PusherServer;
using Tweetinvi;

namespace glassteeth.Controllers
{
    public class StreamController : ApiController
    {
        public void GetMyStream(string input)
        {
            PusherStream.Term = input;
            PusherStream.StartAsyncTask();
        }
    }
}
