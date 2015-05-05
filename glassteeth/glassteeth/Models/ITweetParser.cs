using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Tweetinvi.Core.Interfaces;

namespace glassteeth.Models
{
    public class ITweetParser
    {
        SentimentAnalysis SA { get; set; }

        public ITweetParser()
        {
            SA = new SentimentAnalysis();    
        }

        public IEnumerable<MyTweet> ParseITweets (IEnumerable<ITweet> tweets)
        {
            List<MyTweet> queryTweets = new List<MyTweet>();

            foreach (ITweet tweet in tweets)
            {
                if (tweet.Coordinates != null)
                {
                    string sentiment = SA.Analyze(tweet.Text);
                    MyTweet thisTweet = new MyTweet(tweet.Text, tweet.Coordinates.Latitude.ToString(), tweet.Coordinates.Longitude.ToString(), sentiment);
                    queryTweets.Add(thisTweet);
                }
                else if (tweet.Place != null)
                {
                    string sentiment = SA.Analyze(tweet.Text);
                    MyTweet thisTweet = new MyTweet(tweet.Text, tweet.Place.Name, sentiment);
                    queryTweets.Add(thisTweet);
                }
            }
            return queryTweets;
        }
    }
}