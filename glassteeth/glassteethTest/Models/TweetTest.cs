using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using glassteeth.Models;
using NUnit.Framework;

namespace glassteethTest.Models
{
    [TestFixture]
    class TweetTest
    {
        [Test]
        public void create_a_class_for_tweets()
        {
            // Arrange
            var tweet = new MyTweet();
            // Act

            // Assert
            Assert.IsNotNull(tweet);
        }

        [Test]
        public void add_body_property_to_tweet_class()
        {
            // Arrange
            var tweet = new MyTweet("asdasda", "Here");

            // Act

            // Assert
            Assert.IsNotNull(tweet.Body);
        }

        [Test]
        public void add_longitude_and_latitude_properties_to_tweet()
        {
            // Arrange
            var tweet = new MyTweet("asdad", "123", "123");

            // Act

            // Assert
            Assert.IsNotNull(tweet.Longitude);
            Assert.IsNotNull(tweet.Latitude);
        }

        [Test]
        public void add_location_property ()
        {
            // Arrange
            var tweet = new MyTweet("asd", "Auckland");
            // Act

            // Assert
            Assert.IsNotNull(tweet.Location);
        }

    }
}
