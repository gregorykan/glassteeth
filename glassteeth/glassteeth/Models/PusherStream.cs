using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PusherServer;
using System.Threading.Tasks;
using Tweetinvi;

namespace glassteeth.Models
{
    public static class PusherStream
    {
        public static string Term { get; set; }

        public static void StartAsyncTask()
        {
            Task.Factory.StartNew(TweetStream);
        }

        public static void TweetStream()
        {
            TwitterAuth twitAuth = new TwitterAuth();
            Pusher pusher = new Pusher("117600", "45c06fa98717fe603c5a", "62f457d7131dbb7708b4");
            SentimentAnalysis SA = new SentimentAnalysis();

            var filteredStream = Stream.CreateFilteredStream();
            filteredStream.AddTrack(Term);
            filteredStream.MatchingTweetReceived += (sender, arg) =>
            {
                pusher.Trigger("tweetStream", "tweetEvent", new { message = arg.Tweet.Text });
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