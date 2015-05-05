using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using glassteeth.Models;
using Tweetinvi;
using Tweetinvi.Core.Extensions;
using Tweetinvi.Core.Interfaces;

namespace glassteeth.Controllers
{
    public class MyTweetsController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MyTweets
        public IEnumerable<MyTweet> GetMyTweets(string input)
        {
            TwitterAuth twitterAuth = new TwitterAuth();
            ITweetParser parser = new ITweetParser();
            
            var searchParameters = Search.CreateTweetSearchParameter(input);
            searchParameters.MaximumNumberOfResults= 1000;
            var tweets = Search.SearchTweets(searchParameters);

            return parser.ParseITweets(tweets);
        }



        // GET: api/MyTweets/5
        [ResponseType(typeof(MyTweet))]
        public IHttpActionResult GetMyTweet(int id)
        {
            MyTweet myTweet = db.MyTweets.Find(id);
            if (myTweet == null)
            {
                return NotFound();
            }

            return Ok(myTweet);
        }

        // PUT: api/MyTweets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMyTweet(int id, MyTweet myTweet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != myTweet.Id)
            {
                return BadRequest();
            }

            db.Entry(myTweet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyTweetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MyTweets
        [ResponseType(typeof(MyTweet))]
        public IHttpActionResult PostMyTweet(MyTweet myTweet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MyTweets.Add(myTweet);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = myTweet.Id }, myTweet);
        }

        // DELETE: api/MyTweets/5
        [ResponseType(typeof(MyTweet))]
        public IHttpActionResult DeleteMyTweet(int id)
        {
            MyTweet myTweet = db.MyTweets.Find(id);
            if (myTweet == null)
            {
                return NotFound();
            }

            db.MyTweets.Remove(myTweet);
            db.SaveChanges();

            return Ok(myTweet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MyTweetExists(int id)
        {
            return db.MyTweets.Count(e => e.Id == id) > 0;
        }
    }
}