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
        public string GetMyStream(string input)
        {
            Random random = new Random();
            string streamID = random.Next(0, 10000).ToString();
            PusherStream.StreamID = streamID;
            PusherStream.Term = input;
            PusherStream.StartAsyncTask();
            return streamID;
        }
    }
}
