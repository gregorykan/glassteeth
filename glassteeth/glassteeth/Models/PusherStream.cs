using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PusherServer;

namespace glassteeth.Models
{
    public class PusherStream
    {
        private string pusherAppId = "117600";
        private string pusherAppKey = "45c06fa98717fe603c5a";
        private string pusherAppSecret = "62f457d7131dbb7708b4";

        public PusherStream()
        {
            var pusher = new Pusher(pusherAppId, pusherAppKey, pusherAppSecret);
        }
    }
}