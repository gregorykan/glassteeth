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
            TwitterAuth twitterAuth = new TwitterAuth();
            PusherStream stream = new PusherStream();
            SentimentAnalysis SA = new SentimentAnalysis();

            stream.Pusher.Trigger("tweetStream", "tweetEvent", new {message = "controller method hit"});

            var filteredStream = Stream.CreateFilteredStream();
            filteredStream.AddTrack(input);
            filteredStream.MatchingTweetReceived += (sender, arg) =>
            {
                stream.Pusher.Trigger("tweetStream", "tweetEvent", new { message = "this is a tweet" });
                //if (arg.Tweet.Coordinates != null)
                //{
                //    string sentiment = SA.Analyze(arg.Tweet.Text);
                //    MyTweet thisTweet = new MyTweet(arg.Tweet.Text,
                //        arg.Tweet.Coordinates.Latitude.ToString(), arg.Tweet.Coordinates.Longitude.ToString(), sentiment);
                //    stream.Pusher.Trigger("tweetStream", "tweetEvent", new {message = thisTweet});
                //}
                //else if (arg.Tweet.Place != null)
                //{
                //    string sentiment = SA.Analyze(arg.Tweet.Text);
                //    MyTweet thisTweet = new MyTweet(arg.Tweet.Text, arg.Tweet.Place.Name, sentiment);
                //    stream.Pusher.Trigger("tweetStream", "tweetEventWithPlace", new {message = thisTweet});
                //}
            };
            filteredStream.StartStreamMatchingAllConditions();
        }
    }
}
