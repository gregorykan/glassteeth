using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using glassteeth.Models;
using Tweetinvi;

namespace glassteeth.Controllers
{
    public class StreamController : ApiController
    {
        public string GetMyStream(string input)
        {
            PusherStream stream = new PusherStream();
            SentimentAnalysis SA = new SentimentAnalysis();
            
            var filteredStream = Stream.CreateFilteredStream();
            filteredStream.AddTrack(input);
            filteredStream.MatchingTweetReceived += (sender, arg) =>
            {
                if (arg.Tweet.Coordinates != null)
                {
                    string sentiment = SA.Analyze(arg.Tweet.Text);
                    MyTweet thisTweet = new MyTweet(arg.Tweet.Text,
                        arg.Tweet.Coordinates.Latitude.ToString(), arg.Tweet.Coordinates.Longitude.ToString(), sentiment);
                    stream.Pusher.Trigger("tweetStream", "tweetEvent", new {message = thisTweet});
                }
                else if (arg.Tweet.Place != null)
                {
                    string sentiment = SA.Analyze(arg.Tweet.Text);
                    MyTweet thisTweet = new MyTweet(arg.Tweet.Text, arg.Tweet.Place.Name, sentiment);
                    stream.Pusher.Trigger("tweetStream", "tweetEventWithPlace", new {message = thisTweet});
                }
            };
            filteredStream.StartStreamMatchingAllConditions();
            return "hello";
        }
    }
}
