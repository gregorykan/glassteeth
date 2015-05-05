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
        public IEnumerable<MyTweet> ParseITweets (IEnumerable<ITweet> tweets)
        {
            List<MyTweet> queryTweets = new List<MyTweet>();

            foreach (ITweet tweet in tweets)
            {
                if (tweet.Coordinates != null)
                {
                    MyTweet thisTweet = new MyTweet(tweet.Text, tweet.Coordinates.Latitude.ToString(), tweet.Coordinates.Longitude.ToString());
                    queryTweets.Add(thisTweet);
                }
                else if (tweet.Place != null)
                {
                    MyTweet thisTweet = new MyTweet(tweet.Text, tweet.Place.Name);
                    queryTweets.Add(thisTweet);
                }
            }
            return queryTweets;
        }
    }
}