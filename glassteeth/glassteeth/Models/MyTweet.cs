using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace glassteeth.Models
{
    public class MyTweet
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Location { get; set; }
        public string Sentiment { get; set; }

        public MyTweet()
        {
            
        }

        public MyTweet(string body, string latitude, string longitude, string sentiment)
        {
            Body = body;
            Latitude = latitude;
            Longitude = longitude;
            Sentiment = sentiment;
        }

        public MyTweet(string body, string location, string sentiment)
        {
            Body = body;
            Location = location;
            Sentiment = sentiment;
        }
    }
}