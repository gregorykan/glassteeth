using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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
        public static string StreamID { get; set; }

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
                if (arg.Tweet.Coordinates != null)
                {
                    Debug.WriteLine("tweet with coordinates");
                    string sentiment = SA.Analyze(arg.Tweet.Text);
                    MyTweet thisTweet = new MyTweet(arg.Tweet.Text,
                        arg.Tweet.Coordinates.Latitude.ToString(), arg.Tweet.Coordinates.Longitude.ToString(), sentiment);
                    pusher.Trigger(StreamID, "tweetEvent", new { message = thisTweet });
                }
                else if (arg.Tweet.Place != null)
                {
                    Debug.WriteLine("tweet with location");
                    string sentiment = SA.Analyze(arg.Tweet.Text);
                    MyTweet thisTweet = new MyTweet(arg.Tweet.Text, arg.Tweet.Place.Name, sentiment);
                    pusher.Trigger(StreamID, "tweetEventWithPlace", new { message = thisTweet });
                }
            };
            filteredStream.StartStreamMatchingAllConditions();
        }
    }
}